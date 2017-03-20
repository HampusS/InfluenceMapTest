using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluenceMapTest.MapFiles.Maps
{
    class FinalInfluenceMap : Map
    {

        public FinalInfluenceMap(Texture2D texture, Color color) 
            : base(texture, color)
        {
            this.texture = texture;
            this.myColor = color;
        }

        public void FinalizeInfluence(InfluenceMap lhs, InfluenceMap rhs)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j].Influence = lhs.map[i, j].Influence - rhs.map[i, j].Influence;
                    if (map[i, j].Influence < 0)
                        map[i, j].Influence *= -1;

                    if (lhs.map[i, j].Influence > rhs.map[i, j].Influence)
                        map[i, j].MyColor = lhs.map[i, j].MyColor;
                    else if (lhs.map[i, j].Influence < rhs.map[i, j].Influence)
                        map[i, j].MyColor = rhs.map[i, j].MyColor;
                    else
                        map[i, j].MyColor = Color.Black;
                }
            }
        }
    }
}
