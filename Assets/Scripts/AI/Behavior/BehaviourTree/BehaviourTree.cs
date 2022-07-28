using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BehaviourTree : MonoBehaviour
{
    protected Selector rootNode;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        // rootNode?.Evaluate();
    }

    public void Evaluate()
    {
        rootNode?.Evaluate();
    }
}