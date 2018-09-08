using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NyanCat
{
    class Platform
    {
        Texture2D PlatformTexture;
        public Vector2 PlatformPosition;
        public int Length;

        public Platform(Texture2D texture,int x, int y,int screenWidth)
        {
            PlatformTexture = texture;
            PlatformPosition = new Vector2(x, y);
            Length = PlatformTexture.Width;
        }

        public void DrawPlatform(SpriteBatch sprite)
        {
            sprite.Draw(PlatformTexture, PlatformPosition, Color.White);
        }

        public void Scroll(int speed)
        {
            if (PlatformPosition.X + Length <= 0)
                PlatformPosition.X = 320 * 50 - (PlatformPosition.X + Length);
            else
                PlatformPosition.X -= speed;
        }
    }
}
