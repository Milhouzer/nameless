namespace Milhouzer.InventorySystem
{
    public struct RemoveItemOperation 
    {

        public RemoveItemOperationResult Result {get; private set;}
        public int Removed {get; private set;}

        public RemoveItemOperation(RemoveItemOperationResult result, int removed)
        {
            Result = result;
            Removed = removed;
        }

        public void CombineOperation(RemoveItemOperation other)
        {
            Removed += other.Removed;
            switch(other.Result)
            {
                case RemoveItemOperationResult.RemovedNone:
                    Result = Result == RemoveItemOperationResult.RemovedNone ? RemoveItemOperationResult.RemovedNone : RemoveItemOperationResult.PartiallyRemoved;
                    break;
                case RemoveItemOperationResult.RemovedAll:
                    Result = Result == RemoveItemOperationResult.RemovedAll ? RemoveItemOperationResult.RemovedAll : RemoveItemOperationResult.PartiallyRemoved;
                    break;
                case RemoveItemOperationResult.PartiallyRemoved:
                    Result = RemoveItemOperationResult.PartiallyRemoved;
                    break;
            }
        }

        public static RemoveItemOperation RemovedNone() => new RemoveItemOperation(RemoveItemOperationResult.RemovedNone, 0);        
    }
}
