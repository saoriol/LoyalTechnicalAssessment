using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AmazonReviewGenerator.Data
{
    public class DataLoader
    {
        public List<string> Reviews { get; private set; } = new();

        public void LoadData(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Dataset not found at {filePath}");

            using var reader = new StreamReader(filePath);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    try
                    {
                        var record = JsonSerializer.Deserialize<AmazonReview>(line);
                        if (!string.IsNullOrEmpty(record?.reviewText))
                            Reviews.Add(record.reviewText);
                    }
                    catch (JsonException)
                    {
                        continue; // Skip invalid lines
                    }
                }
            }
        }
    }

    public class AmazonReview
    {
        public string reviewText { get; set; }
    }
}
