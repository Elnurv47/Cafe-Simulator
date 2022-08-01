public interface IItemHolder
{
    bool CanPut(StorableItem item);
    void Put(StorableItem item);
}
