using FlurrysStory.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlurrysStory.States
{
    public class GameState : State
    {
        private List<Sprite> _sprites;
        private List<Component> _gameComponents;
        private Color _bgColor = Color.WhiteSmoke;
        private Camera _camera;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            game.Window.Title = "蔚蓝";
            //_camera = new Camera();
            // 蔚蓝、按钮、字体贴图
            var weiTexture = _content.Load<Texture2D>("Sprites/wei_stand01");
            var btnTexture = _content.Load<Texture2D>("Controls/textBox");
            var fontTexture = _content.Load<SpriteFont>("Fonts/tinyFont");
            // 随机颜色按钮与返回按钮
            var randomButton = new Button(btnTexture, fontTexture)
            {
                Position = new Vector2(300, 300),
                Text = "Random",
            };
            var backButton = new Button(btnTexture, fontTexture)
            {
                Position = new Vector2(300, 360),
                Text = "Back",
            };
            randomButton.Click += RandomButton_Click;
            backButton.Click += BackButton_Click;
            _gameComponents = new List<Component>()
            {
                randomButton,
                backButton
            };
            // 一些蔚蓝
            _sprites = new List<Sprite>()
            {
                new Sprite(weiTexture)
                {
                    Origin = new Vector2(weiTexture.Width / 2, weiTexture.Height / 2),
                    Position = new Vector2(weiTexture.Width / 2, weiTexture.Height/ 2),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                        Left = Keys.A,
                        Right = Keys.D
                    }
                },
                //new Sprite(weiTexture)
                //{
                //    Origin = new Vector2(weiTexture.Width / 2, weiTexture.Height / 2),
                //    Position = new Vector2(100, 100),
                //    Input = new Input()
                //    {
                //        Up = Keys.S,
                //        Down = Keys.W,
                //        Left = Keys.D,
                //        Right = Keys.A
                //    }
                //}
            };
        }

        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            // 获取随机色
            var random = new Random();
            _bgColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            //Exit();
        }

        private void BackButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // 设置背景色
            _graphicsDevice.Clear(_bgColor);
            spriteBatch.Begin();
            // 绘制所有按钮
            foreach (var c in _gameComponents)
                c.Draw(gameTime, spriteBatch);
            // 绘制所有蔚蓝
            foreach (Sprite s in _sprites)
                s.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            // 按钮更新
            foreach (var c in _gameComponents)
                c.Update(gameTime);
            // 蔚蓝更新
            foreach (Sprite s in _sprites)
            {
                //_camera.Follow(s.Position);
                s.Update();
            }
        }
    }
}
