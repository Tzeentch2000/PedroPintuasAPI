using API_PedroPinturas.Models;
using API_PedroPinturas.DataAccess.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace API_PedroPinturas.Controllers;

[ApiController]
[Route("[controller]")]
public class CompraController : ControllerBase
{
    private readonly RespositoryAsync<Compra> _repository;
    public CompraController(RespositoryAsync<Compra> repository)
    {
        _repository = repository;
    }

    // GET all action
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        return Ok(await _repository.GetAll());
    }

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
       var color = await _repository.GetInnerJoin(id,c => c.Id == id,new List<string>{"Producto","Producto.Color"});
       if(color == null) return NotFound();
       return Ok(await _repository.Get(id)); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<Compra> compras){
        if(compras == null) return BadRequest();
        //Si lo que me están mandando no coincide con el modelo que yo he recibido
        if(!ModelState.IsValid) return BadRequest(ModelState);
        //Do entry porque el color que le añadimos al producto ya existe en la base de datos y por
        //tanto no queremos insertarlo, sino cogerlo
        List<Compra> comprasDevolver = new List<Compra>();
        foreach(Compra compra in compras){
            var created = await _repository.DoEntry(compra);
            comprasDevolver.Add(created);
        }

        return Created("created",comprasDevolver);
    }
}