using System.Collections.Generic;
using UnityEngine;

public class HerbivoreModel : MonoBehaviour, IHerbivoreModel
{
    [SerializeField] private List<string> plantTags = new List<string>();

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public List<string> GetPlantTags()
    {
        return this.plantTags;
    }
}
