using CoreEstate.Data;
using CoreEstate.Dtos;
using CoreEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreEstate.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForSalePhotosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public ForSalePhotosController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // POST: api/ForSalePhotos
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostForSalePhoto([FromForm] PropertyPhotoDto propertyPhotoDto)
        {
            await Console.Out.WriteLineAsync("PostForSalePhoto:" + propertyPhotoDto.PropertyId);
            if (_context.PropertyPhotos == null || _context.ForSaleProperties == null)
            {
                return Problem("Entity set 'ApplicationDbContext' is null.");
            }

            var forSaleProperty = await _context.ForSaleProperties.FindAsync(propertyPhotoDto.PropertyId);

            if (forSaleProperty == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            await Console.Out.WriteLineAsync("Copy for sale photo");
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(propertyPhotoDto.File.FileName);
            var filePath = Path.Combine(uploadsFolder, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await propertyPhotoDto.File.CopyToAsync(stream);
            }

            await Console.Out.WriteLineAsync("Create new PropertyPhoto");
            var propertyPhoto = new PropertyPhoto { Filename = filename };
            forSaleProperty.Photos.Add(propertyPhoto);
            await _context.PropertyPhotos.AddAsync(propertyPhoto);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
