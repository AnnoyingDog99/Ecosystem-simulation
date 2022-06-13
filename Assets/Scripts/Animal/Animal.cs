using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : ELActor
{
    [SerializeField] protected BehaviourTree behaviourTree;
    [SerializeField] protected AnimalMemory memory;
    [SerializeField] protected Sight sight;
    [SerializeField] protected HungerBar hungerBar;
    [SerializeField] protected StaminaBar staminaBar;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float rotationSpeed = 180f;
    [SerializeField] protected float acceleration = 1f;
    [SerializeField] protected float walkSpeed = 1;
    [SerializeField] protected float runSpeed = 2;
    [SerializeField] private float babyScale = 0.5f;
    [SerializeField] private float adultScale = 1f;
    [SerializeField] protected Sex sex = Sex.F;
    [SerializeField] protected uint age = 0;
    [SerializeField] protected uint maxAge = 100;
    [SerializeField] protected List<string> predatorTags = new List<string>();
    [SerializeField] protected uint biteSize = 1;

    private Animator _animator;

    private int _isIdleHash, _isWalkingHash, _isRunningHash, _isDeadHash, _isAirbornHash, _isEatingHash;

    public bool isIdle { get; protected set; }
    public bool isWalking { get; protected set; }
    public bool isRunning { get; protected set; }
    public bool isAirborn { get; protected set; }
    public bool isEating { get; protected set; }

    protected List<Animal> offspring = new List<Animal>();

    protected Animal mother;

    protected internal enum Sex { M, F }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.agent.angularSpeed = rotationSpeed;
        this.agent.acceleration = acceleration;

        _animator = GetComponentInChildren<Animator>();
        this._isWalkingHash = Animator.StringToHash("isWalking");
        this._isRunningHash = Animator.StringToHash("isRunning");
        this._isDeadHash = Animator.StringToHash("isDead");
        this._isAirbornHash = Animator.StringToHash("isAirborn");
        this._isEatingHash = Animator.StringToHash("isEating");

        this.Idle();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.isDead) return;
        if (this.GetHealthBar().GetHealthPercentage() <= 0)
        {
            this.Die();
        }
        this.behaviourTree.Evaluate();

        this.isWalking = _animator.GetBool(_isWalkingHash);
        this.isRunning = _animator.GetBool(_isRunningHash);
        this.isAirborn = _animator.GetBool(_isAirbornHash);
        this.isIdle = !this.isWalking && !this.isRunning;
        // TODO:
        // this.isEating = _animator.GetBool(_isEatingHash);

        if (this.ReachedDestination())
        {
            this.Idle();
        }
    }

    protected void Die()
    {
        this.isDead = true;
        _animator.SetBool(_isWalkingHash, false);
        _animator.SetBool(_isRunningHash, false);
        _animator.SetBool(_isAirbornHash, false);
        _animator.SetBool(_isIdleHash, false);
        _animator.SetBool(_isEatingHash, false);
        _animator.SetBool(_isDeadHash, true);
    }

    private bool MoveTo(NavMeshPath path, float speed)
    {
        this.agent.speed = speed;
        // return this.agent.SetDestination(position);
        return this.agent.SetPath(path);
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

        this.agent.ResetPath();
    }

    public bool WalkTo(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (!this.IsReachable(position, out path))
        {
            // Position is unreachable
            return false;
        }

        return this.WalkTo(path);
    }

    public bool WalkTo(NavMeshPath path)
    {
        if (this.staminaBar.GetStaminaPercentage() < this.staminaBar.minimumWalkPercentage)
        {
            this.Idle();
            return true;
        }

        if (!isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        if (isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
        if (!this.MoveTo(path, walkSpeed))
        {
            // Failed to reach position
            this.Idle();
            return false;
        }
        return true;
    }

    public bool RunTo(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (!this.IsReachable(position, out path))
        {
            // Position is unreachable
            return false;
        }

        return this.RunTo(path);
    }

    public bool RunTo(NavMeshPath path)
    {
        if (this.staminaBar.GetStaminaPercentage() < this.staminaBar.minimumRunPercentage)
        {
            return this.WalkTo(path);
        }

        if (isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (!isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        if (!this.MoveTo(path, runSpeed))
        {
            // Failed to reach position
            this.Idle();
            return false;
        }
        return true;
    }

    public bool IsReachable(Vector3 position, out NavMeshPath path)
    {
        path = new NavMeshPath();
        if (this.agent.CalculatePath(position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            return true;
        }
        return false;
    }

    public bool ReachedDestination()
    {
        if (this.agent.pathPending)
        {
            return false;
        }
        // Ignore y-coördinate
        if (this.agent.remainingDistance > Mathf.Abs(this.agent.pathEndPosition.y - this.GetPosition().y))
        {
            return false;
        }
        if (this.agent.hasPath && this.agent.velocity.sqrMagnitude != 0f)
        {
            return false;
        }
        return true;
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
        Animal newOffspring = Instantiate(this.ownKindGameObject, transform.position, transform.rotation).GetComponent<Animal>();
        newOffspring.SetScale(new Vector3(this.babyScale, this.babyScale, this.babyScale));
        newOffspring.SetMother(this);
        offspring.Add(newOffspring);
    }

    public void SetMother(Animal mother)
    {
        this.mother = mother;
    }

    public List<string> GetPredatorTags()
    {
        return predatorTags;
    }

    public AnimalMemory GetMemory()
    {
        return this.memory;
    }

    public Sight GetSight()
    {
        return this.sight;
    }

    public HungerBar GetHungerBar()
    {
        return this.hungerBar;
    }

    public HealthBar GetHealthBar()
    {
        return this.healthBar;
    }

    public uint GetBiteSize()
    {
        return this.biteSize;
    }

    public void Attack(Animal animal)
    {
        animal.GetDamaged(this.damage);
        // TODO: Animations
    }

    public void GetDamaged(float damage)
    {
        this.GetHealthBar().RemoveHealthPoints(damage);
        // TODO: Animations
    }
}
