using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    [SerializeField] Camera _camera;
    public float from = 0f;
    public float to = 360;
    public float duration = 10f;

    private float t;
    // Start is called before the first frame update
    void Start()
    {
        t = from;
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.eulerAngles += new Vector3(0, (Time.deltaTime / (duration / (to - from))), 0);
    }
}
