using UnityEngine;

public class InstantiateActorCommand : ICommand
{
    private Director _director;
    private ActorFactory.ActorType _type;
    private Vector3 _position;
    private Quaternion _rotation;

    private ELActor actor = null;

    public InstantiateActorCommand(Director director, ActorFactory.ActorType type, Vector3 position, Quaternion rotation)
    {
        this._director = director;
        this._type = type;
        this._position = position;
        this._rotation = rotation;
    }

    public void Execute()
    {
        this.actor = this._director.SpawnActor(this._type, this._position, this._rotation);
    }

    public void Undo()
    {
        if (this.actor == null) return;
        this._position = this.actor.GetPosition();
        this._rotation = this.actor.GetRotation();
        this._director.QueueActorDestruction(this.actor, 0);
    }

    public void Redo()
    {
        this.Execute();
    }
}