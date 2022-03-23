using System;

namespace Items
{
    [Serializable]
    public class ItemsToDrop
    {
        public Item itemsToDrop;
        public int count;
        public float chance;

        public ItemsToDrop(Item item, int count, float chance)
        {
            this.count = count;
            this.itemsToDrop = item;
            this.chance = chance;
        }
    }
}
