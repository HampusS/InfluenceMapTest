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
        int width, height;
        Cell[,] map;
        int cellSize;
        Texture2D texture;

        public Map(Texture2D texture, int width, int height, int cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.texture = texture;
            CreateMap();
        }

        void CreateMap()
        {
            map = new Cell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = new Cell(texture, i * (cellSize), j * (cellSize), cellSize);
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j].CheckPointIntertsect(Game1.mouse.Position))
                        map[i, j].UpdateColor(4);
                    else
                        map[i, j].UpdateColor(0.4f);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j].Draw(spriteBatch);
                }
            }
        }

        public void SetCellOccupancy(Point pos, bool filler)
        {
            if (pos.X > 0 && pos.X < width * cellSize && pos.Y > 0 && pos.Y < height * cellSize)
            {
                int x = (int)pos.X / cellSize;
                int y = (int)pos.Y / cellSize;
                if (filler)
                {
                    map[x, y].myColor = Color.LimeGreen;
                    map[x, y].isOccupied = true;
                }
                else
                {
                    map[x, y].myColor = Color.Red;
                    map[x, y].isOccupied = false;
                }
            }
        }

        public void ClearOccupancy()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j].myColor = Color.White;
                    map[i, j].isOccupied = false;
                }
            }
        }

        public bool CheckVacancy(Point pos)
        {
            if (pos.X > 0 && pos.X < width * cellSize && pos.Y > 0 && pos.Y < height * cellSize)
            {
                int x = (int)pos.X / cellSize;
                int y = (int)pos.Y / cellSize;
                if (map[x, y].isOccupied)
                    return false;
            }
            return true;
        }

        public Vector2 GetCellPosition(Point pos)
        {
            if (pos.X > 0 && pos.X < width * cellSize && pos.Y > 0 && pos.Y < height * cellSize)
            {
                int x = (int)pos.X / cellSize;
                int y = (int)pos.Y / cellSize;
                return map[x, y].GetPosition();
            }
            return Vector2.Zero;
        }

        public Cell GetCell(Point pos)
        {
            if (pos.X > 0 && pos.X < width * cellSize && pos.Y > 0 && pos.Y < height * cellSize)
            {
                int x = (int)pos.X / cellSize;
                int y = (int)pos.Y / cellSize;
                return map[x, y];
            }
            return null;
        }
    }
}
