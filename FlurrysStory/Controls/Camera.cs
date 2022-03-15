using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlurrysStory
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 target)
        {
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0);
            var offset = Matrix.CreateTranslation(400, 250, 0);

            Transform = position * offset;
        }
    }
}
