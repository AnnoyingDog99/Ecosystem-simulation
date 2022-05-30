using System.Collections.Generic;
using UnityEngine;

public class Animal : ELActor
{
    [SerializeField] protected float walkSpeed = 1;
    [SerializeField] protected float runSpeed = 2;
    [SerializeField] private GameObject offspringGameObject = null;
    [SerializeField] private float babyScale = 0.5f;
    [SerializeField] private float adultScale = 1f;
    [SerializeField] protected Sex sex = Sex.F;
    [SerializeField] protected uint age = 0;
    [SerializeField] protected uint maxAge = 100;
    [SerializeField] private float viewRadius = 360;
    [SerializeField] private float viewAngle = 90;

    protected BasicSight sight;

    private Animator _animator;

    private int _isWalkingHash, _isRunningHash, _isDeadHash, _isAirbornHash;

    protected bool isWalking, isRunning, isDead, isAirborn;

    protected List<Animal> offspring = new List<Animal>();

    protected Animal mother;

    protected internal enum Sex { M, F }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
        sight = GetComponent<BasicSight>();
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
    }

    protected void Die()
    {
        _animator.SetBool("isDead", true);
    }

    protected void Idle()
    {
        if (isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }

        base.MoveTo(GetPosition(), 0);
    }

    /// <summary>
    /// Walk towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to walk to, give Vector will be normalized.
    /// </param>
    protected void WalkTo(Vector3 position)
    {
        if (!isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        if (isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }

        base.MoveTo(position, walkSpeed);
    }

    /// <summary>
    /// Run towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to run to, give Vector will be normalized.
    /// </param>
    protected void RunTo(Vector3 position)
    {
        if (isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (!isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        base.MoveTo(position, runSpeed);
    }

    public List<Animal> getOffspring()
    {
        return offspring;
    }

    /// <summary>
    /// Instantiates offspring which this instance will be the parent of.
    /// Animal has to be of the Female biological sex.
    /// </summary>
    protected void instantiateOffspring()
    {
        if (this.sex != Sex.F) return;
        Animal newOffspring = Instantiate(offspringGameObject, transform.position, transform.rotation).GetComponent<Animal>();
        newOffspring.SetScale(babyScale);
        newOffspring.SetMother(this);
        offspring.Add(newOffspring);
    }

    public void SetMother(Animal mother)
    {
        this.mother = mother;
    }

    public BasicSight GetSight()
    {
        return sight;
    }
}
