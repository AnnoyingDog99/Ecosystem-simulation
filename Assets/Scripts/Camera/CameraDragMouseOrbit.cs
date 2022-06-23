using UnityEngine;
using System.Collections;

public class CameraDragMouseOrbit : MonoBehaviour
{
    [SerializeField] MainCamera mainCamera;
    [SerializeField] KeyCode exitOrbitKey = KeyCode.Escape;
    public float mouseVelocityX = 1.0f;
    public float mouseVelocityY = 1.0f;
    public float constantMinDistance = 1f;
    public float constantMaxDistance = 2f;
    public float smoothTime = 2f;
    float rotationAxisX = 0.0f;
    float rotationAxisY = 0.0f;

    private float currentMouseVelocityX = 0.0f;
    private float currentMouseVelocityY = 0.0f;

    private float distance = 2.0f;
    private float minRotationY = -90f;
    private float maxRotationY = 90f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationAxisX = angles.y;
        rotationAxisY = angles.x;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (mainCamera.getCurrentTarget() == null)
        {
            return;
        }

        if (Input.GetKey(this.exitOrbitKey))
        {
            this.mainCamera.unsetTarget();
            return;
        }

        Transform targetTransform = mainCamera.getCurrentTarget().transform;
        
        // FIXME: Remove Hold down requirement before building
        if (Input.GetMouseButton(0))
        {
            this.currentMouseVelocityX += this.mouseVelocityX * Input.GetAxis("Mouse X") * this.distance * 0.02f;
            this.currentMouseVelocityY += this.mouseVelocityY * Input.GetAxis("Mouse Y") * 0.02f;
        }
        this.rotationAxisX += this.currentMouseVelocityX;
        this.rotationAxisY -= this.currentMouseVelocityY;
        // this.rotationAxisY = ClampAngle(rotationAxisY, this.minRotationY, this.maxRotationY);
        this.rotationAxisY = Mathf.Clamp(this.rotationAxisY, this.minRotationY, this.maxRotationY);

        Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        Quaternion toRotation = Quaternion.Euler(rotationAxisY, rotationAxisX, 0);
        Quaternion rotation = toRotation;

        float actorScaleDistance = Vector3.Distance(new Vector3(0, 0, 0), mainCamera.getCurrentTarget().GetScale());
        this.distance = Mathf.Clamp(this.distance - Input.GetAxis("Mouse ScrollWheel") * 15, actorScaleDistance + this.constantMinDistance, actorScaleDistance + this.constantMaxDistance);
        RaycastHit hit;
        if (Physics.Linecast(targetTransform.position, transform.position, out hit))
        {
            this.distance -= hit.distance;
        }
        Vector3 negativeDistance = new Vector3(0.0f, 0.0f, -this.distance);
        Vector3 position = rotation * negativeDistance + targetTransform.position;

        transform.rotation = rotation;
        transform.position = position;

        // Reduce mouse velocity over time
        this.currentMouseVelocityX = Mathf.Lerp(this.currentMouseVelocityX, 0, Time.deltaTime * smoothTime);
        this.currentMouseVelocityY = Mathf.Lerp(this.currentMouseVelocityY, 0, Time.deltaTime * smoothTime);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}