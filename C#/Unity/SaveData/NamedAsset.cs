using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

[Serializable]
[XmlInclude(typeof(LevelSaveData)), XmlInclude(typeof(WorldSaveData))]
public class NamedAsset
{
    public string Name;
}

