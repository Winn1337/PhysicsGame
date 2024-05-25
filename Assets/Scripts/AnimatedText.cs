using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatedText : MonoBehaviour
{
    public AnimationCurve scale;
    public AnimationCurve opacity;
    public string message;

    float length;
    float timer;

    TextMeshProUGUI text;

    void Start()
    {
        length = Mathf.Max(scale.keys[^1].time, opacity.keys[^1].time);
        text = GetComponent<TextMeshProUGUI>();
        text.text = message;

        Destroy(gameObject, length);
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.localScale = Vector3.one * scale.Evaluate(timer);
        var color = text.color;
        color.a = opacity.Evaluate(timer);
        text.color = color;

        if (timer >= length)
            enabled = false;
    }
}
