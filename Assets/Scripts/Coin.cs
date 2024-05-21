using UnityEngine;

/// <summary>
/// Add score when picked upp and destroy self
/// </summary>
public class Coin : MonoBehaviour
{
    public int score;

    private void OnTriggerEnter(Collider other)
    {
        var score = Score.Instance();
        score.Add(this.score);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var score = Score.Instance();
        score.Add(this.score);
        Destroy(gameObject);
    }
}
