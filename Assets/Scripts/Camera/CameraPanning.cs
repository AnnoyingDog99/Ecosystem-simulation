using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    [SerializeField] private MainCamera mainCamera;
    [SerializeField] private float rotateTime = 5f;

    private float rotateTimer = 0f;

    private float direction = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rotateTimer >= this.rotateTime)
        {
            this.direction *= -1;
            this.rotateTimer = 0f;
        }

        float smoothOut = this.CalculateSmoothOut();

        this.mainCamera.transform.Rotate(((Vector3.up * this.direction) * smoothOut) * Time.deltaTime);
        this.rotateTimer += Time.deltaTime;
    }

    private float CalculateSmoothOut()
    {
        if (this.rotateTimer > this.rotateTime / 2)
        {
            return this.rotateTime - this.rotateTimer;
        }
        return (this.rotateTime / 2) - ((this.rotateTime / 2) - this.rotateTimer);
    }
}
