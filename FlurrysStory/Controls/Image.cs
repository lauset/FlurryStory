using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FlurrysStory
{
    public class Image
    {
        // 透明度
        public float Alpha, Rotation;
        // 文本、字体、内容路径
        public string Text, FontName, Path;
        // 位置、缩放
        public Vector2 Position, Scale;
        // 区域
        public Rectangle SourceRect;
        // 是否启用、全屏
        public bool IsActive, IsFullScreen;
        [XmlIgnore]
        public Texture2D Texture;
        // 中心坐标
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        SpriteFont font;
        // 效果列表
        Dictionary<string, ImageEffect> effectList;
        // 拥有效果
        public string Effects;
        // 淡出淡入效果
        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            effectList.Add(effect.GetType().ToString().Replace("FlurrysStory.", ""),
                (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }
            if (Effects != String.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
            string[] split = Effects.Split(":");
            foreach (string s in split)
                ActivateEffect(s);
        }

        public Image()
        {
            Path = Text = Effects = String.Empty;
            FontName = "Fonts/tinyFont";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            Rotation = 0.0f;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);
            font = content.Load<SpriteFont>(FontName);
            Vector2 dimensions = Vector2.Zero;
            if (Texture != null)
                dimensions.X += Texture.Width;
            //dimensions.X += font.MeasureString(Text).X;
            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y = font.MeasureString(Text).Y;
            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            renderTarget = new RenderTarget2D(
                ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X,
                (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            // 绘制贴图
            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            // 绘制文本水平和垂直居中
            if (Text != String.Empty)
                ScreenManager.Instance.SpriteBatch.DrawString(font, Text,
                    new Vector2(
                        ((int)dimensions.X - font.MeasureString(Text).X) / 2,
                        ((int)dimensions.Y - font.MeasureString(Text).Y) / 2),
                    Color.Black);
            ScreenManager.Instance.SpriteBatch.End();
            Texture = renderTarget;
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);
            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(":");
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var e in effectList)
                DeactivateEffect(e.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var e in effectList)
            {
                if (e.Value.IsActive)
                    e.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 中心点，绘制时先使用(0,0)，防止缩放错误
            origin = new Vector2(
                SourceRect.Width / 2,
                SourceRect.Height / 2);
            spriteBatch.Draw(
                Texture,
                Position,
                SourceRect,
                Color.White * Alpha,
                Rotation,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0.0f);
        }

    }
}
