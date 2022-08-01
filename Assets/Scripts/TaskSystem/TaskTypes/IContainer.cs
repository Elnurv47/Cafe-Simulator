namespace TaskSystem
{
    public interface IContainer
    {
        bool ContainsFood();
        StorableItem GetStorableItem();
    }
}
