using Microsoft.EntityFrameworkCore;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Fornecedor>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Fornecedor>>(_context.Fornecedores.AsEnumerable());
    }

    public Task<Fornecedor?> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.Fornecedores.Find(id));
    }

    public Task AddAsync(Fornecedor fornecedor)
    {
        _context.Fornecedores.Add(fornecedor);
        return Task.FromResult(_context.SaveChanges());
    }

    public Task UpdateAsync(Fornecedor fornecedor)
    {
        _context.Fornecedores.Update(fornecedor);
        return Task.FromResult(_context.SaveChanges());
    }

    public Task DeleteAsync(int id)
    {
        var fornecedor = _context.Fornecedores.Find(id);
        if (fornecedor != null)
        {
            _context.Fornecedores.Remove(fornecedor);
            return Task.FromResult(_context.SaveChanges());
        }

        return Task.CompletedTask;
    }
}