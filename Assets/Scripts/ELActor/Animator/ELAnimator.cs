using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELAnimator : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    private Dictionary<string, int> animationHashes = new Dictionary<string, int>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected int AddAnimation(string animation)
    {
        if (this.animationHashes.ContainsKey(animation))
        {
            Debug.LogError("Animation with name: " + animation + " already exists.");
            return -1;
        }
        int animationHash = Animator.StringToHash(animation);
        this.animationHashes.Add(animation, animationHash);
        return animationHash;
    }

    public Dictionary<string, int> GetAnimationHashes()
    {
        return this.animationHashes;
    }

    public void SetBool(string animation, bool value)
    {
#if DEBUG
        if (!this.animationHashes.ContainsKey(animation))
        {
            Debug.LogError("Animation with name: " + animation + " could not be found.");
            return;
        }
#endif
        this.animator.SetBool(animation, value);
    }

    public void SetBool(int animation, bool value)
    {
#if DEBUG
        this.ValidateHashExists(animation);
#endif
        this.animator.SetBool(animation, value);
    }

    public bool GetBool(string animation)
    {
#if DEBUG
        if (!this.animationHashes.ContainsKey(animation))
        {
            Debug.LogError("Animation with name: " + animation + " could not be found.");
            return false;
        }
#endif
        return this.animator.GetBool(animation);
    }

    public bool GetBool(int animation)
    {
#if DEBUG
        this.ValidateHashExists(animation);
#endif
        return this.animator.GetBool(animation);
    }

#if DEBUG
    private void ValidateHashExists(int hash)
    {
        bool found = false;
        foreach (string key in this.animationHashes.Keys)
        {
            if (this.animationHashes[key] == hash)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            Debug.LogError("Failed to find animation hash");
        }
    }
#endif
}
