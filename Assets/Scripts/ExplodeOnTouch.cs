using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    public GameObject explosion;
    public float explosionDuration;
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //don't collide with self
        if (collision.transform.root == transform.root)
            return;

        //don't collide with triggers
        if (collision.isTrigger)
            return;

        Explode();
    }

    public void Explode()
    {
        gameObject.SetActive(false);

        //play explosion VFX
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), explosionDuration);

        //play explosion sound
        SoundManager.Instance().PlaySoundEffect(sound);
    }
}
