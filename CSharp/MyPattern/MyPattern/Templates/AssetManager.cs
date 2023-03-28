using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates
{
    internal class AssetManager
    {
        public static SpriteFont MainFont { get; private set; }
        public static Song MusicGamePlay { get; private set; }
        public static void Load(ContentManager pContentManager)
        {
            MainFont = pContentManager.Load<SpriteFont>("FontM6");
            MusicGamePlay = pContentManager.Load<Song>("techno");

        }
    }
}
