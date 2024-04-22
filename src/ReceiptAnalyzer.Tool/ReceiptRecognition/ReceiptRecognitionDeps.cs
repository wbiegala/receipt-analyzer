﻿using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Tool.ReceiptRecognition
{
    internal static class ReceiptRecognitionDeps
    {
        public static IServiceCollection AddReceiptRecognition(this IServiceCollection services)
        {
            services.AddSingleton<IImagesProcessor, ReceiptRecognitionProcessor>();
            services.AddLocalReceiptRecognizerCore();
            
            return services;
        }
    }
}
