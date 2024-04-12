namespace Milhouzer.InventorySystem
{
    public struct AddItemOperation
    {
        public AddItemOperationResult Result {get; private set;}
        public int Added {get; private set;}

        public AddItemOperation(AddItemOperationResult result, int added)
        {
            Result = result;
            Added = added;
        }

        public static AddItemOperation AddedNone() => new AddItemOperation(AddItemOperationResult.AddedNone, 0); 
    }
}
