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
    class InfluenceMap : Map
    {

        public InfluenceMap(Texture2D texture)
            :base(texture)
        {
            this.texture = texture;
            CreateMap();
        }

        public void CalculateInfluenceFromObject(GameObject gameobj)
        {
            Cell influenceOrigin = GetCellFromPoint(gameobj.GetPosition());
            double influence;
            float objDistance;

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    objDistance = Vector2.Distance(new Vector2(map[i, j].GetPosition().X / cellWidth, map[i, j].GetPosition().Y / cellHeight)
                        , new Vector2(influenceOrigin.GetPosition().X / cellWidth, influenceOrigin.GetPosition().Y / cellHeight));
                    influence = Math.Pow(falloff, objDistance);
                    map[i, j].myColor = gameobj.myColor;
                    map[i, j].GiveInfluence(influence);
                }
            }
        }
    }
}
