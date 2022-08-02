using UnityEngine;
public abstract class Controller : MonoBehaviour
{
    private bool firstUpdate = true;

    protected virtual void Update()
    {
        if (this.firstUpdate)
        {
            this.FirstUpdate();
            this.firstUpdate = false;
        }
    }

    protected virtual void FirstUpdate()
    {
    }
}