using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MTemplates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Templates;

namespace MyPattern
{
    class Hero : Sprite
    {
        public float energy;
        public Hero(Texture2D pTexture) : base(pTexture)
        {
            energy = 100;
        }
        public override void TouchBy(IActor pBy)
        {
            if (pBy is Meteor)
            {
                energy -= 10;
            }

        }
    }
    class Meteor : Sprite
    {

        public Meteor(Texture2D pTexture) : base(pTexture)
        {
            do
            {
                velovityX = (float)Util.GetInt(-3, 3) / 5;
            } while (velovityX == 0);
            do
            {


                velovityY = (float)Util.GetInt(-3, 3) / 5;
            } while (velovityY == 0);
        }
    }
    internal class SceneGamePlay : Scene
    {
        private KeyboardState oldKeyboardState;
        private Hero MyShip;
        private Song music;
        private SoundEffect soundExplode;
        public SceneGamePlay(MainGame pGame) : base(pGame)
        {
            music = AssetManager.MusicGamePlay;
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }
        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();
            //Debug.WriteLine("SceneGamePlay.Load");
            Rectangle Screen = mainGame.Window.ClientBounds; // taille de la fenetre de jeu
            soundExplode = mainGame.Content.Load<SoundEffect>("explode");
            // MediaPlayer.Play(music);

            MyShip = new Hero(mainGame.Content.Load<Texture2D>("ship"));
            MyShip.Position = new Vector2(
                Screen.Width / 2 - MyShip.Texture.Width / 2,
                Screen.Height / 2 - MyShip.Texture.Height / 2);
            listActors.Add(MyShip);
            for (int i = 0; i < 20; i++)
            {
                Meteor m = new Meteor(mainGame.Content.Load<Texture2D>("meteor"));
                //do
                //{
                    m.Position = new Vector2(
                        Util.GetInt(1, Screen.Width - m.Texture.Width),
                        Util.GetInt(1, Screen.Height - m.Texture.Height)
                        );
                    Debug.WriteLine("Myship: " + MyShip.BoundingBox.ToString() + "Meteor: " + m.BoundingBox.ToString());
                    listActors.Add(m);
                //} while (!m.BoundingBox.Intersects(MyShip.BoundingBox));
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
            KeyboardState newKeyboardState = Keyboard.GetState();
            Rectangle Screen = mainGame.Window.ClientBounds;
            foreach (IActor actor in listActors)
            {
                if (actor is Meteor m) //Meteor m = (Meteor)actor;
                {
                    if (m.Position.X < 0)
                    {
                        m.Position = new Vector2(0, m.Position.Y);
                        m.velovityX = 0 - m.velovityX;
                    }
                    if (m.Position.X > Screen.Width - m.Texture.Width)
                    {
                        m.Position = new Vector2(Screen.Width - m.Texture.Width, m.Position.Y);
                        m.velovityX = 0 - m.velovityX;
                    }
                    if (m.Position.Y < 0)
                    {
                        m.Position = new Vector2(m.Position.X, 0);
                        m.velovityY = 0 - m.velovityY;
                    }
                    if (m.Position.Y > Screen.Height - m.Texture.Height)
                    {
                        m.Position = new Vector2(m.Position.X, Screen.Height - m.Texture.Height);
                        m.velovityY = 0 - m.velovityY;
                    }
                    if (Util.CollideByBox(m, MyShip))
                    {
                        MyShip.TouchBy(m);
                        m.TouchBy(MyShip);
                        m.ToRemove = true;
                        soundExplode.Play();
                    }
                }
            }
            Clean();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))// && !oldKeyboardState.IsKeyDown(Keys.Left))
            {
                MyShip.Move(-1f, 0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))// && !oldKeyboardState.IsKeyDown(Keys.Right))
            {
                MyShip.Move(1f, 0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))// && !oldKeyboardState.IsKeyDown(Keys.Right))
            {
                MyShip.Move(0f, -1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))// && !oldKeyboardState.IsKeyDown(Keys.Right))
            {
                MyShip.Move(0f, 1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                Debug.WriteLine("touche space");
            }
            if (MyShip.energy <= 0)
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.GameOver);
            }

            //Debug.WriteLine("SceneGamePlay.Update");
            oldKeyboardState = newKeyboardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Debug.WriteLine("SceneGamePlay.Draw");
            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
               "GamePlay - Energy: " + MyShip.energy, new Vector2(1, 1), Color.White);
            base.Draw(gameTime);
        }


    }
}