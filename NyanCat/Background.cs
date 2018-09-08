using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanCat
{
    class Background
    {
        Texture2D BgTexture;
        public Vector2 PositionBackgroundFirst;
        public Vector2 PositionBackgroundFollow;
        public int Speed { get; set; }

        public Background(Texture2D texture, int screenWidth, int speed)
        {
            BgTexture = texture;
            PositionBackgroundFirst = new Vector2(0, 0);
            PositionBackgroundFollow = new Vector2(screenWidth, 0);
            Speed = speed;
        }

        public void DrawBackground(SpriteBatch sprite)
        {
            sprite.Draw(BgTexture, PositionBackgroundFirst, Color.White);
            sprite.Draw(BgTexture, PositionBackgroundFollow, Color.White);
        }

        public void Scroll(int screenWidth)
        {
            PositionBackgroundFirst.X -= Speed;
            PositionBackgroundFollow.X -= Speed;
            if (PositionBackgroundFirst.X < (screenWidth - screenWidth * 2))
            {
                PositionBackgroundFirst.X = 0;
                PositionBackgroundFollow.X = screenWidth;
            }
        }
    }
}
