using CoreEstate.Data;
using CoreEstate.Dtos;
using CoreEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreEstate.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForSalePhotosCollectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public ForSalePhotosCollectionController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // POST: api/ForSalePhotosCollection
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostForSalePhotoCollection([FromForm] PropertyPhotoCollectionDto propertyPhotoCollectionDto)
        {
            if (_context.PropertyPhotos == null || _context.ForSaleProperties == null)
            {
                return Problem("Entity set 'ApplicationDbContext' is null.");
            }

            var forSaleProperty = await _context.ForSaleProperties.FindAsync(propertyPhotoCollectionDto.PropertyId);

            if (forSaleProperty == null)
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
                forSaleProperty.Photos.Add(propertyPhoto);
                await _context.PropertyPhotos.AddAsync(propertyPhoto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/ForSale/Edit", new { id = forSaleProperty.Id });
        }
    }
}
