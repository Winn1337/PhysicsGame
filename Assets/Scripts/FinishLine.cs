using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void StopTimer()
    {
        var timer = Timer.Instance();
        timer.Stop();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        StopTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopTimer();
    }
}
