using FinalProject.Persistence.Repositories;
using FinaltProject.Persistence.Test.Helpers;
using NUnit.Framework;

namespace FinaltProject.Persistence.Test.Repositories
{
    public class BaseRepositoryTester<TEntity, TRepository> where TEntity : class where TRepository : BaseRepository<TEntity>
    {
        protected readonly TRepository _repository;
        protected readonly IFaker<TEntity> _faker;

        // TODO: this doesn't work.
        public BaseRepositoryTester(IFaker<TEntity> faker, TRepository repository)
        {
            _repository = repository;
            _faker = faker;
        }

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
