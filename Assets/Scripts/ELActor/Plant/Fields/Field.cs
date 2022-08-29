using UnityEngine;
using System.Collections.Generic;
public class Field : ELActor, IField
{
    [SerializeField] private Plant referencePlant;
    [SerializeField] private int maxAmountOfPlants;
    [SerializeField] private Vector2 size;
    [SerializeField] private float spreadTime;

    protected float spreadTimer;

    private List<Plant> plants = new List<Plant>();
    protected List<Vector2> points = new List<Vector2>();

    protected override void Start()
    {
        base.Start();
        this.spreadTimer = 0;

        float avgPointRadius = (referencePlant.GetMaxScale().x + referencePlant.GetMaxScale().y) / 2;
        this.points = PoissonDiscSampling.GeneratePoints(avgPointRadius, this.GetSize(), maxAmountOfPlants, 20);
        for (int i = 0; i < this.points.Count; i++)
        {
            this.points[i] += new Vector2(this.GetPosition().x - (this.GetSize().x / 2), this.GetPosition().z - (this.GetSize().y / 2));
        }
        if (this.points.Count <= 0)
        {
            Debug.LogError("Field does not have enough room to initialize");
            return;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual void Spread()
    {
        // Translate Vector2 to Vector3, the y position is determined at the start of the ELACtor so an estimation is enough.
        Vector2 newPositionVector2 = this.points[this.plants.Count];
        Vector3 newPositionVector3 = new Vector3(newPositionVector2.x, transform.position.y, newPositionVector2.y);

        // Random rotation, just because
        float randomNumber = Random.Range(-1, 1);
        Quaternion randomRotation = new Quaternion(randomNumber, 0, randomNumber, 1);

        ELActor plant = Director.Instance.SpawnActor(ActorFactory.ActorType.GRASS, newPositionVector3, randomRotation);
        // GameObject childPlantGameObject = Instantiate(referencePlant.gameObject, newPositionVector3, randomRotation);
        plant.transform.parent = this.gameObject.transform;
        this.plants.Add(plant as Plant);

    }

    public Plant GetReferencePlant()
    {
        return this.referencePlant;
    }

    public List<Plant> GetPlants()
    {
        return this.plants;
    }

    public int GetMaxAmountOfPlants()
    {
        return this.maxAmountOfPlants;
    }

    public Vector2 GetSize()
    {
        return this.size;
    }

    public float GetSpreadTime()
    {
        return this.spreadTime;
    }
}