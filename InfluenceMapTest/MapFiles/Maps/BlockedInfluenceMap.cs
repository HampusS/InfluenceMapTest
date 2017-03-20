using InfluenceMapTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluenceMapTest.MapFiles.Maps
{
    class BlockedInfluenceMap : Map
    {
        List<Cell> blockedCells = new List<Cell>();

        public BlockedInfluenceMap(Texture2D texture, Color myColor)
            : base(texture, myColor)
        {
            this.texture = texture;
            this.myColor = myColor;
            CreateMap(myColor);
        }

        public void Update(List<GameObject> objs)
        {
            blockedCells.Clear();
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    foreach (GameObject obj in objs)
                    {
                        if (map[i, j].CheckOccupancy(obj))
                        {
                            map[i, j].isOccupied = true;
                            blockedCells.Add(new Cell(texture, Color.Blue, map[i, j].GetPosition().X, map[i,j].GetPosition().Y, 16, 16));
                            blockedCells[blockedCells.Count - 1].SetInfluence(1);
                            break;
                        }
                        else
                            map[i, j].isOccupied = false;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in blockedCells)
            {
                cell.Draw(spriteBatch);
            }
        }

        public bool CheckVacancy(Point pos)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = pos.X / cellWidth;
                int y = pos.Y / cellHeight;
                if (map[x, y].isOccupied)
                    return false;
            }
            return true;
        }
    }
}
