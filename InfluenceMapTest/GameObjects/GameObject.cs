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
        protected Vector2 position;

        public Color myColor
        {
            get;
            set;
        }

        public Rectangle HitBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 16, 16);
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

        public Vector2 GetOrigin()
        {
            return new Vector2(HitBox().Width / 2, HitBox().Height / 2);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            myColor = Color.LimeGreen;
        }

        public void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, HitBox(), myColor, 0, GetOrigin(), 1, SpriteEffects.None, 0);
        }

    }
}
