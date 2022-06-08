using UnityEngine;

public abstract class ELActor : MonoBehaviour
{
    [SerializeField] protected GameObject ownKindGameObject = null;
    protected LifeTime lifeTime = new LifeTime();
    public bool isDead { get; protected set; } = false;
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

    protected void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    public Vector3 GetScale()
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
