using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Field : MonoBehaviour
{
    [SerializeField] private Plant referencePlant;
    [SerializeField] protected int maxAmountOfPlants;
    [SerializeField] protected Vector2 size;

    [SerializeField] private uint spreadTime;
    private float spreadTimer;

    List<Plant> plants = new List<Plant>();

    private List<Vector2> points = new List<Vector2>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.spreadTimer = this.spreadTime;
        float avgPointRadius = (referencePlant.GetMaxScale().x + referencePlant.GetMaxScale().y) / 2;
        this.points = PoissonDiscSampling.GeneratePoints(avgPointRadius, size, maxAmountOfPlants, 20);
        for (int i = 0; i < this.points.Count; i++) {
            this.points[i] += new Vector2(this.transform.position.x, this.transform.position.z);
        }
        Debug.Log(this.points[0]);
        if (this.points.Count <= 0)
        {
            Debug.LogError("Field does not have enough room to initialize");
            return;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.plants.Count >= maxAmountOfPlants || this.plants.Count >= this.points.Count) return;
        this.spreadTimer -= Time.deltaTime;
        if (this.spreadTimer <= 0)
        {
            this.SpreadV2();
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

    protected virtual void SpreadV2()
    {
        // Translate Vector2 to Vector3, the y position is determined at the start of the ELACtor so an estimation is enough.
        Vector2 newPositionVector2 = this.points[this.plants.Count];
        Vector3 newPositionVector3 = new Vector3(newPositionVector2.x, transform.position.y, newPositionVector2.y);

        // Random rotation, just because
        float randomNumber = Random.Range(-1, 1);
        Quaternion randomRotation = new Quaternion(randomNumber, 0, randomNumber, 1);

        GameObject childPlantGameObject = Instantiate(referencePlant.gameObject, newPositionVector3, randomRotation);
        childPlantGameObject.transform.parent = this.gameObject.transform;
        this.plants.Add(childPlantGameObject.GetComponent<Plant>());
    }
}
