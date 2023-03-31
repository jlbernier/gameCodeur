using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class InventoryManager
    {
        private List<Item> lstInventory;
        public const int MAXITEMS = 7 * 7;

        public InventoryManager()
        {
            lstInventory = new List<Item>();
        }

        public Item GetItemAt(int pSlot)
        {
            foreach (Item item in lstInventory)
            {
                if (item.InventorySlot == pSlot)
                    return item;
            }
            return null;
        }

        public void RemoveItem(int pSlot)
        {
            foreach (Item item in lstInventory)
            {
                if (item.InventorySlot == pSlot)
                {
                    lstInventory.Remove(item);
                    break;
                }
            }
        }

        public bool AddObject(string pItemID, int pQuantity)
        {
            if (lstInventory.Count >= MAXITEMS)
            {
                return false;
            }
            if (ItemData.Data.ContainsKey(pItemID))
            {
                Item item = new Item(ItemData.Data[pItemID]);
                item.Quantity = pQuantity;
                int slot = -1;
                // Which slot?
                for (int i = 0; i < MAXITEMS; i++)
                {
                    if (GetItemAt(i) == null)
                    {
                        slot = i;
                        break;
                    }
                }
                if (slot != -1)
                {
                    item.InventorySlot = slot;
                    lstInventory.Add(item);
                    return true;
                }
            }
            return false;
        }

        public List<Item> GetObjectList()
        {
            return lstInventory;
        }
    }
}
