using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class ELActor : MonoBehaviour
{
    [SerializeField] protected float gravity = -9.80665f;
    private CharacterController _characterController;
    
    protected bool isGrounded = true;

    protected Vector3 velocity = new Vector3(0, 0, 0);
    private Vector3 _positionToLookAt = new Vector3(0, 0, 0);
    private float _currentMoveSpeed, _currentVerticalForce = 0;
    protected float targetReachedOffsetMagnitude = .1f;
    protected Vector3 currentTargetPosition = new Vector3(0, 0, 0);

    // Speed at which an Actor can rotate where 4.0f equals a full 360 degrees rotation within one second.
    // A negative value will default to an instant rotation.
    [SerializeField] private float rotationFactorPerSecond = -1f;

    private float fallingSpeed = 0;

    private float scale = 1;

    protected LifeTime lifeTime = new LifeTime();
    protected internal class LifeTime
    {
        public int seconds = 0;
        public int minutes = 0;
        public int hours = 0;

        public void addSecond()
        {
            if (seconds < 59)
            {
                seconds += 1;
                return;
            }
            seconds = 0;
            if (minutes < 59)
            {
                minutes += 1;
                return;
            }
            minutes = 0;
            hours += 1;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _characterController = GetComponentInChildren<CharacterController>();
        InvokeRepeating("handleLifeTime", 1, 1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.HandleMoveTowardsPosition();
        this.HandleRotation();
        this.HandleGravity();
        this.HandleVerticalForce();
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleMoveTowardsPosition()
    {
        Vector3 offset = this.currentTargetPosition - transform.position;
        if (offset.magnitude > targetReachedOffsetMagnitude)
        {
            // Further away than .1f
            // Move towards target
            offset = offset.normalized * this._currentMoveSpeed;
            this.velocity = offset;
        }
    }

    private void HandleVerticalForce()
    {
        this.velocity.y += _currentVerticalForce;
    }

    /// <summary>
    /// Rotates Actor based on where it would be facing given the current movement.
    /// </summary>
    private void HandleRotation()
    {
        if (_positionToLookAt == Vector3.zero) return;

        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(this._positionToLookAt);
        float rotationFactor = rotationFactorPerSecond < 0 ? 1 : (rotationFactorPerSecond * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor);
    }

    /// <summary>
    /// Simulates gravity, causing the animal to fall to the surface.
    /// </summary>
    private void HandleGravity()
    {
        float groundedGravity = 1;
        if (_characterController.isGrounded)
        {
            fallingSpeed = 0;
            velocity.y = -groundedGravity;
        }
        else
        {
            fallingSpeed += (gravity * Time.deltaTime);
            velocity.y += fallingSpeed;
        }
        isGrounded = velocity.y <= groundedGravity && velocity.y >= -groundedGravity;
    }

    private void handleLifeTime()
    {
        lifeTime.addSecond();
    }

    /// <summary>
    /// Look towards a certain direction
    /// </summary>
    /// <param name="direction">
    /// Vector will be normalized to fit within a range of -1.0f to 1.0f.
    /// </param>    
    protected void Look(Vector3 direction)
    {
        // Allow directional looking in a range of -1.0f to 1.0f
        this._positionToLookAt = direction.normalized;
    }

    /// <summary>
    /// Move towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Vector will be normalized to fit within a range of -1.0f to 1.0f.
    /// </param>
    /// <param name="speed">
    /// The movement speed.
    /// </param>
    protected void MoveTo(Vector3 position, float speed)
    {
        this.currentTargetPosition = position;
        this._currentMoveSpeed = speed;
        // Allow directional movement in a range of -1.0f to 1.0f
        this.velocity = (this.currentTargetPosition.normalized * speed);
    }

    protected void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    protected void ApplyVerticalForce(float force)
    {
        _currentVerticalForce = force;
    }

    /// <summary>
    /// Scale Actor.
    /// </summary>
    /// <param name="newScale">
    /// The new scale of the actor
    /// 1 == default.
    /// < 1 == smaller than default.
    /// > 1 == larger than default.
    /// newScale can not be lower than 0
    /// </param>
    protected void SetScale(float newScale)
    {
        scale = newScale < 0 ? 0 : newScale;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    protected float GetScale()
    {
        return scale;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
