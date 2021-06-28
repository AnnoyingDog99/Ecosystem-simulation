using System.Collections.Generic;
using UnityEngine;

public class Bunny : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.Jump();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        List<Transform> visibleTargets = sight.GetVisibleTargets();
        foreach(Transform visibleTarget in visibleTargets)
        {
            // Ignore self
            if (visibleTarget.transform == transform) continue;
            switch(visibleTarget.tag)
            {
                case "Fox":
                    ReactToPredator(visibleTarget.transform.gameObject.GetComponent<Animal>());
                    break;
            }
        }
    }

    private void ReactToPredator(Animal predator)
    {
        Debug.Log("Reacting to predator");
        base.WalkTo(predator.GetPosition());
    }
}
