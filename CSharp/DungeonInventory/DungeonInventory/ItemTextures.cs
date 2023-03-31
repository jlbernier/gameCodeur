using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public static class ItemTextures
    {
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public static void PopulateTextures(Game pGame)
        {
            Textures.Add("HAMMER", pGame.Content.Load<Texture2D>("icons/2HHammer_normal"));
            Textures.Add("ARROW", pGame.Content.Load<Texture2D>("icons/Arrow"));
            Textures.Add("AXEBATTLE", pGame.Content.Load<Texture2D>("icons/Axe_Battle"));
            Textures.Add("BOOTCLOTH", pGame.Content.Load<Texture2D>("icons/Bootc_Cloth"));
            Textures.Add("ARMORCLOTHLUXURY", pGame.Content.Load<Texture2D>("icons/Chest_Cloth_Luxury"));
            Textures.Add("ARMORCLOTHTORN", pGame.Content.Load<Texture2D>("icons/Chest_Cloth_Torn"));
            Textures.Add("DAGGER", pGame.Content.Load<Texture2D>("icons/Dagger"));
            Textures.Add("HELMETPLATE", pGame.Content.Load<Texture2D>("icons/Head_Plate"));
            Textures.Add("HELMETPLATEU", pGame.Content.Load<Texture2D>("icons/Head_Plate_Upgraded"));
            Textures.Add("SHIELDSTEEL", pGame.Content.Load<Texture2D>("icons/Shield_Steel2"));
        }
    }
}
