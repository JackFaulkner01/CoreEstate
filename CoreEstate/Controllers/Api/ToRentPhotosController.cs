using CoreEstate.Data;
using CoreEstate.Dtos;
using CoreEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreEstate.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToRentPhotosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public ToRentPhotosController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // POST: api/ToRentPhotos
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostToRentPhoto([FromForm] PropertyPhotoDto propertyPhotoDto)
        {
            if (_context.PropertyPhotos == null || _context.ToRentProperties == null)
            {
                return Problem("Entity set 'ApplicationDbContext' is null.");
            }

            var toRentProperty = await _context.ToRentProperties.FindAsync(propertyPhotoDto.PropertyId);

            if (toRentProperty == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filename = Guid.NewGuid().ToString() + Path.GetExtension(propertyPhotoDto.File.FileName);
            var filePath = Path.Combine(uploadsFolder, filename);

            using var image = Image.Load(propertyPhotoDto.File.OpenReadStream());
            image.Mutate(x => x.Resize(948, 598));
            image.Save(filePath);

            var propertyPhoto = new PropertyPhoto { Filename = filename };
            toRentProperty.Photos.Add(propertyPhoto);
            await _context.PropertyPhotos.AddAsync(propertyPhoto);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ToRent/Edit", new { id = toRentProperty.Id });
        }
    }
}
