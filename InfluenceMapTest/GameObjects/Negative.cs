using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluenceMapTest.GameObjects
{
    class Negative : GameObject
    {

        public Negative(Texture2D texture, Vector2 position)
            :base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            myColor = Color.OrangeRed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
