using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;
using TiledSharp;
using System.Reflection.Metadata;

namespace MyPattern
{
    internal class SceneMap : Scene
    {
        private KeyboardState oldKeyboardState;
        private GamePadState oldGamePadState;
        private Button MyButton;
        private Song music;

        public TmxMap map;
        Texture2D tileset;
        Texture2D tilesetGrass;
        int tileWidth;
        int tileHeight;
        int mapWidth;
        int mapHeight;
        int tilesetLines;
        int tilesetColumns;
        // Grass
        int tileWidthGrass;
        int tileHeightGrass;
        int tilesetLinesGrass;
        int tilesetColumnsGrass;
        int lastTileset;


        public SceneMap(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneMap");
        }
        public void onClickPlay(Button pSender)
        {
            mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
        }
        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();
            Rectangle Screen = mainGame.Window.ClientBounds;
            music = mainGame.Content.Load<Song>("cool");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            MyButton = new Button(mainGame.Content.Load<Texture2D>("button"));
            MyButton.Position = new Vector2(
                Screen.Width / 2 - MyButton.Texture.Width / 2,
                Screen.Height / 2 - MyButton.Texture.Height / 2);
            MyButton.onClick = onClickPlay;
            listActors.Add(MyButton);

            Debug.WriteLine("SceneMenu.Load");

            map = new TmxMap("Content/MyMap.tmx");
            tileset = mainGame.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tilesetGrass = mainGame.Content.Load<Texture2D>(map.Tilesets[1].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            mapWidth = map.Width;
            mapHeight = map.Height;
            tilesetColumns = tileset.Width / tileWidth;
            tilesetLines = tileset.Height / tileHeight;
            //TXTilesetGrass
            tileWidthGrass = map.Tilesets[1].TileWidth;
            tileHeightGrass = map.Tilesets[1].TileHeight;
            mapWidth = map.Width;
            mapHeight = map.Height;
            tilesetColumnsGrass = tilesetGrass.Width / tileWidthGrass;
            tilesetLinesGrass = tilesetGrass.Height / tileHeightGrass;
            // Code pour 1 ou 2 tilsets à modifier pour plus de tilsets
            for (int nLayer = 0; nLayer < map.Layers.Count; nLayer++)
            {
                lastTileset = map.Tilesets[nLayer].FirstGid;
            }



            base.Load();
        }

        public override void UnLoad()
        {
            MediaPlayer.Stop();
            base.UnLoad();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState newMouseState = Mouse.GetState();
            if (newMouseState.LeftButton == ButtonState.Pressed)
            {

            }
            KeyboardState newKeyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                Debug.WriteLine("touche enter");
                mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
            }

            oldKeyboardState = newKeyboardState;
            //Debug.WriteLine("SceneMenu.Update");
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            int nbLayers = map.Layers.Count;
            int line;
            int column;
            for (int nLayer = 0; nLayer < nbLayers; nLayer++)
            {
                line = 0;
                column = 0;
                for (int i = 0; i < map.Layers[nLayer].Tiles.Count; i++)
                {

                    int gid = map.Layers[nLayer].Tiles[i].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        //murs
                        if (gid < lastTileset)
                        {
                            int tilesetColumn = tileFrame % tilesetColumns;
                            int tilesetLine =
                            (int)Math.Floor(
                            (double)tileFrame / (double)tilesetColumns
                            );
                            float x = column * tileWidth;
                            float y = line * tileHeight;
                            Rectangle tilesetRec =
                            new Rectangle(
                            tileWidth * tilesetColumn,
                            tileHeight * tilesetLine,
                            tileWidth, tileHeight);
                            mainGame.spriteBatch.Draw(
                            tileset,
                            new Vector2(x, y),
                            tilesetRec, Color.White);
                        }

                        //Grass
                        else
                        {
                            int tilesetColumnGrass = (tileFrame - lastTileset + 1) % tilesetColumnsGrass;
                            int tilesetLineGrass =
                            (int)Math.Floor(
                            (double)(tileFrame - lastTileset + 1) / (double)tilesetColumnsGrass
                            );
                            float xGrass = column * tileWidthGrass;
                            float yGrass = line * tileHeightGrass;
                            Rectangle tilesetRecGrass =
                            new Rectangle(
                            tileWidthGrass * tilesetColumnGrass,
                            tileHeightGrass * tilesetLineGrass,
                            tileWidthGrass, tileHeightGrass);
                            mainGame.spriteBatch.Draw(
                            tilesetGrass,
                            new Vector2(xGrass, yGrass),
                            tilesetRecGrass, Color.White);
                        }
                    }
                    column++;
                    if (column == mapWidth)
                    {
                        column = 0;
                        line++;
                    }
                }
            }



            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the map", new Vector2(1, 1), Color.White);
            base.Draw(gameTime);
        }


    }
}