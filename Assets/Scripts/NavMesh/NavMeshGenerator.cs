using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private float delay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // surface.BuildNavMesh();
        // StartCoroutine(RebuildNavMeshWithDelay(this.delay));
    }

    IEnumerator RebuildNavMeshWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            // surface.BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
