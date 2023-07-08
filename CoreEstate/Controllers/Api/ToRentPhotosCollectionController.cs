using CoreEstate.Data;
using CoreEstate.Dtos;
using CoreEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreEstate.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToRentPhotosCollectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public ToRentPhotosCollectionController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // POST: api/ToRentPhotosCollection
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostToRentPhotoCollection([FromForm] PropertyPhotoCollectionDto propertyPhotoCollectionDto)
        {
            if (_context.PropertyPhotos == null || _context.ForSaleProperties == null)
            {
                return Problem("Entity set 'ApplicationDbContext' is null.");
            }

            var toRentProperty = await _context.ToRentProperties.FindAsync(propertyPhotoCollectionDto.PropertyId);

            if (toRentProperty == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var propertyPhotoFile in propertyPhotoCollectionDto.Files)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(propertyPhotoFile.FileName);
                var filePath = Path.Combine(uploadsFolder, filename);

                using var image = Image.Load(propertyPhotoFile.OpenReadStream());
                image.Mutate(x => x.Resize(948, 598));
                image.Save(filePath);

                var propertyPhoto = new PropertyPhoto { Filename = filename };
                toRentProperty.Photos.Add(propertyPhoto);
                await _context.PropertyPhotos.AddAsync(propertyPhoto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/ToRent/Edit", new { id = toRentProperty.Id });
        }
    }
}
