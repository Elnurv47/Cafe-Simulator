namespace TaskSystem
{
    public interface IContainer
    {
        bool ContainsFood();
        IConsumable GetConsumable();
    }
}
