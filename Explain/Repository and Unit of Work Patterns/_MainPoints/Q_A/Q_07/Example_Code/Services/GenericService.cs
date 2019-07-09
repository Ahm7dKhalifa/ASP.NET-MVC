public class GenericService<TEntity> where TEntity : class
{
    protected GenericRepository<TEntity> _repository;

    public GenericService(GenericRepository<TEntity> repository)
    {
        //
        // TODO: Add constructor logic here
        //
        _repository = repository;
    }

    public virtual TEntity GetByID(object id)
    {
        return _repository.GetByID(id);
    }

    public virtual void Insert(TEntity entity)
    {
        _repository.Insert(entity);
    }

    public virtual void Delete(object id)
    {
        _repository.Delete(id);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        _repository.Delete(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _repository.Update(entityToUpdate);
    }


    public void Save()
    {
        _repository.Save();
    }
}