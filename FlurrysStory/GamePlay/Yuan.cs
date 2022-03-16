using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory.GamePlay
{
    public class Yuan
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        // 随机行为准备时间
        private float actionSeconds = 0.0f;
        // 随机行为执行时间
        private int actionRandom = 0;
        public Yuan()
        {
            Velocity = Vector2.Zero;
        }

        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            bool checkScroll = false;
            Image.IsActive = true;
            // 随机行为触发时间间隔，5-10秒一次
            if (actionRandom == 0)
                actionRandom = new Random().Next(5, 10);
            if (Velocity.X == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.Down))
                {
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                    //pos = new Vector2(pos.X, pos.Y - MoveSpeed);
                    checkScroll = true;
                }
                else if (InputManager.Instance.KeyDown(Keys.Up))
                {
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                    //pos = new Vector2(pos.X, pos.Y + MoveSpeed);
                    checkScroll = true;
                }
                else
                    Velocity.Y = 0;
            }
            if (Velocity.Y == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.Right))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                    //pos = new Vector2(pos.X + MoveSpeed, pos.Y);
                    checkScroll = true;
                }
                else if (InputManager.Instance.KeyDown(Keys.Left))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                    //pos = new Vector2(pos.X - MoveSpeed, pos.Y);
                    checkScroll = true;
                }
                else
                    Velocity.X = 0;
            }
            // 如果无操作，则给予随机行为，随机行为过后则禁止行为
            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                actionSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (actionSeconds > actionRandom)
                {
                    Image.SpriteSheetEffect.CurrentFrame.Y = 5;
                    // 随机行为执行5S左右后停止，然后重新开始计算
                    if (actionSeconds > (actionRandom + 5))
                    {
                        Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                        actionSeconds = 0.0f;
                        actionRandom = 0;
                        Image.IsActive = false;
                    }
                }
                else
                    Image.IsActive = false;
            }
            else
            {
                // 操作时每次都要重置随机行为
                actionSeconds = 0.0f;
                actionRandom = new Random().Next(5, 20);
            }
            if (checkScroll)
            {
                //GameGlobals.CheckScroll(Image.Position, gameTime);
            }
            Image.Update(gameTime);
            Image.Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 确保切片后再绘制，防止原始图绘制
            if (Image.SourceRect.Width <= 32)
                Image.Draw(spriteBatch);
        }

    }
}
