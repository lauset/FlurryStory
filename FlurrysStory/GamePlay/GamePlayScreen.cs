using FlurrysStory.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory.GamePlay
{
    public class GamePlayScreen : GameScreen
    {
        //public List<ProjectTile2d> projectTiles = new List<ProjectTile2d>();
        //public List<Mob> mobs = new List<Mob>();
        Vector2 offset;
        Yuan yuan;
        Map maps;

        public GamePlayScreen()
        {
            //GameGlobals.PassProjectTile = AddProjectTile;
            //GameGlobals.PassMob = AddMob;
            GameGlobals.CheckScroll = CheckScroll;
            offset = Vector2.Zero;
        }

        //public virtual void AddMob(object Info)
        //{
        //    mobs.Add((Mob)Info);
        //}

        //public virtual void AddProjectTile(object Info)
        //{
        //    projectTiles.Add((ProjectTile2d)Info);
        //}

        public virtual void CheckScroll(Vector2 Info, GameTime gameTime)
        {
            Vector2 tempPos = Info;

            if (tempPos.X < -offset.X + (1280 * .4f))
            {
                offset = new Vector2(
                    offset.X + yuan.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,
                    offset.Y);
            }

            if (tempPos.X > -offset.X + (1280 * .6f))
            {
                offset = new Vector2(
                    offset.X - yuan.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,
                    offset.Y);
            }

            if (tempPos.Y > -offset.Y + (1280 * .4f))
            {
                offset = new Vector2(
                    offset.X,
                    offset.Y + yuan.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (tempPos.Y > -offset.Y + (1280 * .6f))
            {
                offset = new Vector2(
                    offset.X,
                    offset.Y - yuan.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

        }

        public override void LoadContent()
        {
            base.LoadContent();
            XmlManager<Yuan> yuanLoader = new XmlManager<Yuan>();
            XmlManager<Map> mapLoader = new XmlManager<Map>();
            yuan = yuanLoader.Load("Content/Load/Players/Yuan.xml");
            maps = mapLoader.Load("Content/Load/Maps/Map01.xml");
            yuan.LoadContent();
            maps.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            yuan.UnloadContent();
            maps.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            yuan.Update(gameTime);
            maps.Update(gameTime, ref yuan);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            maps.Draw(spriteBatch, "Underlay", offset);
            yuan.Draw(spriteBatch);
            maps.Draw(spriteBatch, "Overlay", offset);
        }

    }
}
