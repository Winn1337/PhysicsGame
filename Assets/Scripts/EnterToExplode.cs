using UnityEngine;

public class EnterToExplode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ExplodeOnTouch e))
            e.Explode();
    }
}
