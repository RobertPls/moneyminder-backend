namespace SharedKernel.Core
{
    public interface IUnitOfWork
    {
        Task Commit();
        void RemoveEvent(DomainEvent eventToRemove, Entity<Guid> entity);
    }
}
