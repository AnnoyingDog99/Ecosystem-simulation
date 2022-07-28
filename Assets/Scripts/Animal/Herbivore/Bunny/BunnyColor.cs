using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyColor : MonoBehaviour
{
    [SerializeField] MeshRenderer bodyAndHead;
    [SerializeField] Color bodyAndHeadColor;

    [SerializeField] MeshRenderer eyes;
    [SerializeField] Color eyeColor;

    [SerializeField] MeshRenderer innerEars;
    [SerializeField] Color innerEarsColor;

    [SerializeField] MeshRenderer nose;
    [SerializeField] Color noseColor;

    [SerializeField] MeshRenderer paws;
    [SerializeField] Color pawsColor;

    [SerializeField] MeshRenderer tail;
    [SerializeField] Color tailColor;

    // Start is called before the first frame update
    void Start()
    {
        this.bodyAndHead.material.color = this.bodyAndHeadColor;
        this.eyes.material.color = this.eyeColor;
        this.nose.material.color = this.noseColor;
        this.innerEars.material.color = this.innerEarsColor;
        this.paws.material.color = this.pawsColor;
        this.tail.material.color = this.tailColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
