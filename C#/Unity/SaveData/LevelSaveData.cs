using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Holds all information about a level that is supposed to be written to disk.
/// </summary>
[Serializable]
public class LevelSaveData : NamedAsset
{
	//Save width and height for convenience (could get that information from the tile array).
	public int Width;
	public int Height;
    public float TargetTime;
    public bool HasTutorial;
	//C# can't serialize multi-dimensional arrys so we have to use an array of arrays.
	public TileSaveData[][] Tiles;
    public PathNetworkSaveData PathNetwork = new PathNetworkSaveData();
}
