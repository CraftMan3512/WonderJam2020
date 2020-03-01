using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanging : MonoBehaviour
{
    private float t;
    private Color red = new Color(195 / 256f, 40 / 256f, 30 / 256f);

    private bool reset = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = Color.Lerp(Color.black, red, t);

        if (t < 1 && reset)
        {
            t += Time.deltaTime * 4;
        }

        if (t >= 1 || !reset)
        {
            t -= Time.deltaTime / 1.5f;
            reset = false;
            if (t <= 0)
            {
                t = 0;
                reset = true;
            }
        }
    }
}