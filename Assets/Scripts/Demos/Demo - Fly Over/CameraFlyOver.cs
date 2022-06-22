using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlyOver : MonoBehaviour
{
    [SerializeField] Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.position += (Vector3.forward * 5f) * Time.deltaTime;
        _camera.transform.position += (Vector3.down * 0.05f) * Time.deltaTime;
    }
}
