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
    class Map
    {
        protected int mapWidth, mapHeight;
        protected int cellWidth, cellHeight;
        public Cell[,] map;
        protected Texture2D texture;
        protected Color myColor;
        protected float falloff;

        public Map(Texture2D texture, Color myColor)
        {
            this.texture = texture;
            this.myColor = myColor;
            Initialize();
            CreateMap(myColor);
        }

        protected virtual void Initialize()
        {
            falloff = InfluenceMapConfig.FallOff;
            cellWidth = InfluenceMapConfig.CellWidth;
            cellHeight = InfluenceMapConfig.CellHeight;
            mapWidth = InfluenceMapConfig.MapWidth;
            mapHeight = InfluenceMapConfig.MapHeight;
        }

        protected virtual void CreateMap(Color myColor)
        {
            map = new Cell[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j] = new Cell(texture, myColor, i * (cellWidth), j * (cellHeight), cellWidth, cellHeight);
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

        public Cell GetCell(Point pos)
        {
            if (pos.X >= 0 && pos.X < mapWidth * cellWidth && pos.Y >= 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = pos.X / cellWidth;
                int y = pos.Y / cellHeight;
                return map[x, y];
            }
            return null;
        }
    }
}
