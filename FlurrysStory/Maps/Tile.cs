using FlurrysStory.GamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory.Maps
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;
        string state;
        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Tile()
        {

        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime, ref Yuan yuan)
        {
            if (state == "Solid")
            {
                Rectangle tileRect = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    sourceRect.Width,
                    sourceRect.Height);
                Rectangle yuanRect = new Rectangle(
                    (int)yuan.Image.Position.X,
                    (int)yuan.Image.Position.Y,
                    yuan.Image.SourceRect.Width,
                    yuan.Image.SourceRect.Height);
                if (yuanRect.Intersects(tileRect))
                {
                    if (yuan.Velocity.X < 0)
                        yuan.Image.Position.X = tileRect.Right;
                    else if (yuan.Velocity.X > 0)
                        yuan.Image.Position.X = tileRect.Left - yuan.Image.SourceRect.Width;
                    else if (yuan.Velocity.Y < 0)
                        yuan.Image.Position.Y = tileRect.Bottom;
                    else
                        yuan.Image.Position.Y = tileRect.Top - yuan.Image.SourceRect.Height;

                    yuan.Velocity = Vector2.Zero;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
