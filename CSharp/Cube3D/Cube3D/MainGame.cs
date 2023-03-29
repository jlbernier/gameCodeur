using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MapTools;
using System.Windows.Forms;
using System.Text.Json;
using xi = Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Cube3D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Model cubeModel;

        private VertexPositionNormalTexture[] quadVertices;
        private AlphaTestEffect mySpriteEffect;

        private FrameAnimation monster;

        private Vector3 cameraPosition;

        private Matrix modelMatrix; // world
        private Matrix view;
        private Matrix projection;

        private float camDirection = 0;
        private MouseState originalMouseState;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;


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

        private KeyboardState oldKBState;
        private MapEditor mapEditor;
        private JSONTools jsonTools;

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
            // see quads from behind also
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState;

            //our quad
            quadVertices = new VertexPositionNormalTexture[4];
            quadVertices[0].Position = new Vector3 (-1,-1,0); //lower left
            quadVertices[1].Position = new Vector3 (-1,1,0);  //upper left
            quadVertices[2].Position = new Vector3 (1,-1,0);  //lower right
            quadVertices[3].Position = new Vector3 (1,1,0);   //upper right

            quadVertices[0].TextureCoordinate = new Vector2(0, 1); // Lower left
            quadVertices[1].TextureCoordinate = new Vector2(0, 0); // Upper left
            quadVertices[2].TextureCoordinate = new Vector2(1, 1); // Lower right
            quadVertices[3].TextureCoordinate = new Vector2(1, 0); // Upper right


            // The 3D model
            modelMatrix = Matrix.Identity;

            cameraPosition = new Vector3(0, 0, 10);

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                16f / 9f,
                1f,
                100f
                );

            CenterMouse();
            originalMouseState = Mouse.GetState();
            mapEditor = new MapEditor(this, 5, ref mapData);

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

            monster = new FrameAnimation(100);
            for (int i = 1; i < 7; i++)
            {

                monster.AddTexture(Content.Load<Texture2D>("Anim_char7_idle_0" + i));
            }
            mySpriteEffect = new AlphaTestEffect(GraphicsDevice)
            {
                Texture = monster.getTexture(),
                FogEnabled = true,
                FogStart = 2,
                FogEnd = 20
            };
            for (int i = 0; i <5; i++)
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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected void UpdateGameplay(GameTime gameTime)
        {
            bool shift = Keyboard.GetState().IsKeyDown(xi.Keys.LeftShift) || Keyboard.GetState().IsKeyDown(xi.Keys.RightShift);

            // TODO: Add your update logic here

            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                camDirection -= 0.001f * xDifference;
                CenterMouse();
            }

            // Rotate Camera with keyboard
            if (Keyboard.GetState().IsKeyDown(xi.Keys.Q) && shift)  // left
            {
                camDirection += .01f;
            }
            if (Keyboard.GetState().IsKeyDown(xi.Keys.D) && shift)  // right
            {
                camDirection -= .01f;
            }

            // Rotate Camera with keyboard
            if (Keyboard.GetState().IsKeyDown(xi.Keys.Q) && !shift)  // left
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(-speed, 0, 0);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }
            if (Keyboard.GetState().IsKeyDown(xi.Keys.D) && !shift)  // right
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(speed, 0, 0);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }
            if (Keyboard.GetState().IsKeyDown(xi.Keys.Z))  // forward
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(0, 0, -speed);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }
            if (Keyboard.GetState().IsKeyDown(  xi.Keys.S))  // backward
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                float speed = .1f;
                Vector3 v = new Vector3(0, 0, speed);
                v = Vector3.Transform(v, forwardMovement);
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;
            }

            // Move camera up & down
            if (Keyboard.GetState().IsKeyDown(xi.Keys.Up))
            {
                cameraPosition += new Vector3(0, .05f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(xi.Keys.Down))
            {
                cameraPosition += new Vector3(0, -.05f, 0);
            }

            Matrix rotationMatrix = Matrix.CreateRotationY(camDirection);
            Vector3 forwardNormal = rotationMatrix.Forward;

            view = Matrix.CreateLookAt(
                cameraPosition,
                cameraPosition + (forwardNormal * 10f),
                Vector3.Up
                );

            monster.Update(gameTime);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==  xi.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(xi.Keys.Escape))
                Exit();

            KeyboardState newKBState = Keyboard.GetState();
            if (newKBState.IsKeyDown(xi.Keys.Tab) && !oldKBState.IsKeyDown(xi.Keys.Tab))
            {
                mapEditor.Active();
                IsMouseVisible = mapEditor.isActive;
                CenterMouse();
            }

            if (newKBState.IsKeyDown(xi.Keys.S) && !oldKBState.IsKeyDown(xi.Keys.S))
            {
                string selectedPath = "";

                Thread t = new Thread((ThreadStart)(() =>
                {
                    saveFileDialog = new SaveFileDialog(); //
                    saveFileDialog.InitialDirectory = "C:\\Users\\33677\\Documents\\projets\\git\\gameCodeur\\CSharp\\Cube3D\\Cube3D\\Content";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.DefaultExt = ".map";
                    saveFileDialog.Filter = "Map Editor JSON (*.map)|*.map";
                    //saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 2;

                    DialogResult result = saveFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Debug.WriteLine("File Name: " + saveFileDialog.FileName);
                        MapJSON json = new MapJSON();
                        json.ConvertForWrite(mapData);
                        MemoryStream stream = new MemoryStream();
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MapJSON));
                        ser.WriteObject(stream, json);
                        stream.Position = 0;
                        StreamReader sr = new StreamReader(stream);
                        string strmap = sr.ReadToEnd();
                        Debug.WriteLine(strmap);
                        File.WriteAllText(saveFileDialog.FileName, strmap);
                        //sr.Close();

                    }
                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
                
                // e.g C:\Users\MyName\Desktop\myfile.json
                Console.WriteLine(selectedPath);
                           
            }

            if (newKBState.IsKeyDown(xi.Keys.O) && !oldKBState.IsKeyDown(xi.Keys.O))
            {
                string selectedOpenPath = "";
                Debug.WriteLine("File Open !!!!!!! ");
                Thread t = new Thread((ThreadStart)(() =>
                {
                    openFileDialog = new OpenFileDialog(); //
                    openFileDialog.InitialDirectory = "C:\\Users\\33677\\Documents\\projets\\git\\gameCodeur\\CSharp\\Cube3D\\Cube3D\\Content";
                    openFileDialog.AddExtension = true;
                    openFileDialog.DefaultExt = ".map";
                    openFileDialog.Filter = "Map Editor JSON (*.map)|*.map";
                    //saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;

                    DialogResult result = openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Debug.WriteLine("File Name: " + openFileDialog.FileName);
                        //jsonTools.ReadJSON(ref mapData, openFileDialog.FileName);
                        MapJSON json;
                        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(openFileDialog.FileName)));
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MapJSON));
                        json = (MapJSON) ser.ReadObject(stream);
                        json.ConvertForRead(ref mapData);
                        mapEditor.UpdateGrid();
                        CenterMouse();  
                    }
                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
            }
            if (!mapEditor.isActive)
            {
                UpdateGameplay(gameTime);
            }
            mapEditor.Update();
            oldKBState = newKBState;

            base.Update(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {

            foreach (ModelMesh mesh in model.Meshes)
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

        private void DrawSprite3D(VertexPositionNormalTexture[] pQuad, Vector3 pQuadPosition, Texture2D pTexture, Matrix pView, Matrix pProjection)
        {
            mySpriteEffect.View = pView;
            mySpriteEffect.Projection = pProjection;
            Vector3 directionVector = Vector3.Normalize(pQuadPosition - cameraPosition);
            Matrix lookAt = Matrix.CreateWorld(pQuadPosition, directionVector, Vector3.Up);
            mySpriteEffect.World = lookAt;
   
            mySpriteEffect.Texture = pTexture;

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
            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp; //not blur when zoom

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
                        Matrix wallMatrix = Matrix.CreateWorld(new Vector3(x, 0, z), Vector3.Forward, Vector3.Up);
                        DrawModel(cubeModel, wallMatrix, view, projection);
                    }
                    else if (id == 2)
                    {
                        DrawSprite3D(quadVertices, new Vector3(x, 0, z), monster.getTexture(), view, projection);
                        //DrawSprite3D(quadVertices, new Vector3(x, 0, z), monsterTexture, view, projection);
                        //DrawSprite3D(quadVertices, Matrix.Identity, view, projection);
                    }
                }

                base.Draw(gameTime);
                spriteBatch.Begin();
                mapEditor.Draw(spriteBatch);
                spriteBatch.End();
                GraphicsDevice.DepthStencilState = new DepthStencilState()
                {
                    DepthBufferEnable = true
                };
            }
        }
    }
}
