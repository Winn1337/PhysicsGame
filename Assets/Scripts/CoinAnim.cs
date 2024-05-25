using System;
using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    public float scale;
    public Vector3 move;
    public Action thenDo;

    private void OnEnable()
    {
        GetComponent<Bob>().enabled = false;
        //GetComponent<Spin>().enabled = false;
    }

    void Update()
    {
        if (transform.localScale.x - scale * Time.deltaTime < 0)
        {
            thenDo.Invoke();
            enabled = false;
            return;
        }

        transform.localScale += scale * Time.deltaTime * Vector3.one;
        transform.position += move * Time.deltaTime;
    }
}
