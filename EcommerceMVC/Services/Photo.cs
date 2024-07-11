using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EcommerceMVC.Data;
using EcommerceMVC.Helper;
using Microsoft.Extensions.Options;

namespace EcommerceMVC.Services
{
	public class Photo
	{
		private readonly Cloudinary _cloundinary;

		private readonly AppDbContext _appDbContext;

		public Photo(IOptions<CloudinarySettings> config, AppDbContext appDbContext)
		{
			var acc = new Account(
				config.Value.CloudName,
				config.Value.ApiKey,
				config.Value.ApiSecret
				);
			_cloundinary = new Cloudinary(acc);
			_appDbContext = appDbContext;
		}

		public async Task<ImageUploadResult> AddPhoto(IFormFile file)
		{
			var upload = new ImageUploadResult();
			if (file.Length > 0)
			{
				using var stream = file.OpenReadStream();
				var uploadParam = new ImageUploadParams
				{
					File = new FileDescription(file.Name, stream),
					Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
				};
				upload = await _cloundinary.UploadAsync(uploadParam);
			}
			return upload;
		}
	}
}
