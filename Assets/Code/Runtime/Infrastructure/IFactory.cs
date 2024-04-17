namespace Code.Runtime.Infrastructure
{
    public interface IFactory<T>
    {
        T Create();
    }
}