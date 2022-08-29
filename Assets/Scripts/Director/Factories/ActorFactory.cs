using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActorFactory : MonoBehaviour
{
    // Animals
    [SerializeField] private GameObject bunnyPrefab;
    [SerializeField] private GameObject foxPrefab;

    // Fields
    [SerializeField] private GameObject grassFieldPrefab;

    // Plants
    [SerializeField] private GameObject grassPrefab;

    public enum ActorType
    {
        NONE,

        // Animals
        BUNNY,
        FOX,

        // Plants
        GRASS
    }

    public static ActorFactory.ActorType TranslateClassToType(IELActor actor)
    {
        ActorFactory.ActorType type = ActorType.NONE;
        switch (actor)
        {
            case Bunny:
                type = ActorFactory.ActorType.BUNNY;
                break;
            case Fox:
                type = ActorFactory.ActorType.FOX;
                break;
            case Grass:
                type = ActorFactory.ActorType.GRASS;
                break;
            default:
                type = ActorType.NONE;
                break;
        }
        return type;
    }

    public GameObject CreateActor(ActorType type, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = null;
        switch (type)
        {
            case ActorType.BUNNY:
                gameObject = Instantiate(bunnyPrefab, position, rotation);
                break;
            case ActorType.FOX:
                gameObject = Instantiate(foxPrefab, position, rotation);
                break;
            case ActorType.GRASS:
                gameObject = Instantiate(grassPrefab, position, rotation);
                break;
        }

        return gameObject;
    }
}
