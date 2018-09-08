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
    class Thing
    {
        public Texture2D EmptyTexture;
        public Texture2D ThingTexture;
        public Vector2 ThingPosition;
        public int Length;
        public int Heigth;
        public int Scores;

        public Thing(Texture2D texture, Texture2D emptyTexture, int x, int y, int scores)
        {
            ThingTexture = texture;
            EmptyTexture = emptyTexture;
            ThingPosition = new Vector2(x, y);
            Length = ThingTexture.Width;
            Heigth = ThingTexture.Height;
            Scores = scores;
        }

        public void DrawThing(SpriteBatch sprite)
        {
            sprite.Draw(ThingTexture, ThingPosition, Color.White);
        }

        public void Scroll(int speed)
        {
            if (ThingPosition.X + Length <= 0)
                ThingPosition.X = 320 * 50 - (ThingPosition.X + Length);
            else
                ThingPosition.X -= speed;
        }
    }
}