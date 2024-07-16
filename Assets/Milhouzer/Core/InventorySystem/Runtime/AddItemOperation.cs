namespace Milhouzer.Core.InventorySystem
{
    public struct AddItemOperation
    {
        public AddItemOperationResult Result {get; private set;}
        public int Added {get; private set;}
        
        /// <TODO>
        /// Keep this value or just pass item name ?
        /// </TODO>
        public IItem Item {get; private set;}


        public AddItemOperation(AddItemOperationResult result, IItem item, int added)
        {
            Result = result;
            Added = added;
            Item = item;
        }

        /// <summary>
        /// Combine this operation to another
        /// </summary>
        /// <param name="other">Operation to combine with</param>
        public void CombineOperation(AddItemOperation other)
        {
            Added += other.Added;
            Result = Result != other.Result ? AddItemOperationResult.PartiallyAdded : Result;
        }

        public static AddItemOperation AddedNone() => new(AddItemOperationResult.AddedNone, null, 0); 
    }
}
