using System;
using System.Collections.Generic;

/// <summary>
/// Contains all data associated with a world.
/// </summary>
[Serializable]
public class WorldSaveData : NamedAsset
{
    public List<string> LevelNames;

    public string NextLevel(string currentLevel)
    {
        int index = LevelNames.IndexOf(currentLevel);
        if (index == -1 || index == LevelNames.Count - 1)
        {
            return null;
        }
        return LevelNames[index + 1];
    }
}
