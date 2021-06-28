using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : ELActor
{
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float runningSpeed = 2f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private GameObject offspringGameObject = null;
    [SerializeField] private float babyScale = 0.5f;
    [SerializeField] private float adultScale = 1f;
    [SerializeField] protected Sex sex = Sex.F;
    [SerializeField] protected uint age = 0;
    [SerializeField] protected uint maxAge = 100;
    [SerializeField] private float viewRadius = 360;
    [SerializeField] private float viewAngle = 90;

    private Animator _animator;
    protected BasicSight sight;

    private int _isWalkingHash, _isRunningHash, _isDeadHash, _isAirbornHash;

    private bool _isWalking, _isRunning, _isDead, _isAirborn;

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
        _isWalking = _animator.GetBool(_isWalkingHash);
        _isRunning = _animator.GetBool(_isRunningHash);
        _isDead = _animator.GetBool(_isDeadHash);
        _isAirborn = _animator.GetBool(_isAirbornHash);
        this.HandleMoveToTarget();
        this.HandleAirborn();
    }

    /// <summary>
    /// Handles current movement to target destination;
    /// </summary>
    /// <returns>
    /// Returns true if the current target destination has been reached, return false if it hasn't been reached.
    /// </returns>
    protected bool HandleMoveToTarget()
    {
        if ((currentTargetPosition - transform.position).magnitude < targetReachedOffsetMagnitude)
        {
            // Reached target
            Idle();
            return true;
        }
        return false;
    }

    protected void HandleAirborn()
    {
        if (_isAirborn && base.isGrounded)
        {
            _animator.SetBool(_isAirbornHash, false);
            // Reset vertical force once airborn to prevent the same force from being applied once grounded again
            base.ApplyVerticalForce(0);
        }
        else if (!_isAirborn && !base.isGrounded)
        {
            _animator.SetBool(_isAirbornHash, true);
        }

    }

    protected void Die()
    {
        _animator.SetBool("isDead", true);
    }

    protected void Jump()
    {
        base.ApplyVerticalForce(jumpForce);
    }

    protected void Idle()
    {
        if (_isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (_isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
    }

    /// <summary>
    /// Walk towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to walk to, give Vector will be normalized.
    /// </param>
    protected void WalkTo(Vector3 position)
    {
        if (!_isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        if (_isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }

        base.MoveTo(position, walkingSpeed);
        base.Look(position);
    }

    /// <summary>
    /// Run towards a certain direction.
    /// </summary>
    /// <param name="movement">
    /// Direction to run to, give Vector will be normalized.
    /// </param>
    protected void RunTo(Vector3 position)
    {
        if (_isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        if (!_isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        base.MoveTo(position, runningSpeed);
        base.Look(position);
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
