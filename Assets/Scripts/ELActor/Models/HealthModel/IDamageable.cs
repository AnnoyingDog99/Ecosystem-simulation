public interface IDamageable
{
    public AnimalHealthController GetAnimalHealthController();
    public void GetDamaged(float damage);
    public void OnDeath();
}