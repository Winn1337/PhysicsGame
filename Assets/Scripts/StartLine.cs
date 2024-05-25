using UnityEngine;

public class StartLine : MonoBehaviour
{
    private void StartTimer()
    {
        var timer = Timer.Instance();
        timer.Start();
        enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        StartTimer();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartTimer();
    }
}
