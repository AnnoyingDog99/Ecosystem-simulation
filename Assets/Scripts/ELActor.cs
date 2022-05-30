using UnityEngine;
using UnityEngine.AI;

public class ELActor : MonoBehaviour
{
    [SerializeField] protected Collider collider;
    [SerializeField] protected Rigidbody body;
    [SerializeField] protected NavMeshAgent agent;
    
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
        InvokeRepeating("handleLifeTime", 1, 1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // this.HandleMoveTowardsPosition();
        // this.HandleRotation();
        // this.HandleGravity();
        // this.HandleVerticalForce();
    }

    private void handleLifeTime()
    {
        lifeTime.addSecond();
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
        agent.speed = speed;
        agent.SetDestination(position);
    }

    protected void Teleport(Vector3 position)
    {
        transform.position = position;
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
