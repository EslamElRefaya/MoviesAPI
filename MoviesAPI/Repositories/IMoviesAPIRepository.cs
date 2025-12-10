namespace MoviesAPI.Repositories
{
    public interface IMoviesAPIRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllItems_Repo();
        Task<TEntity> GetItemById_Repo(int Id);
        Task AddItem_Repo(TEntity entity);
        Task UpdateItem_Repo(TEntity entity);
        Task DeleteItem_Repo(TEntity entity);

    }
}
