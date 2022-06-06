using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : ELActor
{
    [SerializeField] protected BehaviourTree behaviourTree;
    [SerializeField] protected ELActorMemory memory;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float rotationSpeed = 180f;
    [SerializeField] protected float acceleration = 1f;

    [SerializeField] protected float walkSpeed = 1;
    [SerializeField] protected float runSpeed = 2;
    [SerializeField] private GameObject offspringGameObject = null;
    [SerializeField] private float babyScale = 0.5f;
    [SerializeField] private float adultScale = 1f;
    [SerializeField] protected Sex sex = Sex.F;
    [SerializeField] protected uint age = 0;
    [SerializeField] protected uint maxAge = 100;
    [SerializeField] protected List<string> predatorTags;

    private Animator _animator;

    private int _isWalkingHash, _isRunningHash, _isDeadHash, _isAirbornHash;

    protected bool isWalking, isRunning, isDead, isAirborn;

    protected List<Animal> offspring = new List<Animal>();

    protected Animal mother;

    protected internal enum Sex { M, F }

    private BasicSight _sight;

    private ELActorMemory _memory;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        agent.angularSpeed = rotationSpeed;
        agent.acceleration = acceleration;

        _animator = GetComponentInChildren<Animator>();
        _sight = GetComponent<BasicSight>();
        _memory = GetComponent<ELActorMemory>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isDeadHash = Animator.StringToHash("isDead");
        _isAirbornHash = Animator.StringToHash("isAirborn");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isDead) return;
        isWalking = _animator.GetBool(_isWalkingHash);
        isRunning = _animator.GetBool(_isRunningHash);
        isDead = _animator.GetBool(_isDeadHash);
        isAirborn = _animator.GetBool(_isAirbornHash);

        if (this.ReachedDestination()) {
            Idle();
        }
    }

    public void Die()
    {
        _animator.SetBool(_isDeadHash, true);
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

    public void Idle()
    {
        if (isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }

        this.MoveTo(GetPosition(), 0);
    }

    /// <summary>
    /// Walk towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to walk to, give Vector will be normalized.
    /// </param>
    public void WalkTo(Vector3 position)
    {
        if (!isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        if (isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }

        this.MoveTo(position, walkSpeed);
    }

    /// <summary>
    /// Run towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to run to, give Vector will be normalized.
    /// </param>
    public void RunTo(Vector3 position)
    {
        if (isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (!isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        this.MoveTo(position, runSpeed);
    }

    public bool ReachedDestination() {
        return agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0;
    }

    public List<Animal> getOffspring()
    {
        return offspring;
    }

    /// <summary>
    /// Instantiates offspring which this instance will be the parent of.
    /// Animal has to be of the Female biological sex.
    /// </summary>
    public void instantiateOffspring()
    {
        if (this.sex != Sex.F) return;
        Animal newOffspring = Instantiate(offspringGameObject, transform.position, transform.rotation).GetComponent<Animal>();
        newOffspring.SetScale(new Vector3(this.babyScale, this.babyScale, this.babyScale));
        newOffspring.SetMother(this);
        offspring.Add(newOffspring);
    }

    public void SetMother(Animal mother)
    {
        this.mother = mother;
    }

    public BasicSight GetSight()
    {
        return _sight;
    }

    public ELActorMemory GetMemory()
    {
        return _memory;
    }

    public List<string> GetPredatorTags() {
        return predatorTags;
    }
}
