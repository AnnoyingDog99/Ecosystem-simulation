using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [SerializeField] protected MainCamera mainCamera;
    [SerializeField] protected float moveVelocity = 1.5f;
    [SerializeField] protected float boostVelocity = 3.0f;
    [SerializeField] protected float maxBoostVelocity = 12.0f;
    [SerializeField] protected float rotateAccelerationX = 1.0f;
    [SerializeField] protected float rotateAccelerationY = 1.0f;
    [SerializeField] protected float mouseSmoothTime = 0f;

    float velocity;

    private float rotationAxisY;
    private float rotationAxisX;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        this.rotationAxisY = angles.x;
        this.rotationAxisX = angles.y;

        this.velocity = this.moveVelocity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mainCamera.getCurrentTarget() != null)
        {
            return;
        }

        Vector3 direction = Vector3.zero;

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
            this.velocity = Mathf.Min(this.maxBoostVelocity, Mathf.Max(this.boostVelocity, this.velocity + (10 * Time.deltaTime)));
        }
        else
        {
            this.velocity = this.moveVelocity;
        }

        Vector3 position = this.transform.position + (direction * this.velocity * Time.deltaTime);

        float mouseVelocityX = 0;
        float mouseVelocityY = 0;
        if (Input.GetMouseButton(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            mouseVelocityX = mouseVelocityX + ((Input.GetAxis("Mouse X") * this.rotateAccelerationX) * Time.deltaTime * 10);
            mouseVelocityY = mouseVelocityY + ((Input.GetAxis("Mouse Y") * this.rotateAccelerationY) * Time.deltaTime * 10);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        this.rotationAxisX += mouseVelocityX;
        this.rotationAxisY -= mouseVelocityY;

        this.rotationAxisY = Mathf.Clamp(this.rotationAxisY, -60f, 90f);

        this.transform.position = position;
        this.transform.eulerAngles = new Vector3(this.rotationAxisY, this.rotationAxisX, 0f);
    }
}
