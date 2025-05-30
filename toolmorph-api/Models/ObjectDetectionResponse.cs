namespace toolmorph_api.Models
{
    public class Prediction
    {
        public string Class { get; set; }
        public double Probability { get; set; }
    }

    public class ObjectDetectionResponse
    {
        public List<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}
