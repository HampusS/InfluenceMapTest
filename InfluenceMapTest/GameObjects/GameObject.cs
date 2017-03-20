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
        protected Vector2 direction;
        protected Color myColor;
        protected float speed;
        protected int size;
        Vector2 origin;


        public Rectangle HitBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, size, size);
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

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetCenter()
        {
            return new Vector2(position.X + (size / 2), position.Y + (size / 2));
        }

        public GameObject(Texture2D texture, Point position, Color color, int size)
        {
            this.texture = texture;
            this.position = new Vector2(position.X, position.Y);
            this.size = size;
            this.speed = 3;
            this.direction = new Vector2(1, 0);
            origin = new Vector2(0, 0);
            myColor = color;
        }

        public virtual void Update(float time)
        {
            position += direction * speed * time;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox(), null, myColor, 0, origin, SpriteEffects.None, 0);
        }

    }
}
