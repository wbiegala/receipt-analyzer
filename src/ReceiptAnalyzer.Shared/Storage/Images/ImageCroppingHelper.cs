using System.Drawing;
using System.Drawing.Imaging;

namespace BS.ReceiptAnalyzer.Shared.Storage.Images
{
    public static class ImageCroppingHelper
    {
        public static byte[] CropImage(byte[] source, int X1, int Y1, int X2, int Y2)
        {
            var sourceBitmap = new Bitmap(new MemoryStream(source));
            var area = new Rectangle(X1, Y1, X2 - X1, Y2 - Y1);
            var resultBitmap = sourceBitmap.Clone(area, sourceBitmap.PixelFormat);
            var resultStream = new MemoryStream();
            resultBitmap.Save(resultStream, ImageFormat.Png);
            resultStream.Position = 0;

            return resultStream.ToArray();
        }
    }
}
