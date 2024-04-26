using UnityEngine;

public class StartLine : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var timer = Timer.Instance();
        timer.Start();
        gameObject.SetActive(false);
    }
}
