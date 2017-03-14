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
        Texture2D texture;
        Vector2 position;
        Rectangle hitBox;

        bool remove;

        public bool isRemoved
        {
            get { return remove; }
            private set { remove = value; }
        }

        public bool isSelected(Point mouse)
        {
            if (hitBox.Contains(mouse))
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
            return new Vector2(hitBox.X + (hitBox.Width / 2), hitBox.Y + (hitBox.Height / 2));
        }

        public Vector2 GetPosition()
        {
            return new Vector2(hitBox.X, hitBox.Y);
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            hitBox = new Rectangle((int)position.X, (int)position.Y, 5, 5);
            remove = false;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, Color.White);
        }

        public void RemoveMe()
        {

            remove = true;
        }
    }
}
