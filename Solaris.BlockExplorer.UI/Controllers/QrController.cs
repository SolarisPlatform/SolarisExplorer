using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QRCoder;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class QrController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public QrController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index(string id)
        {
            MemoryStream memoryStream;

            if (!_memoryCache.TryGetValue(id, out var qr))
            {
                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new QRCode(qrCodeData))
                using (var qrCodeAsBitmap = qrCode.GetGraphic(20))
                {
                    memoryStream = new MemoryStream();
                    qrCodeAsBitmap.Save(memoryStream, ImageFormat.Png);

                    _memoryCache.Set(id, memoryStream.ToArray());

                    memoryStream.Position = 0;
                }
            }
            else
            {
                memoryStream = new MemoryStream((byte[])qr);
            }

            return File(memoryStream, "image/png");
        }
    }
}