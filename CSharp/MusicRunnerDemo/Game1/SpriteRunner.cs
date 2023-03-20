using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicRunnerDemo;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MusicRunnerDemo
{
    class SpriteRunner : GCSprite
    {
        public bool Jumping { get; set; }
        public float Gravity { get; set; }
        private SoundEffect _sndJump;
        private SoundEffect _sndLanding;
        private int _GroundPosition;
        public SpriteRunner(SpriteBatch pSpriteBatch, Texture2D pTexture) : base(pSpriteBatch, pTexture, 20, 20)
        {
            Jumping = false;
            Gravity = 10;
        }

        public void SetSounds(SoundEffect pSndJump, SoundEffect pSndLanding)
        {
            _sndJump = pSndJump;
            _sndLanding = pSndLanding;
        }

        public void SetGroundPosition(int pPosition)
        {
            y = pPosition;
            _GroundPosition = pPosition;
        }

        public void Jump()
        {
            if (Jumping) return;
            Velocity.Y = -400;
            Jumping = true;
            // Play a sound
            if (_sndJump != null)
                _sndJump.Play();
        }

        public override void Update(GameTime gameTime)
        {
            // Test if the hero is on the ground
            if (Jumping && y > _GroundPosition)
            {
                // We fix the vertical position to the ground
                y = _GroundPosition;
                // Stop jumping now!
                Jumping = false;
                Velocity.Y = 0;
                // Sound effect
                if (_sndLanding != null)
                    _sndLanding.Play();
            }
            // Apply the velocity
            if (Jumping)
            {
                Velocity.Y += Gravity;
            }
            base.Update(gameTime);
        }
    }
}