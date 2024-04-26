using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var timer = Timer.Instance();
        timer.Stop();
        gameObject.SetActive(false);
    }
}
