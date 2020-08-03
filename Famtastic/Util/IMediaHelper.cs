using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Famtastic.Util
{
	public interface IMediaHelper
	{
		Task<FileContentResult> RenderPhoto(string imageType, int id);
	}
}