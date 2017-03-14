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
        float influence;
        float max, min;
        int size;

        public bool isOccupied
        {
            get;
            set;
        }

        public Color myColor
        {
            get;
            set;
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(hitBox.X + (size / 2), hitBox.Y + (size / 2));
        }

        public Vector2 GetPosition()
        {
            return new Vector2(hitBox.X, hitBox.Y);
        }

        public Cell(Texture2D texture, int x, int y, int size)
        {
            this.texture = texture;
            this.hitBox = new Rectangle(x, y, size, size);
            this.size = size;
            myColor = Color.White;
            influence = 0.2f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, myColor * influence);
        }

        public bool CheckPointIntertsect(Point pos)
        {
            if (hitBox.Contains(pos))
                return true;
            return false;
        }

        public bool CheckOccupancy(GameObject obj)
        {
            if (hitBox.Contains(obj.GetOrigin()))
                return true;
            return false;
        }

        public void UpdateColor(float modifier)
        {
            influence = modifier;
        }

    }
}
