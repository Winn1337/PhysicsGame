using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private Score score;
    private Timer timer;

    private void Start()
    {
        score = Score.Instance();
        timer = Timer.Instance();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.Get().ToString("0000");
        timeText.text = "Time: " + timer.Get().ToString("00.00");
    }
}
