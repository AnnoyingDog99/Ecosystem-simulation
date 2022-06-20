using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera _camera;

    private ELActor currentTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        this._camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            ELActor target = hit.transform.gameObject.GetComponent<ELActor>();
            if (target == null) return;
            this.currentTarget = target;
        }
    }

    public void setTarget(ELActor target)
    {
        this.currentTarget = target;
    }

    public ELActor getCurrentTarget()
    {
        return this.currentTarget;
    }
}
