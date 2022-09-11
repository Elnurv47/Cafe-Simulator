namespace TaskSystem
{
    public interface IContainer
    {
        bool Contains();
        StorableItem GetStorableItem();
    }
}
