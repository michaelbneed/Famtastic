using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Biz;
using DataAccess.Crud;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;           
using System.Drawing.Drawing2D;    
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace Famtastic.Util
{
	public class MediaHelper : Controller, IMediaHelper
	{
		private readonly IDbReadService _dbRead;
		private readonly IDbWriteService _dbWrite;

		public MediaHelper(IDbReadService dbRead, IDbWriteService dbWrite)
		{
			_dbRead = dbRead;
			_dbWrite = dbWrite;
		}

		public static byte[] ResizeOnRenderImage(Image image, Size size,
			bool preserveAspectRatio = true)
		{
			int newWidth;
			int newHeight;
			if (preserveAspectRatio)
			{
				int originalWidth = image.Width;
				int originalHeight = image.Height;
				float percentWidth = (float)size.Width / (float)originalWidth;
				float percentHeight = (float)size.Height / (float)originalHeight;
				float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
				newWidth = (int)(originalWidth * percent);
				newHeight = (int)(originalHeight * percent);
			}
			else
			{
				newWidth = size.Width;
				newHeight = size.Height;
			}
			Image newImage = new Bitmap(newWidth, newHeight);
			using (Graphics graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
			}

			MemoryStream ms = new MemoryStream();
			newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
			return ms.ToArray();

			//return newImage;
		}

		public Image GetImageFromBytes(byte[] imageBytes)
		{
			MemoryStream ms = new MemoryStream(imageBytes);
			return new Bitmap(ms);
		}

		public async Task<FileContentResult> RenderPhoto(string imageType, int id)
		{
			byte[] imageByteArray = null;
			string contentType = string.Empty;

			if (imageType != null && imageType == Constants.RenderFamilyImage)
			{
				var photo = await _dbRead.GetSingleRecordAsync<Family>(p => p.Id.Equals(id));
				imageByteArray = photo.Picture;
				//var image = GetImageFromBytes(photo.Picture);
				//if (image.Height > 325)
				//{
				//	imageByteArray = ResizeOnRenderImage(image, new Size(325, 325), true);
				//}
				
				contentType = photo.PictureContentType;
			}

			if (imageType != null && imageType == Constants.RenderProfileImage)
			{
				var photo = await _dbRead.GetSingleRecordAsync<UserProfile>(p => p.Id.Equals(id));
				imageByteArray = photo.Picture;
				contentType = photo.PictureContentType;
			}

			if (imageType != null && imageType == Constants.RenderMediaImage)
			{
				var photo = await _dbRead.GetSingleRecordAsync<Media>(p => p.Id.Equals(id));
				imageByteArray = photo.Image;
				contentType = photo.ImageContentType;
			}

			return File(imageByteArray, contentType);
		}
		
		public static byte[] ResizeImageFromFile(IFormFile file)
		{
			var fileSize = file.Length / 1048576.0;
			var imageSizeFriendly = (double)Math.Round(fileSize, 2);

			if ((imageSizeFriendly) > 4.00)
			{
				return new byte[]{};
			}

			if ((imageSizeFriendly) > 3.00 && (imageSizeFriendly) <= 4.00)
			{
				return ResizeImageFromFile(file, .25);
			}

			if ((imageSizeFriendly) > 2.50 && (imageSizeFriendly) <= 3.00)
			{
				return ResizeImageFromFile(file, .33);
			}

			if ((imageSizeFriendly) > 2.00 && (imageSizeFriendly) <= 2.50)
			{
				return ResizeImageFromFile(file, .4);
			}

			if ((imageSizeFriendly) > 1.50 && (imageSizeFriendly) <= 2.00)
			{
				return ResizeImageFromFile(file, .5);
			}

			if ((imageSizeFriendly) > 1.00 && (imageSizeFriendly) <= 1.50)
			{
				return ResizeImageFromFile(file, .75);
			}

			if ((imageSizeFriendly) > .5 && (imageSizeFriendly) <= 1.00)
			{
				return ResizeImageFromFile(file, .80);
			}

			return ResizeImageFromFile(file, 1);
		}

		private static byte[] ResizeImageFromFile(IFormFile file, double scaleFactor)
		{
			MemoryStream ms = new MemoryStream();
			var resizeFile = file.OpenReadStream();

			ResizeImage(scaleFactor, resizeFile, ms);

			return ms.ToArray();
		}

		private static void ResizeImage(double scaleFactor, Stream fromStream, Stream toStream)
		{
			var image = Image.FromStream(fromStream);
			
			var newWidth = (int)(image.Width * scaleFactor);
			var newHeight = (int)(image.Height * scaleFactor);

			var thumbnailBitmap = new Bitmap(newWidth, newHeight);

			var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
			thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
			thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
			thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

			var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
			thumbnailGraph.DrawImage(image, imageRectangle);
			
			thumbnailBitmap.Save(toStream, image.RawFormat);

			thumbnailGraph.Dispose();
			thumbnailBitmap.Dispose();
			image.Dispose();
		}
	}
}
