using FinalProject.Persistence.Repositories;
using FinaltProject.Test.Helper.Fakers;
using NUnit.Framework;

namespace FinaltProject.Persistence.Test.Repositories
{
    [TestFixture]
    public abstract class BaseRepositoryTester<TEntity, TRepository> where TEntity : class where TRepository : BaseRepository<TEntity>
    {
        protected TRepository _repository = null!;
        protected readonly IFaker<TEntity> _faker;

        public BaseRepositoryTester(IFaker<TEntity> faker)
        {
            _faker = faker;
        }

        protected async Task<TEntity[]> AddItemsToDatabaseAsync(int amount)
        {
            var items = _faker.generate(10);

            foreach (var item in items)
            {
                await _repository.AddAsync(item);
            }

            await _repository.SaveAsync();
            return items;
        }

        [SetUp]
        public abstract void SetUp();

        [Test]
        public async Task TestAddEntity()
        {
            var entity = _faker.generate();
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            var items = await _repository.GetAllAsync();
            Assert.Multiple(() =>
            {
                Assert.That(items, Is.Not.Null);
                Assert.That(items.Any(), Is.True);
                Assert.That(items, Has.Count.EqualTo(1));
                Assert.That(items.First(), Is.EqualTo(entity));
            });
        }

        [Test]
        public async Task TestGetAll_with_empty_list()
        {
            var items = await _repository.GetAllAsync();
            Assert.Multiple(() =>
            {
                Assert.That(items, Is.Not.Null);
                Assert.That(items.Any(), Is.False);
            });
        }

    }
}
