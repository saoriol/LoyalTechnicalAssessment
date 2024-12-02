using AmazonReviewGenerator.Business;
using AmazonReviewGenerator.Data;
using Microsoft.AspNetCore.Mvc;

namespace AmazonReviewGenerator.Api.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public class ReviewController : ControllerBase
    {
        private readonly MarkovChainGenerator _markovChain;
        private readonly Random _random;

        public ReviewController(MarkovChainGenerator markovChain, DataLoader dataLoader)
        {
            _markovChain = markovChain;
            _random = new Random();

            const string datasetPath = "dataset.txt";

            try
            {
                dataLoader.LoadData(datasetPath);

                if (dataLoader.Reviews.Count > 0)
                {
                    _markovChain.Train(dataLoader.Reviews, order: 2); // Higher-order chain
                    Console.WriteLine($"Markov chain trained on {dataLoader.Reviews.Count} reviews.");
                }
                else
                {
                    Console.WriteLine("No reviews loaded. Markov chain will not be trained.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during data loading or training: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GenerateReview()
        {
            var review = _markovChain.GenerateTextWithTemplate(order: 2);
            var starRating = _markovChain.DetermineStarRating(review);

            return Ok(new
            {
                review,
                starRating
            });
        }
    }
}
