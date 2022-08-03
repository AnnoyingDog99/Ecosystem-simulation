public interface IDamageable : IELActor
{
    public ELActorHealthController GetActorHealthController();
    public void GetDamaged(float damage);
    public void OnDeath();
}