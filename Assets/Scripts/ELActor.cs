using UnityEngine;

public class ELActor : MonoBehaviour
{
    protected LifeTime lifeTime = new LifeTime();
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
        InvokeRepeating("handleLifeTime", 1, 1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    private void handleLifeTime()
    {
        lifeTime.addSecond();
    }

    public Vector3 GetLifeTime() {
        return new Vector3(this.lifeTime.hours, this.lifeTime.minutes, this.lifeTime.seconds);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
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
    protected void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    protected Vector3 GetScale()
    {
        return transform.localScale;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public int GetID()
    {
        return gameObject.GetInstanceID();
    }
}
