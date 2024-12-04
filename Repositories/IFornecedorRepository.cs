public interface IFornecedorRepository
{
    Task<IEnumerable<Fornecedor>> GetAllAsync();
    Task<Fornecedor?> GetByIdAsync(int id);
    Task AddAsync(Fornecedor fornecedor);
    Task UpdateAsync(Fornecedor fornecedor);
    Task DeleteAsync(int id);
}