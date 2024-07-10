namespace Milhouzer.InventorySystem
{
    public struct RemoveItemOperation 
    {

        public RemoveItemOperationResult Result {get; private set;}
        public int Removed {get; private set;}
        public IItem Item {get; private set;}

        public RemoveItemOperation(RemoveItemOperationResult result, IItem item, int removed)
        {
            Result = result;
            Removed = removed;
            Item = item;
        }

        /// <summary>
        /// Combine this operation to another
        /// </summary>
        /// <param name="other">Operation to combine with</param>
        public void CombineOperation(RemoveItemOperation other)
        {
            Removed += other.Removed;
            Result = Result != other.Result ? RemoveItemOperationResult.PartiallyRemoved : Result;
        }

        public static RemoveItemOperation RemovedNone() => new(RemoveItemOperationResult.RemovedNone, null, 0);        
    }
}
