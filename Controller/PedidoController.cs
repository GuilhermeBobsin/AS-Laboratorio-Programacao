using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoController(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public Task<ActionResult<IEnumerable<Pedido>>> GetAll()
    {
        return _pedidoRepository.GetAllAsync().ContinueWith(task =>
        {
            var pedidos = task.Result;
            return (ActionResult<IEnumerable<Pedido>>)Ok(pedidos);
        });
    }

    [HttpGet("{id}")]
    public Task<ActionResult<Pedido>> GetById(int id)
    {
        return _pedidoRepository.GetByIdAsync(id).ContinueWith(task =>
        {
            var pedido = task.Result;
            if (pedido == null)
                return (ActionResult<Pedido>)NotFound();

            return (ActionResult<Pedido>)Ok(pedido);
        });
    }

    [HttpPost]
    public Task<ActionResult> Create(Pedido pedido)
    {
        return _pedidoRepository.AddAsync(pedido).ContinueWith(task =>
        {
            return (ActionResult)CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
        });
    }

    [HttpPut("{id}")]
    public Task<ActionResult> Update(int id, Pedido pedido)
    {
        if (id != pedido.Id)
            return Task.FromResult<ActionResult>(BadRequest("O ID do pedido não corresponde ao informado na URL."));

        return _pedidoRepository.UpdateAsync(pedido).ContinueWith(task =>
        {
            return (ActionResult)NoContent();
        });
    }

    [HttpDelete("{id}")]
    public Task<ActionResult> Delete(int id)
    {
        return _pedidoRepository.GetByIdAsync(id).ContinueWith(task =>
        {
            var pedido = task.Result;
            if (pedido == null)
                return (ActionResult)NotFound();

            return _pedidoRepository.DeleteAsync(id).ContinueWith(deleteTask =>
            {
                return (ActionResult)NoContent();
            }).Result;
        });
    }
}