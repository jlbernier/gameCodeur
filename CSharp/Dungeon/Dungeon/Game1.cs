using GameCodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

using xnai = Microsoft.Xna.Framework.Input;

namespace Dungeon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Model cubeModel;

        VertexPositionNormalTexture[] quadVertices;
        AlphaTestEffect mySpriteEffect;

        private FrameAnimation Monster1;

        private Vector3 cameraPosition;

        private Matrix modelMatrix; // world
        private Matrix view;
        private Matrix projection;

        private float camDirection = 0;
        MouseState originalMouseState;

        int[,] mapData = new int[,]
        {
            { 1,1,1,1,1,1,1,1,1,1 },
            { 1,0,2,0,0,0,0,1,1,1 },
            { 1,0,0,0,0,2,0,0,0,1 },
            { 1,1,0,1,1,1,0,1,0,1 },
            { 1,1,0,1,1,1,2,1,2,1 },
            { 1,1,0,1,1,1,0,1,1,1 },
            { 1,0,0,2,0,0,0,0,0,1 },
            { 1,0,1,1,1,1,0,1,0,1 },
            { 1,2,0,0,0,1,0,0,2,1 },
            { 1,0,1,1,1,1,1,1,1,1 }
        };

        // Text & HUD
        private KeyboardState oldKBState;
        public SpriteFont Font { get; private set; }

        private OverlayMapEditor mapEditor;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private void CenterMouse()
        {
            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //            RasterizerState rasterizerState = new RasterizerState();
            //            rasterizerState.CullMode = CullMode.None;
            //            graphics.GraphicsDevice.RasterizerState = rasterizerState;

            this.Window.Title = "Dungeon Master 3D - Atelier (c) Gamecodeur";

            // Our quad
            quadVertices = new VertexPositionNormalTexture[4];
            quadVertices[0].Position = new Vector3(-1, -1, 0); // Lower left
            quadVertices[1].Position = new Vector3(-1, 1, 0); // Upper left
            quadVertices[2].Position = new Vector3(1, -1, 0); // Lower right
            quadVertices[3].Position = new Vector3(1, 1, 0); // Upper right

            quadVertices[0].TextureCoordinate = new Vector2(0, 1); // Lower left
            quadVertices[1].TextureCoordinate = new Vector2(0, 0); // Upper left
            quadVertices[2].TextureCoordinate = new Vector2(1, 1); // Lower right
            quadVertices[3].TextureCoordinate = new Vector2(1, 0); // Upper right

            // The 3D model
            modelMatrix = Matrix.Identity;

            cameraPosition = new Vector3(0, 0, 10);

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                16f/9f,
                1f,
                100f
                );

            // Center the mouse and get state
            CenterMouse();
            originalMouseState = Mouse.GetState();

            mapEditor = new OverlayMapEditor(this, 5, ref mapData);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cubeModel = Content.Load<Model>("cube");

            Monster1 = new FrameAnimation(100);
            for (int i = 1; i < 7; i++)
            {
                Monster1.AddTexture(Content.Load<Texture2D>("Anim_char7_idle_0" + i));
            }

            mySpriteEffect = new AlphaTestEffect(GraphicsDevice)
            {
                Texture = null,
                FogEnabled = true,
                FogStart = 2,
                FogEnd = 20
            };

            // Font & HUD
            Font = Content.Load<SpriteFont>("mainfont");

            // Map Editor
            for (int i = 0; i < 5; i++)
            {
                mapEditor.AddTile(i, Content.Load<Texture2D>("tile_" + i));
            }
            mapEditor.UpdateGrid();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            bool shift = Keyboard.GetState().IsKeyDown(xnai.Keys.LeftShift) || Keyboard.GetState().IsKeyDown(xnai.Keys.RightShift);

            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                camDirection -= 0.001f * xDifference;
                CenterMouse();
            }

            // Rotate Camera with keyboard
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.Q) && shift)  // left
            {
                camDirection += .01f;
            }
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.D) && shift)  // right
            {
                camDirection -= .01f;
            }

            // Move camera up & down
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.Up))
            {
                cameraPosition += new Vector3(0, .05f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.Down))
            {
                cameraPosition += new Vector3(0, -.05f, 0);
            }

            // The Vector3.Transform() method just multiplies a matrix with a vector

            // Move Camera
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.Q) && !shift)  // left
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(-speed, 0, 0);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.D) && !shift)  // right
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(speed, 0, 0);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.Z))  // forward
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(0, 0, -speed);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }
            if (Keyboard.GetState().IsKeyDown(xnai.Keys.S))  // backward
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(0, 0, speed);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }

            Matrix rotationMatrix = Matrix.CreateRotationY(camDirection);
            Vector3 forwardNormal = rotationMatrix.Forward;

            view = Matrix.CreateLookAt(
                cameraPosition,
                cameraPosition + (forwardNormal * 10f),
                Vector3.Up
                );

            Monster1.Update(gameTime);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == xnai.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(xnai.Keys.Escape))
            {
                if (!mapEditor.isActive)
                    Exit();
            }

            if (!mapEditor.isActive)
            {
                UpdateGameplay(gameTime);
            }
            else
            {
                if (mapEditor.isActive)
                    mapEditor.Update();
            }

            KeyboardState newKBState = Keyboard.GetState();

            if (newKBState.IsKeyDown(xnai.Keys.Tab) && !oldKBState.IsKeyDown(xnai.Keys.Tab))
            {
                mapEditor.Active();
                IsMouseVisible = mapEditor.isActive;
                CenterMouse();
            }

            if (newKBState.IsKeyDown(xnai.Keys.S) && !oldKBState.IsKeyDown(xnai.Keys.S) && mapEditor.isActive)
            {
                // Show the dialog and get result.
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = ".map";
                saveFileDialog.Filter = "*Map Json|*.map";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    Console.WriteLine(saveFileDialog.FileName);
                    MapJSON json = new MapJSON();
                    json.ConvertForWrite(mapData);
                    MemoryStream stream = new MemoryStream();
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MapJSON));
                    ser.WriteObject(stream, json);
                    stream.Position = 0;
                    StreamReader sr = new StreamReader(stream);
                    string strMap = sr.ReadToEnd();
                    Debug.WriteLine(strMap);
                    File.WriteAllText(saveFileDialog.FileName, strMap);
                }
            }

            if (newKBState.IsKeyDown(xnai.Keys.O) && !oldKBState.IsKeyDown(xnai.Keys.O))
            {
                // Show the dialog and get result.
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "*Map Json|*.map";
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    Console.WriteLine(openFileDialog.FileName);
                    // Creation d'un MemoryStream depuis un fichier
                    MapJSON mapJSON;
                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(openFileDialog.FileName))))
                    {
                        // Desérialisation 
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MapJSON));
                        mapJSON = (MapJSON)ser.ReadObject(stream);
                        mapJSON.ConvertForRead(ref mapData);
                        mapEditor.UpdateGrid();
                        CenterMouse();
                    }
                }
            }

            oldKBState = newKBState;

            base.Update(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {

            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.FogEnabled = true;
                    effect.FogStart = 2;
                    effect.FogEnd = 20;
                }
                mesh.Draw();
            }

        }

        private void DrawSprite3D(VertexPositionNormalTexture[] pQuad, Vector3 quadPosition, Texture2D pTexture, Matrix pWorld, Matrix pView, Matrix pProjection)
        {
            mySpriteEffect.View = pView;
            mySpriteEffect.Projection = pProjection;

            Vector3 directionVector = Vector3.Normalize(quadPosition - cameraPosition);
            Matrix lookAt = Matrix.CreateWorld(quadPosition, directionVector, Vector3.Up);
            mySpriteEffect.World = lookAt;

            mySpriteEffect.Texture = pTexture;

            // No need for this, but it will work with advanced effect with multiple passes
            foreach (var pass in mySpriteEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                    PrimitiveType.TriangleStrip,
                    pQuad,
                    0,
                    2);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            int mapPosX = -10;
            int mapPosZ = -20;
            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 10; column++)
                {
                    int id = mapData[line, column];
                    int x = mapPosX + (column * 2);
                    int z = mapPosZ + (line * 2);
                    if (id == 1)
                    {
                        Matrix wallMatrix = Matrix.CreateWorld(new Vector3(x,0,z), Vector3.Forward, Vector3.Up);
                        DrawModel(cubeModel, wallMatrix, view, projection);
                    }
                    else if (id == 2)
                    {
                        DrawSprite3D(quadVertices, new Vector3(x, 0, z), Monster1.getTexture(), Matrix.Identity, view, projection);
                    }
                }
            }

            spriteBatch.Begin();

            if (mapEditor.isActive)
                mapEditor.Draw(spriteBatch, Font);

            spriteBatch.End();

            // Set the depth buffer on again
            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            base.Draw(gameTime);
        }
    }
}
