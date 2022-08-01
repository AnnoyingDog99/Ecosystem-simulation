using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivoreModel : MonoBehaviour, ICarnivoreModel
{
    [SerializeField] private List<string> preyTags = new List<string>();


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public List<string> GetPreyTags()
    {
        return preyTags;
    }
}
