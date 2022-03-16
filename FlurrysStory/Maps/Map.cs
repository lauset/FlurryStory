using FlurrysStory.GamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FlurrysStory.Maps
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layers;
        public Vector2 TileDimensions;

        public Map()
        {
            Layers = new List<Layer>();
            TileDimensions = Vector2.Zero;
        }

        public void LoadContent()
        {
            foreach (Layer l in Layers)
            {
                l.LoadContent(TileDimensions);
            }
        }

        public void UnloadContent()
        {
            foreach (Layer l in Layers)
                l.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Yuan yuan)
        {
            foreach (Layer l in Layers)
                l.Update(gameTime, ref yuan);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType, Vector2 offset)
        {
            foreach (Layer l in Layers)
                l.Draw(spriteBatch, drawType, offset);
        }
    }
}
