using System.Collections;
using UnityEngine;

public class ELActor : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector3 _currentMovement, _positionToLookAt = new Vector3(0, 0, 0);

    private float rotationFactorPerFrame = 1.0f;

    private float fallingSpeed = 0;

    private float scale = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _characterController = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.handleRotation();
        this.handleGravity();
        _characterController.Move(_currentMovement * Time.deltaTime);
    }

    /// <summary>
    /// Rotates Actor based on where it would be facing given the current movement.
    /// </summary>
    private void handleRotation()
    {
        if (_currentMovement == Vector3.zero) return;

        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(this._positionToLookAt);
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
            fallingSpeed = 0;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.80665f;
            fallingSpeed += (gravity * Time.deltaTime);
            _currentMovement.y += fallingSpeed;
        }
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
    protected void Move(Vector3 movement, float speed)
    {
        // Allow directional movement in a range of -1.0f to 1.0f
        this._currentMovement = (movement.normalized * speed);
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
}
