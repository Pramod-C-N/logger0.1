using CRUDApplication.DAL;
using CRUDApplication.Model;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;


namespace CRUDApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductController));
        //static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProductController));
        private readonly MyAppDbContext _appDbContext;
        public ProductController(MyAppDbContext appDbContext)
        {
           
            // Assign the provided database context to the private field.
            _appDbContext = appDbContext;
           

        }
        [HttpGet]
        public IActionResult Get()
        {
            

            try
            {
              
                var products = _appDbContext.Products.ToList();
                
                log.Info("This is the Get method.");
                if (products.Count == 0)
                {
                    log.Warn("No products found.");
                    return NotFound("Products not available.");
                }
                log.Info("Products retrieved successfully.");
                return Ok(products);
                
            }
            catch (Exception ex)
            {

                log.Error($"An error occurred in the Get method. { ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            
            try
            {
                var startTime = DateTime.Now;
                var product = _appDbContext.Products.Find(id);
                log.Info($"This is the Get Id method. Start Time : {startTime}");
                var endTime = DateTime.Now;
                if (product == null)
                {
                    log.Warn("No products found.");
                    return NotFound($"Product details not fount with id {id}");
                }
                
                log.Info("Products retrieved successfully.");
                log.Info($"End time : {endTime}");

                return Ok(product);
               
            }
            catch (Exception ex)
            {
                log.Error("An error occurred in the Get Id method.", ex);
                return BadRequest(ex.Message);
                
            }
        }

        [HttpPost]
        public IActionResult Post(Product model)
        {
            try
            {
                _appDbContext.Products.Add(model);
                _appDbContext.SaveChanges();
                log.Info("Product Created");
                return Ok("Product Created");

            }
            catch (Exception ex)
            {
                log.Error("Error creating product.", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Product model)
        {
            if (model == null || model.Id == 0)
            {
                if (model == null)
                {
                    return BadRequest("Model data is invalid.");
                }
                else if (model.Id == 0)
                {
                    return BadRequest($"Product Id {model.Id} is invalid.");
                }
            }
            try
            {
                var product = _appDbContext.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {model.Id}");
                }
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _appDbContext.SaveChanges();
                return Ok("Product details updated");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _appDbContext.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {id}");
                }
                _appDbContext.Remove(product);
                _appDbContext.SaveChanges();
                return Ok("Product details deleted.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
