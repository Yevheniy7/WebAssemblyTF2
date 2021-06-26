using Microsoft.ML.Transforms.Image;
using System;
using SixLabors.ImageSharp;
using System.Drawing;

namespace ModelBuilder.DataModel
{
    public class ImageInputData
    {
        [ImageType(224, 224)]
        public Bitmap Image { get; set; }
    }
}