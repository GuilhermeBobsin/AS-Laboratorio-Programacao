using Microsoft.EntityFrameworkCore;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Pedido>>(_context.Pedidos.AsEnumerable());
    }

    public Task<Pedido?> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.Pedidos.Find(id));
    }

    public Task AddAsync(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        return Task.FromResult(_context.SaveChanges());
    }

    public Task UpdateAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        return Task.FromResult(_context.SaveChanges());
    }

    public Task DeleteAsync(int id)
    {
        var pedido = _context.Pedidos.Find(id);
        if (pedido != null)
        {
            _context.Pedidos.Remove(pedido);
            return Task.FromResult(_context.SaveChanges());
        }

        return Task.CompletedTask;
    }
}