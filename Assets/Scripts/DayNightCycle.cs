using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Material skyboxMaterial;
    public float cycleLength = 10.0f;
    [SerializeField] Gradient dayNightGradientTop;
    [SerializeField] Gradient dayNightGradientBottom;
    float t = 0f;

    float a = 0f;
    float b = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (a > b)
        {
            t -= (Time.deltaTime / cycleLength);
        }
        else
        {
            t += (Time.deltaTime / cycleLength);
        }
        if ((a > b && t <= b) || (a < b && t >= b))
        {
            float temp = a;
            a = b;
            b = temp;
        }
        Color colorTop = dayNightGradientTop.Evaluate(t);
        Color colorBottom = dayNightGradientBottom.Evaluate(t);
        skyboxMaterial.SetColor("_Top", colorTop);
        skyboxMaterial.SetColor("_Bottom", colorBottom);
    }
}
