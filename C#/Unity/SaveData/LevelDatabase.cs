using UnityEngine;

/// <summary>
/// The level database scans the folder at Assets/Resources/Levels for level files and loads them into memory.
/// </summary>
[DataLocation("Levels")]
public class LevelDatabase : TextAssetDatabase<LevelSaveData, LevelDatabase>
{

}
