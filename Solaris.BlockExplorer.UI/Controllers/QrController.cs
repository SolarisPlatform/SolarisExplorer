using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class QrController : Controller
    {
        public IActionResult Index(string address)
        {
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(address, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrCodeData))
            using (var qrCodeAsBitmap = qrCode.GetGraphic(20))
            {
                var memoryStream = new MemoryStream();
                qrCodeAsBitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;
                return File(memoryStream, "image/png");
            }
        }
    }
}