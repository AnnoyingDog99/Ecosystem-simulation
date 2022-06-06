using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassColor : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> blades;
    [SerializeField] List<Color> colors;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < blades.Count; i++)
        {
            blades[i].material.color = colors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
