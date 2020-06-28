public class GenericRepository<TEntity> where TEntity : class
{
    internal AppdbEntities context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository()
    {
        this.context = new AppdbEntities();
        this.dbSet = context.Set<TEntity>();
    }

    public GenericRepository(AppdbEntities context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }


    public virtual TEntity GetByID(object id)
    {
        return dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }


    public void Save()
    {
        context.SaveChanges();
    }
}
