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
        int mapWidth, mapHeight;
        int cellWidth, cellHeight;
        Cell[,] map;
        Texture2D texture;

        float falloff;

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

        void CreateMap()
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

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j].Draw(spriteBatch);
                }
            }
        }

        public void CalculateInfluenceFromObject(GameObject gameobj)
        {
            Cell influenceOrigin = GetCellFromVect(gameobj.GetPosition());
            double influence;
            float objDistance;

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    objDistance = Vector2.Distance(new Vector2(map[i, j].GetPosition().X / cellWidth, map[i, j].GetPosition().Y / cellHeight)
                        , new Vector2(influenceOrigin.GetPosition().X / cellWidth, influenceOrigin.GetPosition().Y / cellHeight));
                    influence = Math.Pow(falloff, objDistance);
                    if (gameobj is Negative)
                        influence *= -1;
                    map[i, j].GiveInfluence(influence);
                }
            }
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

        public Vector2 GetCellPosition(Point pos)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = pos.X / cellWidth;
                int y = pos.Y / cellHeight;
                return map[x, y].GetOrigin();
            }
            return Vector2.Zero;
        }

        public Point GetCellPoint(Point pos)
        {
            if (pos.X > 0 && pos.X < mapWidth * cellWidth && pos.Y > 0 && pos.Y < mapHeight * cellHeight)
            {
                int x = pos.X / cellWidth;
                int y = pos.Y / cellHeight;
                return new Point((int)map[x, y].GetPosition().X, (int)map[x, y].GetPosition().Y);
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
