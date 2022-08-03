using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public interface IELActor : IScaleModel
{
    public NavMeshAgent GetAgent();

    public BoxCollider GetBoundingBox();

    public ELActorAnimator GetActorAnimator();

    public Vector3 GetPosition();

    public void SetPosition(Vector3 position);

    public Quaternion GetRotation();
    public void SetRotation(Quaternion rotation);

    public Vector3 GetScale();

    public void SetScale(Vector3 newScale);

    public int GetID();

    public int GetLayer();

    public string GetTag();

    public List<IELActor> GetCollidingActors();

    public bool DoActorsCollide(IELActor actor);

    public ELActorMovementController GetActorMovementController();

    public ELActorScaleController GetActorScaleController();
}
