using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [SerializeField] protected MainCamera mainCamera;
    [SerializeField] protected float moveVelocity = 1.0f;
    [SerializeField] protected float boostVelocity = 2.0f;
    [SerializeField] protected float rotateAccelerationX = 1.0f;
    [SerializeField] protected float rotateAccelerationY = 1.0f;
    [SerializeField] protected float maxRotateVelocityX = 0.1f;
    [SerializeField] protected float maxRotateVelocityY = 0.1f;
    [SerializeField] protected float mouseSmoothTime = 0f;

    private float rotationAxisY;
    private float rotationAxisX;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        this.rotationAxisY = angles.x;
        this.rotationAxisX = angles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mainCamera.getCurrentTarget() != null)
        {
            return;
        }

        Vector3 direction = Vector3.zero;
        float velocity = this.moveVelocity;

        if (Input.GetKey(KeyCode.W))
        {
            direction += this.mainCamera.transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction -= this.mainCamera.transform.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction -= this.mainCamera.transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += this.mainCamera.transform.right;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            direction += Vector3.up;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            direction += Vector3.down;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = this.boostVelocity;
        }

        Vector3 position = this.transform.position + (direction * velocity * Time.deltaTime);

        float mouseVelocityX = 0;
        float mouseVelocityY = 0;
        // FIXME: Remove Hold down requirement before building
        if (Input.GetMouseButton(0))
        {
            mouseVelocityX = Mathf.Min(mouseVelocityX + Input.GetAxis("Mouse X") * this.rotateAccelerationX, this.maxRotateVelocityX);
            mouseVelocityY = Mathf.Min(mouseVelocityY + Input.GetAxis("Mouse Y") * this.rotateAccelerationY, this.maxRotateVelocityY);
        }

        this.rotationAxisX += mouseVelocityX;
        this.rotationAxisY -= mouseVelocityY;

        this.rotationAxisY = Mathf.Clamp(this.rotationAxisY, -60f, 90f);
        // rotationXAxis = ClampAngle(rotationXAxis, -90.0f, 90.0f);
        // Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        // Quaternion toRotation = Quaternion.Euler(this.rotationAxisY, this.rotationAxisX, 0);
        // Quaternion rotation = toRotation;

        this.transform.position = position;
        this.transform.eulerAngles = new Vector3(this.rotationAxisY, this.rotationAxisX, 0f);


        // this.mouseVelocityX = Mathf.Lerp(this.mouseVelocityX, 0, Time.deltaTime * this.mouseSmoothTime);
        // this.mouseVelocityY = Mathf.Lerp(this.mouseVelocityY, 0, Time.deltaTime * this.mouseSmoothTime);
    }
}
