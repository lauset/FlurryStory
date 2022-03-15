using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlurrysStory
{
    class Sprite
    {
        private Texture2D _texture;
        // 旋转角度
        private float _rotation;
        // 位置坐标
        public Vector2 Position;
        // 中心坐标
        public Vector2 Origin;
        // 案件
        public Input Input;
        // 速度
        public float Speed = 2f;
        // 旋转速度
        public float RotationVelocity = 3f;
        // 冲撞速度
        public float LinearVelocity = 4f;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Input == null)
            {
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                // jump
                Position.Y -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                // right
                Position.X += Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                // left
                Position.X -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                // down
                Position.Y += Speed;
            }
            // 左旋右旋
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
                _rotation += MathHelper.ToRadians(RotationVelocity);
            // 方向
            var direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - _rotation),
                -(float)Math.Sin(MathHelper.ToRadians(90) - _rotation));
            // 加速
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                Position += direction * LinearVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, Position, Color.White);
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f);
        }

    }
}
