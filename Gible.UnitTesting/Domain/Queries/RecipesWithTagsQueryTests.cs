using Gible.Domain.Models;
using Gible.Domain.Queries;
using Gible.UnitTesting.Domain.TestDoubles;

namespace Gible.UnitTesting.Domain.Queries
{
    [TestClass]
    public class RecipesWithTagsQueryTests
    {
        private readonly TestRepository<Recipe> testRepository;

        private readonly RecipesWithTagsQueryHandler underTest;

        public RecipesWithTagsQueryTests()
        {
            testRepository = new();

            underTest = new(testRepository);
        }

        [TestMethod]
        public async Task MultipleRecipesAreReturned()
        {
            var testTag = "Sauce";

            var expected = new List<Recipe>() {
                Recipe.Default with { Tags = ["Meat", "Tomato", "Pasta", testTag] },
                Recipe.Default with { Tags = ["Mexican", "Salsa", testTag] }
            };

            testRepository.Items.AddRange(expected);

            var result = await underTest.RequestAsync(new RecipesWithTagsQuery([testTag]));

            Assert.AreEqual(expected.Count, result.Count());
        }

        [TestMethod]
        public async Task RecipesWithTagsContainingRequestAreReturned()
        {
            var partialTag = "Potatoes";
            var fullTag = "Mashed " + partialTag;

            var expected = new List<Recipe>() {
                Recipe.Default with { Tags = ["Meat", "Tomato", "Pasta", fullTag] },
            };

            testRepository.Items.AddRange(expected);

            var result = await underTest.RequestAsync(new RecipesWithTagsQuery([partialTag]));

            Assert.AreEqual(expected.Count, result.Count());
        }
    }
}
