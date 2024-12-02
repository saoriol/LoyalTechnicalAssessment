using AmazonReviewGenerator.Business;
using Xunit;

namespace AmazonReviewGenerator.Tests
{
    public class MarkovChainGeneratorTests
    {
        [Fact]
        public void Train_ShouldBuildTransitionsForBigramModel()
        {
            // Arrange
            var generator = new MarkovChainGenerator();
            var sentences = new[] { "Great product and price", "Great price and quality" };

            // Act
            generator.Train(sentences, order: 2);

            // Assert
            Assert.Contains("Great product", generator._chain.Keys);
            Assert.Contains("product and", generator._chain.Keys);
            Assert.Contains("price and", generator._chain.Keys);
        }
        
        [Fact]
        public void GenerateText_ShouldReturnValidText_WhenTrained()
        {
            // Arrange
            var generator = new MarkovChainGenerator();
            var sentences = new[] { "Great product and price", "Great price and quality" };
            generator.Train(sentences, order: 2);

            // Act
            var generatedText = generator.GenerateText(order: 2);

            // Assert
            Assert.False(string.IsNullOrEmpty(generatedText)); // Ensure output is non-empty

            // Ensure some words from the training set appear in the output
            var words = generatedText.Split(' ');
            var trainingWords = new[] { "Great", "product", "price", "and", "quality" };
            Assert.Contains(words, word => trainingWords.Contains(word));
        }



        [Fact]
        public void GenerateText_ShouldThrowException_IfNotTrained()
        {
            // Arrange
            var generator = new MarkovChainGenerator();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => generator.GenerateText(order: 2));
        }
    }
}
