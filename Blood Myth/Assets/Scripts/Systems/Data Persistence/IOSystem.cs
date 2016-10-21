using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class IOData
{
    [System.NonSerialized]
    private bool _ischanged = false;
    public bool isChanged { get { return this._ischanged; } }

    private string _playername = "Tommy";
    public string PlayerName { get { return this._playername; } set { this._playername = value; _ischanged = true; } }
    
}

public class IOSystem 
{
    private static readonly IOSystem instance = new IOSystem();
    private IOData data;
   

    public static IOSystem Instance { get { return instance; } }

    private IOSystem () { data = new IOData(); }

    public void AutoSave()
    {
        if (data.isChanged)
        { 
            BinaryFormatter Bf = new BinaryFormatter();
            FileStream file;

            if (File.Exists(Application.persistentDataPath + "/autosave.dat"))
                file = File.Open(Application.persistentDataPath + "/autosave.dat", FileMode.Open);
            else
                file = File.Create(Application.persistentDataPath + "/autosave.dat");
     
            Bf.Serialize(file, data);
            file.Close();
        }
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/autosave.dat") )
        {
            FileStream file = File.Open(Application.persistentDataPath + "/autosave.dat", FileMode.Open);
            this.data = (IOData)bf.Deserialize(file);
            file.Close();
        }   
    }
}