using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxColor : MonoBehaviour
{
    [SerializeField] MeshRenderer bodyAndHead;
    [SerializeField] Color bodyAndHeadColor;

    [SerializeField] MeshRenderer chin;
    [SerializeField] Color chinColor;

    [SerializeField] MeshRenderer eyes;
    [SerializeField] Color eyeColor;

    [SerializeField] MeshRenderer feet;
    [SerializeField] Color feetColor;

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
        bodyAndHead.material.color = bodyAndHeadColor;
        chin.material.color = chinColor;
        eyes.material.color = eyeColor;
        nose.material.color = noseColor;
        feet.material.color = feetColor;
        innerEars.material.color = innerEarsColor;
        paws.material.color = pawsColor;
        tail.material.color = tailColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
