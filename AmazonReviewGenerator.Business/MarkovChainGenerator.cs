using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonReviewGenerator.Business
{
    public class MarkovChainGenerator
    {
        public readonly Dictionary<string, List<string>> _chain = new();
        private readonly Random _random = new();

        public void Train(IEnumerable<string> sentences, int order = 2)
        {
            foreach (var sentence in sentences)
            {
                var words = sentence.Split(' ');
                for (int i = 0; i <= words.Length - order; i++)
                {
                    var key = string.Join(" ", words.Skip(i).Take(order));
                    var nextWord = i + order < words.Length ? words[i + order] : null;

                    if (!_chain.ContainsKey(key))
                        _chain[key] = new List<string>();

                    if (nextWord != null)
                        _chain[key].Add(nextWord);
                }
            }
        }

        public string GenerateText(int maxWords = 50, int order = 2)
        {
            if (!_chain.Any())
                throw new InvalidOperationException("Markov chain is not trained yet.");

            var keys = _chain.Keys.ToList();
            var currentKey = keys[_random.Next(keys.Count)];
            var result = new List<string>(currentKey.Split(' '));

            for (int i = 0; i < maxWords - order; i++)
            {
                if (!_chain.ContainsKey(currentKey) || _chain[currentKey].Count == 0)
                    break;

                var nextWord = GetWeightedRandomNext(_chain[currentKey]);
                result.Add(nextWord);

                // Update the key to the last `order` words in the result
                currentKey = string.Join(" ", result.Skip(result.Count - order).Take(order));
            }

            return string.Join(" ", result);
        }

        private string GetWeightedRandomNext(List<string> words)
        {
            var grouped = words.GroupBy(w => w).Select(g => new { Word = g.Key, Count = g.Count() }).ToList();
            var total = grouped.Sum(g => g.Count);
            var rand = _random.Next(total);

            foreach (var group in grouped)
            {
                if (rand < group.Count)
                    return group.Word;
                rand -= group.Count;
            }

            return grouped.Last().Word; // Fallback
        }

        private readonly List<string> _templates = new()
        {
            "I give this {stars} stars because {generatedReview}.",
            "This is a {adjective} product! {generatedReview}",
            "Overall, I think {generatedReview}."
        };

        private readonly string[] _positiveAdjectives = { "great", "excellent", "amazing", "fantastic" };
        private readonly string[] _negativeAdjectives = { "terrible", "poor", "disappointing", "awful" };

        public string GenerateTextWithTemplate(int order = 2)
        {
            var template = _templates[_random.Next(_templates.Count)];
            var generatedReview = GenerateText(order: order);

            return template
                .Replace("{stars}", _random.Next(1, 6).ToString())
                .Replace("{adjective}", _random.Next(1, 6) > 3
                    ? _positiveAdjectives[_random.Next(_positiveAdjectives.Length)]
                    : _negativeAdjectives[_random.Next(_negativeAdjectives.Length)])
                .Replace("{generatedReview}", generatedReview);
        }

        private readonly string[] _positiveWords = { "great", "excellent", "amazing", "good", "love" };
        private readonly string[] _negativeWords = { "poor", "terrible", "bad", "hate", "awful" };

        public int DetermineStarRating(string review)
        {
            var lowerReview = review.ToLower();
            var positiveScore = _positiveWords.Count(word => lowerReview.Contains(word));
            var negativeScore = _negativeWords.Count(word => lowerReview.Contains(word));

            if (positiveScore > negativeScore) return _random.Next(4, 6); // 4-5 stars
            if (negativeScore > positiveScore) return _random.Next(1, 3); // 1-2 stars
            return _random.Next(2, 5); // Neutral (3-4 stars)
        }


    }
}
