using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FrameAnimation
{
    private List<Texture2D> frameTextures;
    private int currentFrame;
    private double frameTime;
    private double Timer;

    public FrameAnimation(double pFrameTime)
    {
        Timer = 0;
        currentFrame = 0;
        frameTime = pFrameTime;
        frameTextures = new List<Texture2D>();
    }

    public void AddTexture(Texture2D pTexture)
    {
        frameTextures.Add(pTexture);
    }

    public void Update(GameTime pGameTime)
    {
        Timer += pGameTime.ElapsedGameTime.TotalMilliseconds;
        if (Timer >= frameTime)
        {
            currentFrame++;
            if (currentFrame > frameTextures.Count() - 1)
            {
                currentFrame = 0;
            }
            Timer = 0;
        }
    }

    public Texture2D getTexture()
    {
        return frameTextures[currentFrame];
    }
}
