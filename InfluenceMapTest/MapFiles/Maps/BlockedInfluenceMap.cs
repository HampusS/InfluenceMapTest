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


        public BlockedInfluenceMap(Texture2D texture)
            :base(texture)
        {
            this.texture = texture;
        }

        public void SetCellOccupancy(Point pos, bool filler)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = pos.X / cellWidth;
                int y = pos.Y / cellHeight;
                if (filler)
                    map[x, y].isOccupied = true;
                else
                    map[x, y].isOccupied = false;
            }
        }

        public void ClearOccupancy()
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j].ResetCell();
                }
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
