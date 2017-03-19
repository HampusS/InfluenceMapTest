using InfluenceMapTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluenceMapTest
{
    class Cell
    {
        Rectangle hitBox;
        Texture2D texture;
        int width, height;
        double influence;

        public bool isOccupied
        {
            get;
            set;
        }

        public Color myColor
        {
            get; set;
        }

        public Point GetOrigin()
        {
            return new Point(hitBox.X + (width / 2), hitBox.Y + (height / 2));
        }

        public Point GetPosition()
        {
            return new Point(hitBox.X, hitBox.Y);
        }

        public Cell(Texture2D texture, int x, int y, int width, int height)
        {
            this.texture = texture;
            this.hitBox = new Rectangle(x, y, width, height);
            this.width = width;
            this.height = height;
            influence = 0.0f;
            myColor = Color.Black;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, myColor * (float)influence);
        }

        public void ResetCell()
        {
            myColor = Color.Black;
            isOccupied = false;
            influence = 0;
        }

        public bool CheckPointIntertsect(Point pos)
        {
            if (hitBox.Contains(pos))
                return true;
            return false;
        }

        public bool CheckOccupancy(GameObject obj)
        {
            if (hitBox.Contains(obj.GetPosition()))
                return true;
            return false;
        }

        public void GiveInfluence(double inf)
        {
            influence += inf;
        }

    }
}
