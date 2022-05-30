using UnityEngine;
using UnityEngine.AI;

public class ELActor : MonoBehaviour
{
    [SerializeField] protected BehaviourTree behaviourTree;
    [SerializeField] protected ELActorMemory memory;
    // [SerializeField] protected Rigidbody body;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float rotationSpeed = 180f;
    [SerializeField] protected float acceleration = 1f;
    
    private float m_scale = 1;
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
        agent.angularSpeed = rotationSpeed;
        agent.acceleration = acceleration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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
    public void MoveTo(Vector3 position, float speed)
    {
        agent.speed = speed;
        agent.SetDestination(position);
    }

    public void Teleport(Vector3 position)
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
        m_scale = newScale < 0 ? 0 : newScale;
        transform.localScale = new Vector3(m_scale, m_scale, m_scale);
    }

    protected float GetScale()
    {
        return m_scale;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public bool ReachedDestination() {
        return agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0;
    }

    public int GetID()
    {
        return gameObject.GetInstanceID();
    }
}
