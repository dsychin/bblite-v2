using BBLite.Data;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NewsRepositoryTests
{
    public class Tests
    {
        NewsRepository sut;

        [SetUp]
        public void Setup()
        {
            sut = new NewsRepository();
        }

        [Test]
        public async Task ShouldReturnListOfNews()
        {
            var result = await sut.GetAll();

            Assert.That(result.Count > 0);
        }

        [Test]
        public async Task ShouldReturnDifferentNewsForDifferentPage()
        {
            var result1 = await sut.GetAll(1);
            var result2 = await sut.GetAll(2);

            Assert.That(result1[0].ArticleUrl != result2[0].ArticleUrl);
        }

        [Test]
        [TestCase("adhere-to-fair-practices-businesses-told",
            "Adhere to fair practices, businesses told")]
        [TestCase("22nd-consumer-fair-kicks-off-2",
            "22nd Consumer Fair kicks off")]
        public async Task ShouldRetrieveArticle(string path, string resultTitle)
        {
            var result = await sut.GetArticle(path);

            Assert.That(result.Title.IndexOf(resultTitle) > -1);
        }
    }
}