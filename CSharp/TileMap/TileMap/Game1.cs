using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        int tileWidth;
        int tileHeight;
        int mapWidth;
        int mapHeight;
        int tilesetLines;
        int tilesetColumns;


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
            //tileset = Content.Load<Texture2D>("tileset");
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            mapWidth = map.Width;
            mapHeight = map.Height;
            tilesetColumns = tileset.Width / tileWidth;
            tilesetLines = tileset.Height / tileHeight;

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