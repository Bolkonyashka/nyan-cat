using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanCat
{
    class NyanCat
    {
        Texture2D NyanTexture;
        public Vector2 NyanPosition;
        public bool OnPlatform;
        public bool InJump;
        public bool SecondJump;
        public float Speed;
        public int Length;
        public int Height;

        public NyanCat(Texture2D texture, int borderDist, int startPosition)
        {
            NyanTexture = texture;
            NyanPosition = new Vector2(borderDist, startPosition);
            OnPlatform = false;
            InJump = false;
            SecondJump = false;
            Speed = 5;
            Length = NyanTexture.Width;
            Height = NyanTexture.Height;
        }

        public int NyanCheck (List<Platform> platforms, List<Thing> things, int scores)
        {
            foreach (var thing in things.Where(x => x.ThingPosition.X > 125 && x.ThingPosition.X < 175))
            {
                if (NyanPosition.Y < thing.ThingPosition.Y + thing.Heigth - 25 &&
                    NyanPosition.Y + Height > thing.ThingPosition.Y + 25)
                {
                    thing.ThingTexture = thing.EmptyTexture;
                    if (scores + thing.Scores < 0)
                        scores = 0;
                    else
                        scores += thing.Scores;
                    thing.Scores = 0;
                }
            }

            foreach (var platform in platforms)
            {
                if (NyanPosition.Y + 35 >= platform.PlatformPosition.Y && 
                    NyanPosition.Y + 35 <= platform.PlatformPosition.Y + 5 &&
                    NyanPosition.X + 56 >= platform.PlatformPosition.X &&
                    NyanPosition.X <= platform.PlatformPosition.X + platform.Length)
                {
                    OnPlatform = true;
                    break;
                }
                else
                    OnPlatform = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !InJump && OnPlatform)
            {
                InJump = true;
                Speed = -6;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && !SecondJump && !OnPlatform && InJump)
            {
                SecondJump = true;
                Speed = -7;
            }
            if (InJump)
            {
                if (OnPlatform && Speed >= 0)
                {
                    InJump = false;
                    SecondJump = false;
                }
                else if (Speed < 4.5f)
                    Speed += 0.2f;
            }
            else if (!OnPlatform && Speed < 4.5f)
                Speed += 0.2f;
            else if (OnPlatform)
                Speed = 0;
            ControlNyan(Keyboard.GetState(), (OnPlatform && !InJump) ? 0 : Speed);
            return scores;
        }

        public void DrawNyan(SpriteBatch sprite)
        {
            sprite.Draw(NyanTexture, NyanPosition, Color.White);
        }

        public void ControlNyan(KeyboardState state, float speed)
        {
            NyanPosition.Y += speed;
            /*if ((state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W)) && this.NyanRect.Y > 0)
                NyanRect.Y -= speed;

            if ((state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S)) && this.NyanRect.Bottom < screenHeight - 5)
                NyanRect.Y += speed;*/
        }
    }
}
