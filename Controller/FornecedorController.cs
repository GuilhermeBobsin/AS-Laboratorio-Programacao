using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorController(IFornecedorRepository fornecedorRepository)
    {
        _fornecedorRepository = fornecedorRepository;
    }

    [HttpGet]
    public Task<ActionResult<IEnumerable<Fornecedor>>> GetAll()
    {
        return _fornecedorRepository.GetAllAsync().ContinueWith(task =>
        {
            var fornecedores = task.Result;
            return (ActionResult<IEnumerable<Fornecedor>>)Ok(fornecedores);
        });
    }

    [HttpGet("{id}")]
    public Task<ActionResult<Fornecedor>> GetById(int id)
    {
        return _fornecedorRepository.GetByIdAsync(id).ContinueWith(task =>
        {
            var fornecedor = task.Result;
            if (fornecedor == null)
                return (ActionResult<Fornecedor>)NotFound();

            return (ActionResult<Fornecedor>)Ok(fornecedor);
        });
    }

    [HttpPost]
    public Task<ActionResult> Create(Fornecedor fornecedor)
    {
        return _fornecedorRepository.AddAsync(fornecedor).ContinueWith(task =>
        {
            return (ActionResult)CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
        });
    }

    [HttpPut("{id}")]
    public Task<ActionResult> Update(int id, Fornecedor fornecedor)
    {
        if (id != fornecedor.Id)
            return Task.FromResult<ActionResult>(BadRequest("O ID do fornecedor não corresponde ao informado na URL."));

        return _fornecedorRepository.UpdateAsync(fornecedor).ContinueWith(task =>
        {
            return (ActionResult)NoContent();
        });
    }

    [HttpDelete("{id}")]
    public Task<ActionResult> Delete(int id)
    {
        return _fornecedorRepository.GetByIdAsync(id).ContinueWith(task =>
        {
            var fornecedor = task.Result;
            if (fornecedor == null)
                return (ActionResult)NotFound();

            return _fornecedorRepository.DeleteAsync(id).ContinueWith(deleteTask =>
            {
                return (ActionResult)NoContent();
            }).Result;
        });
    }
}