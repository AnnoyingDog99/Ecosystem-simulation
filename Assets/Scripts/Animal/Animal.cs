using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : ELActor
{
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float runningSpeed = 2f;
    [SerializeField] private GameObject offspringGameObject = null;
    [SerializeField] private float babyScale = 0.5f;
    [SerializeField] private float adultScale = 1f;
    [SerializeField] protected Sex sex = Sex.F;
    [SerializeField] protected uint age = 0;
    [SerializeField] protected uint maxAge = 100;

    private Animator _animator;

    private int _isWalkingHash, _isRunningHash, _isDeadHash;

    private bool _isWalking, _isRunning, _isDead;

    protected List<Animal> offspring = new List<Animal>();

    protected Animal mother;

    protected internal enum Sex { M,F }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isDeadHash = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _isWalking = _animator.GetBool("isWalking");
        _isRunning = _animator.GetBool("isRunning");
        _isDead = _animator.GetBool("isDead");
    }

    protected void Die()
    {
        _animator.SetBool("isDead", true);
    }

    /// <summary>
    /// Walk towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to walk to, give Vector will be normalized.
    /// </param>
    protected void Walk(Vector3 movement)
    {
        if (!_isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        if (_isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }

        base.Move(movement, walkingSpeed);
        base.Look(movement);
    }

    /// <summary>
    /// Run towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to run to, give Vector will be normalized.
    /// </param>
    protected void Run(Vector3 movement)
    {
        if (_isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (!_isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        base.Move(movement, runningSpeed);
        base.Look(movement);
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
}
