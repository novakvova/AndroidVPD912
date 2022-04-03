using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Helpers;
using Shop.Models;
using System.Drawing.Imaging;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create(CreateProductViewModel model)
        {
            var img = ImageWorker.FromBase64StringToImage(model.Image);
            string randomFilename = Path.GetRandomFileName() + ".jpeg";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
            img.Save(dir, ImageFormat.Jpeg);

            ProductEntity product = _mapper.Map<ProductEntity>(model);

            return Ok();
        }
    }
}
