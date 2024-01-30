using Gible.Domain.Models;
using Gible.Domain.Queries;
using Gible.UnitTesting.Domain.TestDoubles;

namespace Gible.UnitTesting.Domain.Queries
{
    [TestClass]
    public class RecipesByNameQueryTests
    {
        private readonly TestRepository<Recipe> testRepository;

        private readonly RecipesByNameQueryHandler underTest;

        public RecipesByNameQueryTests()
        {
            testRepository = new();

            underTest = new(testRepository);
        }

        [TestMethod]
        public async Task FullNameReturnsRecipe()
        {
            var name = "Pound Cake";
            var recipe = Recipe.Default with { Name = name };

            testRepository.Items.Add(recipe);

            var result = await underTest.RequestAsync(new RecipesByNameQuery([name]));

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task QueryDisregardsCasing()
        {
            var name = "Tas-TEA";
            var recipe = Recipe.Default with { Name = name.ToLower() };

            testRepository.Items.Add(recipe);

            var result = await underTest.RequestAsync(new RecipesByNameQuery([name.ToUpper()]));

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task PartialNameReturnsRecipe()
        {
            var partialName = "Cake";
            var name = "Pound " + partialName;
            var recipe = Recipe.Default with { Name = name };

            testRepository.Items.Add(recipe);

            var result = await underTest.RequestAsync(new RecipesByNameQuery([partialName]));

            Assert.IsTrue(result.Any());
        }
    }
}
