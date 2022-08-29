using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public abstract class ELActor : MonoBehaviour, IELActor, IScalable
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private BoxCollider boundingBox;
    [SerializeField] private ELActorAnimator animator;
    [SerializeField] private ScaleModel scaleModel;
    private ELActorMovementController _actorMovementController;
    private ELActorScaleController _actorScaleController;
    [SerializeField] private List<ELActor> _collidingActors = new List<ELActor>();

    private Observable<ELActorInitializationState> actorInitializationState = new Observable<ELActorInitializationState>(ELActorInitializationState.NONE);

    private bool firstUpdate = true;

    protected virtual void Awake()
    {
        this.actorInitializationState.Set(ELActorInitializationState.AWAKE);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this._actorMovementController = GetComponent<ELActorMovementController>();
        this._actorScaleController = GetComponent<ELActorScaleController>();

        this.actorInitializationState.Set(ELActorInitializationState.STARTED);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.firstUpdate)
        {
            this.FirstUpdate();
            this.firstUpdate = false;
        }
        for (int i = this._collidingActors.Count - 1; i >= 0; i--)
        {
            if (!Director.Instance.ActorExists(this._collidingActors[i]))
            {
                this._collidingActors.RemoveAt(i);
            }
        }
    }

    protected virtual void FirstUpdate()
    {
        // Place ELActor on NavMesh
        if (!this._actorMovementController.WarpTo(this.GetPosition()))
        {
            Debug.LogWarning("Failed to place Agent of ELActor on NavMesh");
        }
        this.actorInitializationState.Set(ELActorInitializationState.UPDATING);
    }

    private void OnTriggerEnter(Collider collider)
    {
        ELActor actor = collider.gameObject.GetComponent<ELActor>();
        if (actor != null)
        {
            if (!this._collidingActors.Contains(actor))
            {
                this._collidingActors.Add(actor);
            }
        }

    }

    private void OnTriggerExit(Collider collider)
    {
        ELActor actor = collider.gameObject.GetComponent<ELActor>();
        if (actor != null)
        {
            if (this._collidingActors.Contains(actor))
            {
                this._collidingActors.Remove(actor);
            }
        }
    }

    public NavMeshAgent GetAgent()
    {
        return this.agent;
    }

    public BoxCollider GetBoundingBox()
    {
        return this.boundingBox;
    }

    public ELActorAnimator GetActorAnimator()
    {
        return this.animator;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public Quaternion GetRotation()
    {
        return gameObject.transform.rotation;
    }
    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    public Vector3 GetScale()
    {
        return this.transform.lossyScale;
    }
    public void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    public int GetID()
    {
        return gameObject.GetInstanceID();
    }

    public int GetLayer()
    {
        return this.gameObject.layer;
    }

    public string GetTag()
    {
        return this.tag;
    }

    public List<ELActor> GetCollidingActors()
    {
        this._collidingActors = this._collidingActors.FindAll((actor) => Director.Instance.ActorExists(actor));
        return this._collidingActors;
    }

    public bool DoActorsCollide(IELActor actor)
    {
        return this.GetCollidingActors().Find((_actor) => _actor.GetID() == actor.GetID()) != null;
    }

    public ELActorMovementController GetActorMovementController()
    {
        return this._actorMovementController;
    }

    public ELActorScaleController GetActorScaleController()
    {
        return this._actorScaleController;
    }

    public Vector3 GetMinScale()
    {
        return this.scaleModel.GetMinScale();
    }

    public Vector3 GetMaxScale()
    {
        return this.scaleModel.GetMaxScale();
    }

    public Observable<ELActorInitializationState> GetActorInitializationState()
    {
        return this.actorInitializationState;
    }
}

public enum ELActorInitializationState
{
    NONE = 0,
    AWAKE = 1,
    STARTED = 2,
    UPDATING = 3
}
