using DungeonInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonInventory
{
    public class InventoryManager
    {
        private List<Item> lstInventory;
        public const int MAXITEM = 7 * 7;

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

        public bool AddObject(string pItemID, int pQuantity)
        {
            if (lstInventory.Count >= MAXITEM)
                return false;

            if (ItemData.Data.ContainsKey(pItemID))
            {
                Item item = new Item(ItemData.Data[pItemID]);
                item.Quantity = pQuantity;
                int slot = -1;
                for (int i = 0; i < MAXITEM; i++)
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

        public void RemoteItem(int pSlot)
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

        public List<Item> GetObjectList()
        {
            return lstInventory;
        }
    }
}
