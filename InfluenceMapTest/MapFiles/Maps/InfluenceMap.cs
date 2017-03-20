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

        public InfluenceMap(Texture2D texture, Color myColor)
            : base(texture, myColor)
        {
            this.texture = texture;
            this.myColor = myColor;
            CreateMap(myColor);
        }

        public void Update(List<GameObject> objs)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    double tempInf = 0;
                    foreach (GameObject obj in objs)
                    {
                        Cell influenceOrigin = GetCell(new Point((int)obj.GetCenter().X, (int)obj.GetCenter().Y));
                        double influence;
                        float objDistance;

                        objDistance = Vector2.Distance(new Vector2(map[i, j].GetPosition().X / cellWidth, map[i, j].GetPosition().Y / cellHeight)
                            , new Vector2(influenceOrigin.GetPosition().X / cellWidth, influenceOrigin.GetPosition().Y / cellHeight));
                        influence = Math.Pow(falloff, objDistance);
                        tempInf += influence;
                    }
                    map[i, j].SetInfluence(tempInf);
                }
            }
        }

    }
}
