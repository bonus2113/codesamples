using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class PathNodeSaveData
{
    public Point2 TilePos;
    public List<Point2> Connections;
    public bool IsRandomConnection = true;
    public int ChosenConnectionIndex = 0;
}
