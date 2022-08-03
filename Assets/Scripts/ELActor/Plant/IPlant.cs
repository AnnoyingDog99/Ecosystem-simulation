public interface IPlant : IELActor, INutritional, IScalable, IDamageable
{
    public ELActorHealthController GetPlantHealthController();
}