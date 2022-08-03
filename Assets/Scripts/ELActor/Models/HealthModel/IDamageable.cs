public interface IDamageable : IELActor
{
    public void GetDamaged(float damage);
    public void OnDeath();
}