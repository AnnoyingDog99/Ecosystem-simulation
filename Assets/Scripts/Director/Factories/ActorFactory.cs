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
        // Animals
        BUNNY,
        FOX,

        // FIELDS
        GRASSFIELD,

        // Plants
        GRASS
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
            case ActorType.GRASSFIELD:
                gameObject = Instantiate(grassFieldPrefab, position, rotation);
                break;
            case ActorType.GRASS:
                gameObject = Instantiate(grassPrefab, position, rotation);
                break;
        }

        return gameObject;
    }
}
