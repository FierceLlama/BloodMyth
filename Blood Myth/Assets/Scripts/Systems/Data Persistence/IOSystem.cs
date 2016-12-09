using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class IOSystem 
{
    private static readonly IOSystem instance = new IOSystem();
    public IOData data;
   
    public static IOSystem Instance
    {
        get
        {
            return instance;
        }
    }

    private IOSystem ()
    {
        data = new IOData();
    }

    public void AutoSave()
    {
        if (data.isChanged)
        { 
            BinaryFormatter Bf = new BinaryFormatter();
            FileStream file;

            file = File.Open(Application.persistentDataPath + "/autosave.dat", FileMode.OpenOrCreate);

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
    
    public void ClearData()
    {
        data = new IOData();
        data.isChanged = true;
        this.AutoSave();
    }
}
