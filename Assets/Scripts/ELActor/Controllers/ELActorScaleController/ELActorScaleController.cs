using UnityEngine;
using UnityEngine.AI;

public class ELActorScaleController : Controller
{
    private IScalable actor;
    private float scaleOverTimeTimer = 0;
    private float scaleOverTimeTime = 0;
    private Vector3 currentTargetScale = Vector3.zero;
    private Vector3 scaleStep = Vector3.zero;
    private bool isScalingOverTime = false;

    protected override void Start()
    {
        this.actor = GetComponentInParent<IScalable>();
    }

    protected override void Update()
    {
        base.Update();
        if (this.isScalingOverTime)
        {
            this.isScalingOverTime = this.ScaleOverTime();
        }
    }

    public void SetScale(Vector3 newScale)
    {
        // Stop scaling over time
        this.isScalingOverTime = false;

        float x = Mathf.Clamp(newScale.x, this.actor.GetMinScale().x, this.actor.GetMaxScale().x);
        float y = Mathf.Clamp(newScale.y, this.actor.GetMinScale().y, this.actor.GetMaxScale().y);
        float z = Mathf.Clamp(newScale.z, this.actor.GetMinScale().z, this.actor.GetMaxScale().z);
        this.actor.SetScale(new Vector3(x, y, z));
    }

    private bool ScaleOverTime()
    {
        if ((this.scaleOverTimeTimer += Time.deltaTime) > this.scaleOverTimeTime)
        {
            return false;
        }
        if (this.actor.GetScale() == this.currentTargetScale)
        {
            return false;
        }
        this.SetScale(this.actor.GetScale() + (this.scaleStep * Time.deltaTime));
        return true;
    }

    private int GetScalePercentage()
    {
        float maxDistance = Vector3.Distance(this.actor.GetMaxScale(), this.actor.GetMinScale());
        float currentDistance = Vector3.Distance(this.actor.GetMaxScale(), this.actor.GetScale());
        return Mathf.RoundToInt((100 / maxDistance) * (maxDistance - currentDistance));
    }

    public void ScaleOverTime(Vector3 targetScale, float seconds)
    {
        this.scaleOverTimeTimer = 0;
        this.scaleOverTimeTime = seconds;
        this.currentTargetScale = targetScale;
        this.scaleStep = (this.currentTargetScale - this.actor.GetScale()) / this.scaleOverTimeTime;
        this.isScalingOverTime = true;
    }
}