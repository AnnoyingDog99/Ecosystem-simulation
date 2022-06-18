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
    [SerializeField] protected AgeBar ageBar;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float rotationSpeed = 180f;
    [SerializeField] protected float acceleration = 1f;
    [SerializeField] protected float walkSpeed = 1;
    [SerializeField] protected float runSpeed = 2;
    [SerializeField] protected Sex sex = Sex.F;
    [SerializeField] protected List<string> predatorTags = new List<string>();
    [SerializeField] protected uint biteSize = 1;
    [SerializeField] uint startAge = 0;

    private Animator _animator;

    private int _isIdleHash, _isWalkingHash, _isRunningHash, _isDeadHash, _isAirbornHash, _isEatingHash;

    public bool isIdle { get; protected set; }
    public bool isWalking { get; protected set; }
    public bool isRunning { get; protected set; }
    public bool isAirborn { get; protected set; }
    public bool isEating { get; protected set; }

    protected List<Animal> offspring = new List<Animal>();
    protected Animal mother;
    protected Animal father;
    protected Animal partner;

    public enum Sex { M, F }

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

        this.lifeTime.minutes += (int)this.startAge;

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
        if (this.GetAgeBar().GetAgePercentage(this.GetAgeBar().GetMaxAge()) >= 100 || this.GetHungerBar().GetHungerPercentage() <= 0)
        {
            this.Die();
        }

        this.GetAgeBar().SetAge((uint)(
            // (this.lifeTime.seconds + (60 * this.lifeTime.minutes) + (3600 * this.lifeTime.hours))  // Age up every second
            (this.lifeTime.minutes) + (60 * this.lifeTime.hours) // Age up every minute
            // (this.lifeTime.hours) */ // Age up every hour
        ));

        Vector3 newScale = (this.maxScale * GetAgeBar().GetAgePercentage(this.GetAgeBar().GetAdultAge()) / 100);
        if (Vector3.Distance(this.minScale, this.maxScale) < Vector3.Distance(newScale, this.maxScale))
        {
            newScale = this.minScale;
        }
        SetScale(newScale);

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
        if (this.agent.hasPath)
        {
            // Ignore y-coördinate
            if (this.agent.remainingDistance > Mathf.Abs(this.agent.pathEndPosition.y - this.GetPosition().y))
            {
                return false;
            }
            if (this.agent.velocity.sqrMagnitude != 0f)
            {
                return false;
            }
        }
        return true;
    }

    public Sex GetSex()
    {
        return this.sex;
    }

    public List<Animal> GetOffspring()
    {
        return offspring;
    }

    public void AddOffspring(Animal offspring)
    {
        this.offspring.Add(offspring);
    }

    public uint GetAge()
    {
        return (uint)Mathf.RoundToInt(this.GetAgeBar().GetCurrent());
    }

    public uint GetMaxAge()
    {
        return this.GetAgeBar().GetMaxAge();
    }

    /// <summary>
    /// Instantiates offspring which this instance will be the parent of.
    /// Animal has to be of the Female biological sex.
    /// </summary>
    public Animal instantiateOffspring()
    {
        if (this.sex != Sex.F) return null;
        Animal newOffspring = Instantiate(this.ownKindGameObject, transform.position, transform.rotation).GetComponent<Animal>();
        newOffspring.SetScale(this.minScale);
        newOffspring.SetMother(this);
        newOffspring.startAge = 0;
        offspring.Add(newOffspring);
        return newOffspring;
    }
    public Animal GetMother()
    {
        return this.mother;
    }

    public void SetMother(Animal mother)
    {
        this.mother = mother;
    }

    public Animal GetFather()
    {
        return this.father;
    }

    public void SetFather(Animal father)
    {
        this.father = father;
    }

    public Animal GetPartner()
    {
        return this.partner;
    }

    public bool IsPotentialPartner(Animal animal)
    {
        return this.sex != animal.sex && this.GetAge() >= this.GetAgeBar().GetAdultAge() && this.GetPartner() == null;
    }

    public void SetPartner(Animal partner)
    {
        this.partner = partner;
    }

    public bool SendMateRequest(Animal animal)
    {
        if (!animal.RetrieveMateRequest(this))
        {
            return false;
        }
        this.SetPartner(animal);
        if (this.sex == Sex.F)
        {
            Animal newOffspring = this.instantiateOffspring();
            newOffspring.father = animal;
        }
        else if (this.sex == Sex.M)
        {
            this.offspring.AddRange(this.partner.offspring);
        }
        return true;
    }

    public bool RetrieveMateRequest(Animal animal)
    {
        if (!animal.IsPotentialPartner(this))
        {
            return false;
        }
        this.SetPartner(animal);
        if (this.sex == Sex.F)
        {
            Animal newOffspring = this.instantiateOffspring();
            newOffspring.father = animal;
        }
        else if (this.sex == Sex.M)
        {
            this.offspring.AddRange(this.partner.offspring);
        }
        return true;
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

    public StaminaBar GetStaminaBar()
    {
        return this.staminaBar;
    }

    public HungerBar GetHungerBar()
    {
        return this.hungerBar;
    }

    public HealthBar GetHealthBar()
    {
        return this.healthBar;
    }

    public AgeBar GetAgeBar()
    {
        return this.ageBar;
    }

    public uint GetBiteSize()
    {
        return this.biteSize;
    }

    public void Attack(Animal animal)
    {
        animal.ReceiveDamage(this.damage);
        // TODO: Animations
    }

    public void ReceiveDamage(float damage)
    {
        this.GetHealthBar().RemoveHealthPoints(damage);
        // TODO: Animations
    }
}
