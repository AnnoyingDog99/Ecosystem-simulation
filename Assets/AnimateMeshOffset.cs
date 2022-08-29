using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMeshOffset : MonoBehaviour
{
    // Start is called before the first frame update

    public MapGenerator mapGenerator;
    public float flowSpeed = 3.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mapGenerator.noiseSettings.offset += new Vector2(1, 0) * Time.deltaTime;
        mapGenerator.GenerateMap();
    }
}
