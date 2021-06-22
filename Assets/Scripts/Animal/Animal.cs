using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;

    private Vector3 direction;

    private CharacterController _characterController;
    private Animator _animator;

    private int _isWalkingHash, _isRunningHash;

    private bool _isWalking, _isRunning;

    private Vector3 _currentMovement, _positionToLookAt = new Vector3(0, 0, 0);

    private float rotationFactorPerFrame = 1.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _characterController = GetComponentInChildren<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _isWalking = _animator.GetBool("isWalking");
        _isRunning = _animator.GetBool("isRunning");

        this.handleRotation();
        this.handleGravity();

        _characterController.Move(_currentMovement * Time.deltaTime);
    }

    /// <summary>
    /// Rotates Animal based on where it would be facing given the current movement.
    /// </summary>
    private void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
    }

    /// <summary>
    /// Simulates gravity, causing the animal to fall to the surface.
    /// </summary>
    private void handleGravity()
    {
        if (_characterController.isGrounded)
        {
            float groundedGravity = -0.05f;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.80665f;
            _currentMovement.y += gravity;
        }
    }

    /// <summary>
    /// Sit still and face a given direction.
    /// </summary>
    /// <param name="direction"></param>    
    protected void Idle(Vector3 direction)
    {
        _animator.SetBool(_isWalkingHash, false);
        _animator.SetBool(_isRunningHash, false);

        this.Move(direction, _characterController.minMoveDistance/1.1f);
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
    private void Move(Vector3 movement, float speed)
    {
        // Allow a directional movement in a range of -1.0f to 1.0f
        movement.Normalize();
        _currentMovement = (movement * speed);
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

        this.Move(movement, walkingSpeed);
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

        this.Move(movement, runningSpeed);
    }
}
