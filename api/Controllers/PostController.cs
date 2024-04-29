using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : Controller
{
    private IAppDBContext _context;

    public PostController(IAppDBContext context)
    {
        _context = context;
    }
    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var res = await _context.Post.Where(w => w.Habilitado == true).ToListAsync();
        if (res == null) return NotFound();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var res = await _context.Post.Where(w => w.Id == id).AsNoTracking().FirstOrDefaultAsync();
        if (res == null) return NotFound();
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Post post)
    {
        var cat = _context.Categorias.Where(w => w.Id == post.CategoriaId).AsNoTracking().FirstOrDefault();
        if (cat == null) return NotFound($"la categoria no existe :{post.CategoriaId}");
        post.Categoria = null;
        _context.Post.Add(post);
        await _context.SaveChangesAsync();
        return Ok(post);

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Post post, Guid id)
    {
        if (id != post.Id) throw new Exception();
        var res = await _context.Post.Where(w => w.Id == id).AsNoTracking().FirstOrDefaultAsync();
        if (res == null) return NotFound();
        res.Titulo= post.Titulo;
        res.Contenido = post.Contenido;
        res.CategoriaId = post.CategoriaId;
        res.Habilitado = post.Habilitado;
        await _context.SaveChangesAsync();
        return Ok(res);


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _context.Post.Where(w => w.Id == id).AsNoTracking().FirstOrDefaultAsync();
        if (res == null) return NotFound();
        _context.Post.Remove(res);
        await _context.SaveChangesAsync();
        return Ok(id);
    }
}
