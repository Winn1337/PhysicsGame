using UnityEngine;

/// <summary>
/// Add score when picked upp and destroy self
/// </summary>
public class Coin : MonoBehaviour
{
    public int score;
    public AudioClip sound;
    bool alreadyGaveScore;

    private void GiveScore()
    {
        if (alreadyGaveScore)
            return;

        var score = Score.Instance();
        score.Add(this.score);
        //Destroy(gameObject);

        alreadyGaveScore = true;
        SoundManager.Instance().PlaySoundEffect(sound);

        CoinAnim scale = GetComponent<CoinAnim>();
        scale.enabled = true;
        scale.thenDo = () => { Destroy(gameObject); };
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GiveScore();
    }
}
