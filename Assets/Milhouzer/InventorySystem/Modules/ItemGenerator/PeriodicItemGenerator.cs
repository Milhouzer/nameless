using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public class PeriodicItemGenerator : MonoBehaviour, IItemGenerator 
    {
        [SerializeField]
        protected float _period;
        public float Period => _period;

        protected int _count;
        [SerializeField]
        protected int _maxCount;
        public int MaxCount => _maxCount;

        [SerializeField]
        protected ItemDropTable _table;
        public ItemDropTable Table => _table;

        protected float timer = 0;

        private void Update() {
            timer += Time.deltaTime;
            if(timer >= _period)
            {
                if(CanGenerate())
                {
                    Generate();
                }
                timer-=_period;
            }    
        }

        protected void PickedUp()
        {
            _count--;
        }

        public bool CanGenerate()
        {
           return _count < MaxCount;
        }

        public virtual void Generate()
        {
            BaseItemData generated = _table.GetRandomElement();

            GameObject droppedItemGO = InventoryManager.DropItem(new ItemStack(generated, 1), GetGenerationPosition(), GetGenerationRotation(), transform);
            DroppedItem droppedItem = droppedItemGO.GetComponent<DroppedItem>();

            droppedItem.OnPickedUp += PickedUp;
            _count++;
        }
        
        public virtual Vector3 GetGenerationPosition()
        {
            Vector2 rd = Random.insideUnitCircle;

            return transform.position + new Vector3(rd.x, 0.5f , rd.y);
        }

        public virtual Quaternion GetGenerationRotation()
        {
            return Quaternion.identity;
        }
    }
}