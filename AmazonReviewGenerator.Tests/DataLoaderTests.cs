using AmazonReviewGenerator.Data;
using Xunit;
using System.IO;

namespace AmazonReviewGenerator.Tests
{
    public class DataLoaderTests
    {
        [Fact]
        public void LoadData_ShouldLoadReviewsFromValidFile()
        {
            // Arrange
            var dataLoader = new DataLoader();
            const string testFile = "test_dataset.jsonl";
            File.WriteAllText(testFile, "{\"reviewText\": \"Great product!\"}\n{\"reviewText\": \"Not so good.\"}");

            // Act
            dataLoader.LoadData(testFile);

            // Assert
            Assert.Equal(2, dataLoader.Reviews.Count);
            Assert.Contains("Great product!", dataLoader.Reviews);
            Assert.Contains("Not so good.", dataLoader.Reviews);

            // Cleanup
            File.Delete(testFile);
        }

        [Fact]
        public void LoadData_ShouldHandleInvalidJsonGracefully()
        {
            // Arrange
            var dataLoader = new DataLoader();
            const string testFile = "invalid_dataset.jsonl";
            File.WriteAllText(testFile, "Invalid JSON\n{\"reviewText\": \"Valid review.\"}");

            // Act
            dataLoader.LoadData(testFile);

            // Assert
            Assert.Single(dataLoader.Reviews); // Only the valid review should load
            Assert.Contains("Valid review.", dataLoader.Reviews);

            // Cleanup
            File.Delete(testFile);
        }
    }
}
