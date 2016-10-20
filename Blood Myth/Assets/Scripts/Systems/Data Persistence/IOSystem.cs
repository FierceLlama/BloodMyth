using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class IOData
{
   string playername = "Tommy";
   //Add Whatever data We need to Save.
}

//TODO (OMAR) AutoSave will happen everytime the Loading Scene is called
//Before doing that Check that something has changed to avoid unnecessary writing to the disk.
public class IOSystem
{
    private static readonly IOSystem instance = new IOSystem();
    public IOData data;

    public static IOSystem Instance { get { return instance; } }

    private IOSystem () { data = new IOData(); }

    public void AutoSave()
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