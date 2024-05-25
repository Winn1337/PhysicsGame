using UnityEngine;

public enum FlipType
{
    Front,
    Back
}

public class Flip : MonoBehaviour
{
    Rigidbody2D rb;
    Rigidbody2D[] wheels;
    private float rotation;
    public LayerMask ground;
    public int flips;
    public FlipType flipType;
    public AudioClip sound;
    public int score;
    public Canvas HUD;
    public AnimatedText text;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //get wheel rigidbodies
        WheelJoint2D[] wheelJoints = GetComponents<WheelJoint2D>();
        wheels = new Rigidbody2D[wheelJoints.Length];
        for (int i = 0; i < wheelJoints.Length; i++)
            wheels[i] = wheelJoints[i].connectedBody;
    }

    void FixedUpdate()
    {
        //if at least one wheel is off the ground, start counting rotation
        foreach(var wheel in wheels)
        {
            if (!wheel.IsTouchingLayers(ground))
            {
                rotation += rb.angularVelocity * Time.fixedDeltaTime;
                return;
            }
        }

        //if all wheels are on the ground, evaluate and reset rotation
        flips = (int)(Mathf.Abs(rotation) / 300f);

        if (flips > 0)
        {
            //print("we performed " + flips + " flip" + (flips > 1 ? "s!" : '!'));
            flipType = rotation > 0 ? FlipType.Back : FlipType.Front;
            Score.Instance().Add(score * flips);
            SoundManager.Instance().PlaySoundEffect(sound);
            Instantiate(text, HUD.transform).message = (flipType == FlipType.Front ? "Front" : "Back") + "flip! +" + score + "pts";
        }

        rotation = 0f;
    }
}
