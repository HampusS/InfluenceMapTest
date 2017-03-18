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
            get; private set;
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(hitBox.X + (width / 2), hitBox.Y + (height / 2));
        }

        public Vector2 GetPosition()
        {
            return new Vector2(hitBox.X, hitBox.Y);
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
            float infHold = (float)influence;
            if (influence < 0)
            {
                myColor = Color.OrangeRed;
                infHold *= -1;
            }
            else if (influence > 0)
                myColor = Color.DeepSkyBlue;
            else
                myColor = Color.Black;

            spriteBatch.Draw(texture, hitBox, myColor * (float)Math.Abs(influence));
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
