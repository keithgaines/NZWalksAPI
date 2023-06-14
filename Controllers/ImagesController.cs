using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // POST: /api/images/Upload
        [HttpPost]
        [Route("Upload")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                // Use repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }

            return BadRequest(ModelState);
        }

        // method to validate file upload
        // private because it's only being access inside the images controller
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            // array of allowed extensions
            var allowedExtensions = new string[]
            {
                ".jpg", ".jpeg", ".png"

            };

            // if extension does not contain one of the allowed extensions return error
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file type");
            }
            // if file length in bytes > 10 Mb return error
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB. Please upload smaller size file.");
            }
        }
    }

}
