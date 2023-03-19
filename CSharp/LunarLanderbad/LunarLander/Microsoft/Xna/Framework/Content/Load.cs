using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
    internal class Load<T> : Texture2D
    {
        private string v;

        public Load(string v)
        {
            this.v = v;
        }
    }
}