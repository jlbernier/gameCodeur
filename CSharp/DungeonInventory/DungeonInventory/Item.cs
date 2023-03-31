using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonInventory
{
    public class Item
    {
        public string ID;
        public string Name;
        public int HP;
        public bool Collectible;
        public int MaxPerSlot;
        public int Quantity;
        public int InventorySlot;
        public ItemData.eItemType Type;
        public ItemData.eEquip Equip;

        public Item() 
        { 

        }
        public Item(Item pCopy) 
        {
            ID = pCopy.ID;
            Name = pCopy.Name;
            HP = pCopy.HP;
            Collectible = pCopy.Collectible;
            MaxPerSlot = pCopy.MaxPerSlot;
            Quantity = pCopy.Quantity;
            InventorySlot = pCopy.InventorySlot;

        }
    }
    public static class ItemData
    {
        public enum eItemType
        {
            None,
            Tool,
            WeaponMelee,
            WeaponMissile,
            WeaponBow,
            Helmet,
            Armor,
            Shield,
            Cloth,
            Ammunition
        }
        public enum eEquip
        {
            None,
            Head,
            Armor,
            Shield,
            Hand,
            Finger,
            Wrists,
            Feet
        }
        public static Dictionary<string, Item> Data = new Dictionary<string, Item>();
  
        public static void PopulateData()
        {
            Data.Add("HAMMER", new Item { ID = "HAMMER", Type = eItemType.WeaponMelee, Equip = eEquip.Hand, Name = "Hammer", Collectible = false, HP = 4 });
            Data.Add("ARROW", new Item { ID = "ARROW", Type = eItemType.Ammunition, Equip = eEquip.None, Name = "Arrows", Collectible = true, MaxPerSlot = 10, HP = 4 });
            Data.Add("AXEBATTLE", new Item { ID = "AXEBATTLE", Type = eItemType.WeaponMelee, Equip = eEquip.Hand, Name = "Battle Axe", Collectible = false, HP = 8 });
            Data.Add("HELMETPLATE", new Item { ID = "HELMETPLATE", Type = eItemType.Helmet, Equip = eEquip.Head, Name = "Helmet Plate", Collectible = false });
            Data.Add("HELMETPLATEU", new Item { ID = "HELMETPLATEU", Type = eItemType.Helmet, Equip = eEquip.Head, Name = "Helmet Plate Up.", Collectible = false });
            Data.Add("BOOTCLOTH", new Item { ID = "BOOTCLOTH", Type = eItemType.Cloth, Equip = eEquip.Feet, Name = "Boots basic", Collectible = false });
            Data.Add("ARMORCLOTHLUXURY", new Item { ID = "ARMORCLOTHLUXURY", Type = eItemType.Armor, Equip = eEquip.Armor, Name = "Cloth basic", Collectible = false });
            Data.Add("ARMORCLOTHTORN", new Item { ID = "ARMORCLOTHTORN", Type = eItemType.Armor, Equip = eEquip.Armor, Name = "Cloth torn", Collectible = false });
            Data.Add("DAGGER", new Item { ID = "DAGGER", Type = eItemType.WeaponMelee, Equip = eEquip.Hand, Name = "Dagger", Collectible = false, HP = 6 });
            Data.Add("SHIELDSTEEL", new Item { ID = "SHIELDSTEEL", Type = eItemType.Shield, Equip = eEquip.Shield, Name = "Bouclier", Collectible = false });
        }


    }
}
