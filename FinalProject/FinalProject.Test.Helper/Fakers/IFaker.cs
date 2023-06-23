namespace FinaltProject.Test.Helper.Fakers
{
    public interface IFaker<T>
    {
        T generate();

        T[] generate(int amount);
    }
}
