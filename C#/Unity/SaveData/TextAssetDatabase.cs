//Author: Dario (Please don't kill me.)

using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FileMode = System.IO.FileMode;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.VersionControl;
#endif

/// <summary>
/// Base class which handles text asset loading. Example usage:
/// <para>   </para>
/// [DataLocation("Levels")]
/// public class LevelDatabase : TextAssetDatabase&lt;LevelSaveData, LevelDatabase&gt;
/// {
/// 
/// }
/// <para>   </para>
/// This will create a database that loads all text assets in the Resources\Levels folder and tries to 
/// deserialize them to LevelSaveData objects.
/// <para>   </para>
/// The DataLocation attribute sets the folder to load the assets from.
/// <para>   </para>
/// The the second generic parameter HAS TO BE THE TYPE THAT INHERITS. It is used to get the value of the 
/// DataLocation attribute. (See the static constructor of this class).
/// </summary>
/// <typeparam name="TSerializationType">Type to use for serialization.</typeparam>
/// <typeparam name="TDerived">Type of the class which derives from this. ALWAYS!</typeparam>
public class TextAssetDatabase<TSerializationType, TDerived>
    where TSerializationType : NamedAsset
    where TDerived : TextAssetDatabase<TSerializationType, TDerived>
{
    //Path relative to the resources folder.
    private static readonly XmlSerializer DATA_SERIALIZER = new XmlSerializer(typeof(TSerializationType));
    
    private static string dataPath;

    //Holds all currently loaded assets.
    private static Dictionary<string, TSerializationType> data = new Dictionary<string, TSerializationType>();

    //Remember if an asset was modified to just save the modified ones (prevents some issues with Perforce).
    private static Dictionary<string, bool> modified = new Dictionary<string, bool>();

    static TextAssetDatabase()
    {
        //Get the DataLocationAttribute of the inheriting class to get the path to load from.
        string location =
            ((DataLocationAttribute) Attribute.GetCustomAttribute(typeof (TDerived), typeof (DataLocationAttribute)))
                .Location;
        Load(location);
    }

    /// <summary>
    /// Loads all asset files from the asset folder.
    /// </summary>
    private static void Load(string _path)
    {
        dataPath = _path;
        TextAsset[] files = Resources.LoadAll<TextAsset>(dataPath);
        if (files.Length > 0)
        {
            foreach (var file in files)
            {
                using (var sr = new StringReader(file.text))
                {
                    data.Add(file.name, (TSerializationType)DATA_SERIALIZER.Deserialize(sr));
                }
            }
        }
    }

    /// <summary>
    /// Gets all currently loaded assets.
    /// </summary>
    public static List<TSerializationType> GetAllData()
    {
        return data.Values.ToList();
    }

    /// <summary>
    /// Gets the asset with the given name.
    /// </summary>
    public static TSerializationType GetData(string _name)
    {
        if (!string.IsNullOrEmpty(_name) && data.ContainsKey(_name))
            return data[_name];
        return null;
    }

    /// <summary>
    ///  Adds or overwrites the asset with the name defined in the asset data structure and marks it as modified.
    /// </summary>
    public static void AddData(TSerializationType _data)
    {
        data[_data.Name] = _data;
        modified[_data.Name] = true;
    }



#if UNITY_EDITOR
    /// <summary>
    ///  Adds or overwrites the asset with the name defined in the asset data structure and marks it as modified.
    /// </summary>
    public static void RemoveData(TSerializationType _data)
    {
        if (data.ContainsKey(_data.Name))
        {
            string path = GetPath(_data);
            if (File.Exists(path))
            {
                Provider.Checkout(path, CheckoutMode.Both).Wait();
                File.Delete(path);
                AssetDatabase.Refresh();
            }
            data.Remove(_data.Name);
        }
    }

    /// <summary>
    /// Saves all modified assets to disk.
    /// </summary>
    public static void Save()
    {
        foreach (var level in data)
        {
            if (modified.ContainsKey(level.Key) && modified[level.Key])
            {
                modified[level.Key] = false;
                Serialize(level.Value);
            }
        }
    }


    /// <summary>
    /// Serializes a single asset using the DATA_SERIALIZER. Overwrites the file if it already exists.
    /// </summary>
    static void Serialize(TSerializationType _data)
    {
        string path = GetPath(_data);

        if (File.Exists(path))
        {
            Provider.Checkout(path, CheckoutMode.Both).Wait();
        }

        WriteFile(_data, path);
    }

    static void WriteFile(TSerializationType _data, string path)
    {
        using (var file = new FileStream(path, FileMode.Create))
        {
            DATA_SERIALIZER.Serialize(file, _data);
        }

        //Refresh the asset databse to notify Unity about the change.
        AssetDatabase.Refresh();
    }

    static string GetPath(TSerializationType _data)
    {
        return "Assets\\Resources\\" + dataPath + "\\" + _data.Name + ".txt";
    }
#endif

}

