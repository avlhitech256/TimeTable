namespace DataService.Entity
{
    public interface IDomainEntityFactory
    {
        IDomainEntity<T> GetDomainEntity<T>() where T : class;
        IDomainEntity<T> GetDomainEntity<T>(T entity, long position) where T : class;
    }
}
