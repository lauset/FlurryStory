using FlurrysStory.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FlurrysStory
{
    public class AnimScreen : GameScreen
    {
        public Image Image;
        //Texture2D image;
        //[XmlElement("Path")]
        //public List<string> path;
        //public Vector2 Position;
        public AnimScreen()
        {
            //windowedButton = new ClickableTextureComponent(new Rectangle(Game1.uiViewport.Width - 36 - 16, 16, 36, 36), Game1.mouseCursors, new Rectangle((Game1.options != null && !Game1.options.isCurrentlyWindowed()) ? 155 : 146, 384, 9, 9), 4f)
            //{
            //    myID = 81112,
            //    leftNeighborID = 81111,
            //    downNeighborID = 81113
            //};
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            //image = content.Load<Texture2D>(path[0]);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            // Enter 或 Z 进入菜单界面
            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
            {
                ScreenManager.Instance.ChangeScreens("MenuScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image, Position, Color.White);
            Image.Draw(spriteBatch);
        }

    }
}
