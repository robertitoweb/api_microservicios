using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : Controller
    {
        private IAppDBContext _context;

        public CategoriasController(IAppDBContext context)
        {
            _context = context;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var res = await _context.Categorias.Where(w => w.Habilitado == true).ToListAsync();
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) 
        {
            var res = await _context.Categorias.Where(w => w.Id == id).FirstOrDefaultAsync();
            if(res == null) return NotFound();
           return Ok(res);
        }

        [HttpPost]
        public async Task<Categorias> Post(Categorias category)
        {
            _context.Categorias.Add(category);
            await _context.SaveChangesAsync();
            return category;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Categorias category, Guid id) 
        {
            if (id != category.Id) throw new Exception();
            var res = await _context.Categorias.Where(w=>w.Id==id).FirstOrDefaultAsync();
            if(res == null) return NotFound();
            res.Descripcion = category.Descripcion;
            res.Habilitado = category.Habilitado;
            await _context.SaveChangesAsync();
            return Ok(res);

            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _context.Categorias.Where(w => w.Id == id).FirstOrDefaultAsync();
            if(res == null) return NotFound();
            _context.Categorias.Remove(res);
            await _context.SaveChangesAsync();
            return Ok(id);
        }


    }
}
