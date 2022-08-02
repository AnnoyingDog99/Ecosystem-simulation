using UnityEngine;
using UnityEngine.AI;

public class LandAnimalMovementController : Controller
{
    [SerializeField] private StaminaTracker staminaTracker;

    private ILandAnimal landAnimal;
    private ILandAnimalMovementState landMovementState;

    private void Awake()
    {
    }

    private void Start()
    {
        this.landAnimal = GetComponentInParent<ILandAnimal>();

        this.staminaTracker.GetStatus().Subscribe((StaminaTracker.StaminaStatus status) =>
        {
            switch (status)
            {
                case StaminaTracker.StaminaStatus.ENERGIZED:
                    this.landMovementState = new EnergizedLandAnimalMovementState(this.landAnimal);
                    break;
                case StaminaTracker.StaminaStatus.TIRED:
                    this.landMovementState = new TiredLandAnimalMovementState(this.landAnimal);
                    break;
                case StaminaTracker.StaminaStatus.EXHAUSTED:
                    this.landMovementState = new ExhaustedLandAnimalMovementState(this.landAnimal);
                    break;
            }
        });
    }

    protected override void Update()
    {
        if (this.HasReachedDestination())
        {
            this.Idle();
        }
    }

    public void Idle()
    {
        if (this.IsWalking())
        {
            this.landAnimal.GetLandAnimalAnimator().SetIsWalkingBool(false);
        }
        if (this.IsRunning())
        {
            this.landAnimal.GetLandAnimalAnimator().SetIsRunningBool(false);
        }
        this.landAnimal.GetAgent().ResetPath();
    }

    public bool IsIdle()
    {
        return this.landAnimal.GetLandAnimalAnimator().GetIsWalkingBool()
        && this.landAnimal.GetLandAnimalAnimator().GetIsRunningBool()
        && this.landAnimal.GetLandAnimalAnimator().GetIsEatingBool()
        && this.landAnimal.GetLandAnimalAnimator().GetIsDeadBool();
    }

    public bool WalkTo(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (!this.CalculatePath(position, out path))
        {
            // Position is unreachable
            this.Idle();
            return false;
        }
        return this.WalkTo(path);
    }

    public bool WalkTo(NavMeshPath path)
    {
        return this.landMovementState.WalkTo(path);
    }

    public bool IsWalking()
    {
        return this.landAnimal.GetLandAnimalAnimator().GetIsWalkingBool();
    }

    public bool RunTo(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (!this.CalculatePath(position, out path))
        {
            // Position is unreachable
            this.Idle();
            return false;
        }
        return this.RunTo(path);
    }

    public bool RunTo(NavMeshPath path)
    {
        return this.landMovementState.RunTo(path);
    }

    public bool IsRunning()
    {
        return this.landAnimal.GetLandAnimalAnimator().GetIsRunningBool();
    }

    public bool CalculatePath(Vector3 position, out NavMeshPath path)
    {
        path = new NavMeshPath();
        if (this.landAnimal.GetAgent().CalculatePath(position, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            return true;
        }
        return false;
    }

    public bool HasReachedDestination()
    {
        if (this.landAnimal.GetAgent().pathPending)
        {
            return false;
        }
        if (this.landAnimal.GetAgent().hasPath)
        {
            // Ignore y-coördinate
            if (this.landAnimal.GetAgent().remainingDistance > Mathf.Abs(this.landAnimal.GetAgent().pathEndPosition.y - this.landAnimal.GetPosition().y))
            {
                return false;
            }
            if (this.landAnimal.GetAgent().velocity.sqrMagnitude != 0f)
            {
                return false;
            }
        }
        return true;
    }

    public StaminaTracker GetStaminaTracker()
    {
        return this.staminaTracker;
    }
}