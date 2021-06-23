using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.ML;
using ModelBuilder.DataModel;

namespace BlazorClient.Data
{
    public class ImageClassificationService
    {
        private string[] _labels;
        private PredictionEnginePool<ImageInputData, ImageLabelPredictions> _predictionEnginePool;

        public ImageClassificationService(PredictionEnginePool<ImageInputData, ImageLabelPredictions> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;

            //probleme here
            string labelsFileLocation = "TFInceptionModel/imagenet_comp_graph_label_strings.txt";

            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "TFInceptionModel/imagenet_comp_graph_label_strings.txt");


            _labels = File.ReadAllLines(modelPath);

        }

        public ImageClassificationResult Classify(MemoryStream image)
        {
            // TODO: the classification depends on System.Drawing which needs some libraries to be installed in mac/linux
            // There are alternatives to Bitmap, like ImageSharp, but would that work with ML.NET ????
            // https://www.hanselman.com/blog/HowDoYouUseSystemDrawingInNETCore.aspx
            // Can install with: brew install mono-libgdiplus

            // Convert to Bitmap
            Bitmap bitmapImage = (Bitmap)Image.FromStream(image);

            // Set the specific image data into the ImageInputData type used in the DataView
            ImageInputData imageInputData = new ImageInputData { Image = bitmapImage };

            // Predict code for provided image
            ImageLabelPredictions imageLabelPredictions = _predictionEnginePool.Predict(imageInputData);

            // Predict the image's label (The one with highest probability)
            float[] probabilities = imageLabelPredictions.PredictedLabels;
            var maxProbability = probabilities.Max();
            var maxProbabilityIndex = probabilities.AsSpan().IndexOf(maxProbability);
            return new ImageClassificationResult()
            {
                Label = _labels[maxProbabilityIndex],
                Probability = maxProbability
            };
        }

        internal Task InitializeWeatherAsync()
        {
            throw new NotImplementedException();
        }
    }
}
