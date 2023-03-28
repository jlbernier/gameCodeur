using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using TiledSharp;

namespace TileMap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        // Map
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


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            map = new TmxMap("Content/myMapTile.tmx");
            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tilesetGrass = Content.Load<Texture2D>(map.Tilesets[1].Name.ToString());
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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
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
                            spriteBatch.Draw(
                            tileset,
                            new Vector2(x, y),
                            tilesetRec, Color.White);
                        }

                        //Grass
                        else {
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
                            spriteBatch.Draw(
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
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}