namespace FinaltProject.Persistence.Test.Helpers
{
    public interface IFaker<T>
    {
        T generate();

        T[] generate(int amount);
    }
}
