﻿using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Asuat
{
    public static class Img
    {
        public static byte[] ImageToByteArray(BitmapSource imageC)
        {
            if (imageC != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageC));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
            else return null;
        }
        public static BitmapSource ToBitmapImage(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
    }
}
