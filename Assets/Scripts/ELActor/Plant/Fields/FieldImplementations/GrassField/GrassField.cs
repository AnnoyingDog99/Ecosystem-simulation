using UnityEngine;
public class GrassField : Field
{
    protected override void Update()
    {
        base.Update();
        if (this.GetPlants().Count >= this.GetMaxAmountOfPlants() || this.GetPlants().Count >= this.points.Count) return;
        this.spreadTimer += Time.deltaTime;
        if (this.spreadTimer >= this.GetSpreadTime())
        {
            this.Spread();
            this.spreadTimer = 0;
        }
    }
}