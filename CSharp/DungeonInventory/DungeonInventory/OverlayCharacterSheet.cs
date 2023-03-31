using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    class OverlayCharacterSheet : Overlay
    {
        Character character;

        private Texture2D textureBG;
        private Texture2D textureHead;
        private Texture2D textureArmor;
        private Texture2D textureGloves;
        private Texture2D textureBoots;

        private Vector2 coordOverlay = new Vector2(60, 28);
        private Vector2 coordBody_Head = new Vector2(209, 103);
        private Vector2 coordBody_Armor = new Vector2(186, 129);
        private Vector2 coordBody_Gloves = new Vector2(175, 181);
        private Vector2 coordBody_Boots = new Vector2(179, 260);

        private Vector2 coordSlotHand = new Vector2(101, 188);
        private Vector2 coordSlotHead = new Vector2(291, 98);
        private Vector2 coordSlotArmor = new Vector2(291, 158);
        private Vector2 coordSlotShield = new Vector2(291, 218);
        private Vector2 coordSlotBoots = new Vector2(291, 278);

        private SoundEffect sndEquipArmor;
        private SoundEffect sndEquipWeapon;
        private SoundEffect sndEquipCloth;

        private Emitter ParticleEmitter;

        private string equipHead = "";
        private string equipHands = "";
        private string equipArmor = "";
        private string equipShield = "";
        private string equipGloves = "";
        private string equipBoots = "";

        public OverlayCharacterSheet(Game pGame, Character pCharacter) : base(pGame)
        {
            character = pCharacter;
            // Body parts
            textureBG = pGame.Content.Load<Texture2D>("CharSheet/SheetOverlay");
            textureHead = pGame.Content.Load<Texture2D>("CharSheet/body_head");
            textureArmor = pGame.Content.Load<Texture2D>("CharSheet/body_armor");
            textureGloves = pGame.Content.Load<Texture2D>("CharSheet/body_gloves");
            textureBoots = pGame.Content.Load<Texture2D>("CharSheet/body_boots");

            // Sounds
            sndEquipArmor = pGame.Content.Load<SoundEffect>("sounds/equip_armor");
            sndEquipWeapon = pGame.Content.Load<SoundEffect>("sounds/equip_weapon");
            sndEquipCloth = pGame.Content.Load<SoundEffect>("sounds/equip_cloth");

            ParticleEmitter = new Emitter(pGame);
        }

        public void Equip(string psEquipWith)
        {
            Item item = ItemData.Data[psEquipWith];
            Vector2 coordEmitter = new Vector2();
            switch (item.Equip)
            {
                case ItemData.eEquip.Head:
                    sndEquipArmor.Play();
                    equipHead = psEquipWith;
                    coordEmitter = coordSlotHead;
                    break;
                case ItemData.eEquip.Armor:
                    sndEquipArmor.Play();
                    equipArmor = psEquipWith;
                    coordEmitter = coordSlotArmor;
                    break;
                case ItemData.eEquip.Hands:
                    sndEquipWeapon.Play();
                    equipHands = psEquipWith;
                    coordEmitter = coordSlotHand;
                    break;
                case ItemData.eEquip.Shield:
                    sndEquipCloth.Play();
                    equipShield = psEquipWith;
                    coordEmitter = coordSlotShield;
                    break;
                case ItemData.eEquip.Gloves:
                    sndEquipCloth.Play();
                    equipGloves = psEquipWith;
                    break;
                case ItemData.eEquip.Finger:
                    break;
                case ItemData.eEquip.Boots:
                    sndEquipCloth.Play();
                    equipBoots = psEquipWith;
                    coordEmitter = coordSlotBoots;
                    break;
                default:
                    break;
            }
            if (coordEmitter.X != 0)
                ParticleEmitter.Start((int)coordEmitter.X + 43 / 2, (int)coordEmitter.Y + 43 / 2);
        }

        public override void Update()
        {
            ParticleEmitter.Update();
        }

        public override void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            pSpriteBatch.Draw(textureBG, coordOverlay, Color.White);

            ParticleEmitter.Draw(pSpriteBatch);

            // Body parts and equipment
            if (equipHands != "")
            {
                Texture2D texture = ItemTextures.Textures[equipHands];
                if (texture != null)
                {
                    pSpriteBatch.Draw(texture, coordSlotHand, Color.White);
                }
            }
            if (equipHead != "")
            {
                pSpriteBatch.Draw(textureHead, coordBody_Head, Color.White);
                Texture2D texture = ItemTextures.Textures[equipHead];
                if (texture != null)
                {
                    pSpriteBatch.Draw(texture, coordSlotHead, Color.White);
                }
            }
            if (equipArmor != "")
            {
                pSpriteBatch.Draw(textureArmor, coordBody_Armor, Color.White);
                Texture2D texture = ItemTextures.Textures[equipArmor];
                if (texture != null)
                {
                    pSpriteBatch.Draw(texture, coordSlotArmor, Color.White);
                }
            }
            if (equipShield != "")
            {
                Texture2D texture = ItemTextures.Textures[equipShield];
                if (texture != null)
                {
                    pSpriteBatch.Draw(texture, coordSlotShield, Color.White);
                }
            }
            if (equipBoots != "")
            {
                pSpriteBatch.Draw(textureBoots, coordBody_Boots, Color.White);
                Texture2D texture = ItemTextures.Textures[equipBoots];
                if (texture != null)
                {
                    pSpriteBatch.Draw(texture, coordSlotBoots, Color.White);
                }
            }

            // Stats
            string str;
            Vector2 sizeStr;

            str = "FORCE";
            sizeStr = pFont.MeasureString(str);
            pSpriteBatch.DrawString(pFont, str, new Vector2(187 - sizeStr.X, 364), Color.Gray);
            str = "DEXTERITE";
            sizeStr = pFont.MeasureString(str);
            pSpriteBatch.DrawString(pFont, str, new Vector2(187 - sizeStr.X, 384), Color.Gray);
            str = "CONSTITUTION";
            sizeStr = pFont.MeasureString(str);
            pSpriteBatch.DrawString(pFont, str, new Vector2(187 - sizeStr.X, 404), Color.Gray);

            pSpriteBatch.DrawString(pFont, character.Force.ToString(), new Vector2(200, 364), Color.Gray);
            pSpriteBatch.DrawString(pFont, character.Dextérité.ToString(), new Vector2(200, 384), Color.Gray);
            pSpriteBatch.DrawString(pFont, character.Constitution.ToString(), new Vector2(200, 404), Color.Gray);

            str = "INTELLIGENCE";
            sizeStr = pFont.MeasureString(str);
            pSpriteBatch.DrawString(pFont, str, new Vector2(320 - sizeStr.X, 364), Color.Gray);
            str = "SAGESSE";
            sizeStr = pFont.MeasureString(str);
            pSpriteBatch.DrawString(pFont, str, new Vector2(320 - sizeStr.X, 384), Color.Gray);
            str = "CHARISME";
            sizeStr = pFont.MeasureString(str);
            pSpriteBatch.DrawString(pFont, str, new Vector2(320 - sizeStr.X, 404), Color.Gray);

            pSpriteBatch.DrawString(pFont, character.Intelligence.ToString(), new Vector2(330, 364), Color.Gray);
            pSpriteBatch.DrawString(pFont, character.Sagesse.ToString(), new Vector2(330, 384), Color.Gray);
            pSpriteBatch.DrawString(pFont, character.Charisme.ToString(), new Vector2(330, 404), Color.Gray);

            base.Draw(pSpriteBatch, pFont);
        }
    }
}
