using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Field : MonoBehaviour
{
    [SerializeField] private Plant initialPlant;
    [SerializeField] protected uint maxAmountOfPlants;

    [SerializeField] private uint spreadTime;
    private float spreadTimer;

    List<Plant> plants = new List<Plant>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.plants.Add(initialPlant);
        this.spreadTimer = this.spreadTime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.plants.Count >= maxAmountOfPlants) return;
        this.spreadTimer -= Time.deltaTime;
        if (this.spreadTimer <= 0)
        {
            this.Spread();
            this.spreadTimer = this.spreadTime;
        }
    }

    protected virtual void Spread()
    {
        List<Plant> fullyGrownPlants = this.plants.FindAll(plant => plant.IsFullyGrown());
        if (fullyGrownPlants.Count <= 0) return;
        int randomIndex = Random.Range(0, fullyGrownPlants.Count - 1);
        Plant parentPlantGameObject = fullyGrownPlants[randomIndex];

        // TODO: Check for empty spots to spread to
        Vector3 newPosition = parentPlantGameObject.transform.position + new Vector3(parentPlantGameObject.transform.localScale.x / 2, 0, parentPlantGameObject.transform.localScale.z / 2);
        float distance = Vector3.Distance(parentPlantGameObject.transform.position, newPosition);
        float offset = Random.Range(-distance / 2, distance / 2);
        newPosition += (Vector3.forward * offset);

        GameObject childPlantGameObject = Instantiate(parentPlantGameObject.gameObject, newPosition, transform.rotation);
        childPlantGameObject.transform.parent = this.gameObject.transform;
        this.plants.Add(childPlantGameObject.GetComponent<Plant>());
    }
}
