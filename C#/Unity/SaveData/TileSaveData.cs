using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class PlacableObjectSaveData 
{
    public PlacableObjectType Type;
    public Vector2 Offset;
    public float Rotation;
}

[Serializable]
public class TileSaveData 
{
	public TileType Type;
	public List<Point2> ConnectedTiles;
    public List<PlacableObjectSaveData> Objects;
    public Point2 TilePos = -Point2.one;
    public bool b_Meltable;
    public float m_TimeToYellowFromGreenInSeconds;
    public float m_TimeToRedFromYellowInSeconds;
    public float Rotation = 0;
    public int ChosenSubTile = 0;
    public bool IsRandomConnection = true;
    public int ChosenConnectionIndex = 0;
}
