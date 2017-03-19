using InfluenceMapTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluenceMapTest.MapFiles
{
    internal struct InfluenceMapConfig
    {
        internal static int CellWidth = 16;
        internal static int CellHeight = 16;
        internal static int MapWidth = 45 * 14;
        internal static int MapHeight = 27 * 14;
        internal static float FallOff = 0.9f;
    }

    class Map
    {
        protected int mapWidth, mapHeight;
        protected int cellWidth, cellHeight;
        protected Cell[,] map;
        protected Texture2D texture;

        protected float falloff;

        public Map(Texture2D texture)
        {
            this.texture = texture;
            falloff = InfluenceMapConfig.FallOff;
            cellWidth = InfluenceMapConfig.CellWidth;
            cellHeight = InfluenceMapConfig.CellHeight;
            mapWidth = InfluenceMapConfig.MapWidth;
            mapHeight = InfluenceMapConfig.MapHeight;
            CreateMap();
        }

        protected virtual void CreateMap()
        {
            map = new Cell[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j] = new Cell(texture, i * (cellWidth), j * (cellHeight), cellWidth, cellHeight);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j].Draw(spriteBatch);
                }
            }
        }

        public Point GetCellPosition(Point pos)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = pos.X / cellWidth;
                int y = pos.Y / cellHeight;
                return map[x, y].GetPosition();
            }
            return Point.Zero;
        }

        public Cell GetCellFromPoint(Point pos)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = (int)pos.X / cellWidth;
                int y = (int)pos.Y / cellHeight;
                return map[x, y];
            }
            return null;
        }

        public Cell GetCellFromVect(Vector2 pos)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = (int)pos.X / cellWidth;
                int y = (int)pos.Y / cellHeight;
                return map[x, y];
            }
            return null;
        }
    }
}
