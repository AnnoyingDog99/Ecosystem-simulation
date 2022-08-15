using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    private static Director _instance;
    public static Director Instance { get { return _instance; } }

    [SerializeField] private ActorFactory actorFactory;
    [SerializeField] private List<ELActor> actors = new List<ELActor>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public ELActor SpawnActor(ActorFactory.ActorType type, Vector3 position, Quaternion rotation)
    {
        ELActor newActor = actorFactory.CreateActor(type, position, rotation).GetComponent<ELActor>();
        this.actors.Add(newActor);
        return newActor;
    }

    public bool ActorExists(IELActor actor)
    {
        if (actor == null) return false;
        if (actor.Equals(null)) return false;
        this.actors = this.actors.FindAll((_actor) => _actor != null);
        return this.actors.Find((_actor) => _actor.GetID() == actor.GetID()) != null;
    }

    public void QueueActorDestruction(ELActor actor, float delay)
    {
        StartCoroutine(this.DestroyAfterDelay(actor, delay));
    }

    private IEnumerator DestroyAfterDelay(ELActor actor, float delay)
    {
        yield return new WaitForSeconds(delay);
        this.actors.Remove(actor);
        Destroy(actor.gameObject);
        Destroy(actor);
    }
}
