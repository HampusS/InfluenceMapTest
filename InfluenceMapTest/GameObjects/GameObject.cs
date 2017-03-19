using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluenceMapTest.GameObjects
{
    class GameObject
    {
        protected Texture2D texture;
        protected Point position;

        public Color myColor
        {
            get;
            set;
        }

        public Rectangle HitBox()
        {
            return new Rectangle(position.X, position.Y, 16, 16);
        }

        public bool isSelected(Point mouse)
        {
            if (HitBox().Contains(mouse))
                return true;
            return false;
        }

        public bool myCellIsSelected(Cell cell)
        {
            if (cell.CheckOccupancy(this))
                return true;
            return false;
        }

        public Point GetPosition()
        {
            return position;
        }

        public GameObject(Texture2D texture, Point position)
        {
            this.texture = texture;
            this.position = position;
            myColor = Color.LimeGreen;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox(), null, myColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

    }
}
