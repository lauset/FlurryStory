using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory.GamePlay
{

    public delegate void PassObject(Vector2 i, GameTime gameTime);
    public delegate object PassObjectAndReturn(object i);

    public class GameGlobals
    {
        public static PassObject PassProjectTile, PassMob, CheckScroll;
    }
}
