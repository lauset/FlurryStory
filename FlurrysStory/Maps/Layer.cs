using FlurrysStory.GamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FlurrysStory.Maps
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tile;
        public Image Image;
        public string SolidTiles, OverlayTiles;
        List<Tile> underlayTiles, overlayTiles;
        string state;

        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles = String.Empty;
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();
            Vector2 position = -tileDimensions;
            foreach (string row in Tile.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y - 16;
                if (position.X == -64)
                    position.X = -48;
                if (position.Y == -16)
                    position.Y = 0;
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X - 16;
                        if (!s.Contains('x'))
                        {
                            state = "Pass";
                            Tile tile = new Tile();

                            string str = s.Replace("[", String.Empty);
                            int val1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int val2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles != null)
                                if (SolidTiles.Contains("[" + val1.ToString() + ":" + val2.ToString() + "]"))
                                    state = "Solid";

                            tile.LoadContent(
                                position,
                                new Rectangle(
                                    val1 * (int)tileDimensions.X + 16,
                                    val2 * (int)tileDimensions.Y,
                                    (int)tileDimensions.X - 16,
                                    (int)tileDimensions.Y - 16),
                                state);

                            if (OverlayTiles.Contains("[" + val1.ToString() + ":" + val2.ToString() + "]"))
                                overlayTiles.Add(tile);
                            else
                                underlayTiles.Add(tile);
                        }
                    }
                }
            }
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Yuan yuan)
        {
            foreach (Tile t in underlayTiles)
                t.Update(gameTime, ref yuan);

            foreach (Tile t in overlayTiles)
                t.Update(gameTime, ref yuan);

        }

        public void Draw(SpriteBatch spriteBatch, string drawType, Vector2 offset)
        {
            List<Tile> tiles;
            if (drawType == "Underlay")
                tiles = underlayTiles;
            else
                tiles = overlayTiles;

            foreach (Tile t in tiles)
            {
                Image.Position = t.Position;
                Image.SourceRect = t.SourceRect;
                Image.Position.X += offset.X;
                Image.Position.Y += offset.Y;
                Image.Draw(spriteBatch);
            }
        }
    }
}
