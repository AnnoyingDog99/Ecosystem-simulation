using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlyAlong : MonoBehaviour
{
    [SerializeField] Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.position += (Vector3.right * 1f) * Time.deltaTime;
    }
}
