using System.Collections.Generic;
using UnityEngine;

public class OmnivoreModel : MonoBehaviour, IOmnivoreModel
{
    [SerializeField] protected HerbivoreModel herbivoreModel;
    [SerializeField] protected CarnivoreModel carnivoreModel;

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
        return this.herbivoreModel.GetPlantTags();
    }

    public List<string> GetPreyTags()
    {
        return this.carnivoreModel.GetPreyTags();
    }
}
