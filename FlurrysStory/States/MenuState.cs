using FlurrysStory.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private Texture2D _menuBgTexture;
        private Rectangle _menuBgRectangle;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            game.Window.Title = "风与雪的故事";
            // 背景贴图
            _menuBgTexture = _content.Load<Texture2D>("Controls/stardewPanorama");
            _menuBgRectangle = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            // 按钮贴图
            var buttonTexture = _content.Load<Texture2D>("Controls/textBox");
            var buttonFont = _content.Load<SpriteFont>("Fonts/tinyFont");
            // 新游戏按钮
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 260),
                Text = " New "
            };
            newGameButton.Click += NewGameButton_Click;
            // 加载游戏按钮
            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 320),
                Text = " Load "
            };
            loadGameButton.Click += LoadGameButton_Click;
            // 退出游戏按钮
            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 380),
                Text = " Quit "
            };
            quitGameButton.Click += QuitGameButton_Click;
            // 按钮列表赋值
            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                quitGameButton
            };
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // 绘制背景图
            spriteBatch.Draw(_menuBgTexture, _menuBgRectangle, Color.White);
            // 绘制所有按钮
            foreach (var c in _components)
                c.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // TODO: Remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            // 按钮更新
            foreach (var c in _components)
                c.Update(gameTime);
        }
    }
}
