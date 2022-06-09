using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public abstract class ELActor : MonoBehaviour
{
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected GameObject ownKindGameObject = null;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float foodPoints = 10;
    protected LifeTime lifeTime = new LifeTime();
    public bool isDead { get; protected set; } = false;
    private List<ELActor> actorsBeingTouched = new List<ELActor>();
    protected internal class LifeTime
    {
        public int seconds = 0;
        public int minutes = 0;
        public int hours = 0;

        public void addSecond()
        {
            if (seconds < 59)
            {
                seconds += 1;
                return;
            }
            seconds = 0;
            if (minutes < 59)
            {
                minutes += 1;
                return;
            }
            minutes = 0;
            hours += 1;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Place ELActor on NavMesh
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(this.GetPosition(), out closestHit, 50, 1))
        {
            this.agent.Warp(closestHit.position);
        }
        else
        {
            Debug.LogWarning("Failed to place Agent of ELActor on NavMesh");
        }

        InvokeRepeating("handleLifeTime", 1, 1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Remove dead actor
        if (this.isDead)
        {
            StartCoroutine(DestroyAfterDelay(1));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        Destroy(this);
    }


    protected virtual void OnTriggerEnter(Collider collider)
    {
        // TODO: Handle mouse click

        ELActor actor = collider.gameObject.GetComponent<ELActor>();
        if (actor != null)
        {
            if (!this.actorsBeingTouched.Contains(actor))
            {
                this.actorsBeingTouched.Add(actor);
            }
        }
    }

    protected virtual void OnTriggerStay(Collider collider)
    {
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        ELActor actor = collider.gameObject.GetComponent<ELActor>();
        if (actor != null)
        {
            if (this.actorsBeingTouched.Contains(actor))
            {
                this.actorsBeingTouched.Remove(actor);
            }
        }
    }

    private void handleLifeTime()
    {
        lifeTime.addSecond();
    }

    public Vector3 GetLifeTime()
    {
        return new Vector3(this.lifeTime.hours, this.lifeTime.minutes, this.lifeTime.seconds);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    protected void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    public Vector3 GetScale()
    {
        // return boxCollider.transform.localScale;
        return this.transform.lossyScale;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public int GetID()
    {
        return gameObject.GetInstanceID();
    }

    public List<ELActor> GetActorsBeingTouched()
    {
        return this.actorsBeingTouched;
    }

    // Eat this Actor
    public virtual float Eat(float biteSize)
    {
        return biteSize;
    }
}
