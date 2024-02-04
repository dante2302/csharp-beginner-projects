using ContosoPizza.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shifts_logger.Models;

namespace shifts_logger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : Controller
    {
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            PizzaService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        [HttpPost]
        public ActionResult Create(Pizza pizza)
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, Pizza pizza)
        {
            if(id != pizza.Id)
            {
                return BadRequest();
            }

            Pizza existingPizza = PizzaService.Get(id);

            if(existingPizza is null)
            {
                return NotFound();
            }

            PizzaService.Update(pizza);
            return NoContent(); 
        }

        // GET: PizzaController/Delete/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Pizza pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);
            return NoContent();
        }
    }
}
