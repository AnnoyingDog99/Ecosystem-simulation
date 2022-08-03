using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassColor : MonoBehaviour
{
    [SerializeField] MeshRenderer blade1;
    [SerializeField] Color blade1Color;

    [SerializeField] MeshRenderer blade2;
    [SerializeField] Color blade2Color;

    [SerializeField] MeshRenderer blade3;
    [SerializeField] Color blade3Color;

    [SerializeField] MeshRenderer blade4;
    [SerializeField] Color blade4Color;

    [SerializeField] MeshRenderer blade5;
    [SerializeField] Color blade5Color;

    [SerializeField] MeshRenderer blade6;
    [SerializeField] Color blade6Color;

    [SerializeField] MeshRenderer blade7;
    [SerializeField] Color blade7Color;

    [SerializeField] MeshRenderer blade8;
    [SerializeField] Color blade8Color;

    // Start is called before the first frame update
    void Start()
    {
        this.blade1.material.color = this.blade1Color;
        this.blade2.material.color = this.blade2Color;
        this.blade3.material.color = this.blade3Color;
        this.blade4.material.color = this.blade4Color;
        this.blade5.material.color = this.blade5Color;
        this.blade6.material.color = this.blade6Color;
        this.blade7.material.color = this.blade7Color;
        this.blade8.material.color = this.blade8Color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
