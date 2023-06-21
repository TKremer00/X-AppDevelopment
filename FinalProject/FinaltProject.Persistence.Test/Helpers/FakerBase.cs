using FinaltProject.Persistence.Test.Helpers;

namespace FinalProject.Persistence.Test.Helpers
{
    public abstract class FakerBase<T> : IFaker<T>
    {
        public abstract T generate();

        public T[] generate(int amount)
        {
            var items = new List<T>();

            for (int i = 0; i < amount; i++)
            {
                items.Add(generate());
            }

            return items.ToArray();
        }
    }
}
