namespace AVH.QuestEngine.Domain.LifeTime
{
    public interface ITransientDependency
    {

    }

    public interface IScopedDependency : ITransientDependency
    {

    }

    public interface ISingletonDependency : ITransientDependency
    {

    }
}
