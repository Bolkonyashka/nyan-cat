using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanCat
{
    class Rainbow
    {
        Texture2D RainbowTexture;
        Texture2D RainbowTexture2;
        public Vector2[] PositionsRainbow;

        public Rainbow(Texture2D texture, Texture2D texture2)
        {
            RainbowTexture = texture;
            RainbowTexture2 = texture2;
            PositionsRainbow = new Vector2[14];
            for (int i = 0; i < PositionsRainbow.Length; i++)
                PositionsRainbow[i] = new Vector2(i * RainbowTexture.Width, 0);
        }

        public void DrawRainbow(SpriteBatch sprite)
        {
            for (int i = 0; i < PositionsRainbow.Length; i++)
                sprite.Draw(i % 2 == 0 ? RainbowTexture : RainbowTexture2, PositionsRainbow[i], Color.White);
        }

        public void Scroll(List<float> nyanLocation)
        {
            for (int i = 0; i < PositionsRainbow.Length; i++)
            {
                PositionsRainbow[i].Y = nyanLocation[i];
                PositionsRainbow[i].X -= 2;
            }
            if (PositionsRainbow[0].X < RainbowTexture.Width - RainbowTexture.Width * 2)
            {
                for (int i = 0; i < PositionsRainbow.Length; i++)
                    PositionsRainbow[i].X = RainbowTexture.Width * i;
            }
            nyanLocation.RemoveAt(0);
        }
    }
}
