﻿using System.Drawing;
using System.Drawing.Imaging;

namespace BS.ReceiptAnalyzer.Shared.Storage.Images
{
    public static class ImageFormatHelper
    {
        /// <summary>
        /// Normalizes MIME content type to file extension
        /// </summary>
        /// <param name="MIME">File format as MIME content type</param>
        /// <returns>File extensions without dot (example for "image/jpeg" is: "jpg"</returns>
        public static string NormalizeFileFormat(string MIME)
        {
            return MIME switch
            {
                jpegMIME => "jpg",
                pngMIME => "png",
                _ => string.Empty
            };
        }

        public static string GetMimeFileFormant(string path)
        {
            var extension = Path.GetExtension(path);
            if (ExtensionToMIME.TryGetValue(extension, out var formant))
            {
                return formant;
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts image file to PNG format
        /// </summary>
        /// <param name="MIME">File format as MIME content type</param>
        /// <param name="file">File content as byte array</param>
        /// <returns></returns>
        public static byte[] ConvertToPng(string MIME, byte[] file)
        {
            if (MIME.ToLower() == pngMIME)
                return file;

            var imageObject = new Bitmap(new MemoryStream(file));
            var stream = new MemoryStream();
            imageObject.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            return stream.ToArray();
        }

        private const string jpegMIME = "image/jpeg";
        private const string pngMIME = "image/png";

        private static IDictionary<string, string> ExtensionToMIME = new Dictionary<string, string>
        {
            { ".jpe", jpegMIME },
            { ".jpeg", jpegMIME },
            { ".jpg", jpegMIME },
            { ".png", pngMIME },
            { ".pnz", pngMIME },
        };
    }
}
