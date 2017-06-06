using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Xml;
using System.Xml.Serialization;
using System.IO;


[RequireComponent(typeof(Spawner))]

public class SpawnerXML : MonoBehaviour
{


    // Stores individual data associated 
    // with each spawned object 
    public class SpawnerData
    {
        public Vector3 position;
        public Quaternion rotation;

    }

    [XmlRoot]
    public class XMLContainer
    {
        [XmlArray]
        public SpawnerData[] spawners;

    }

    public string filename = "DefaultFileName";


    private Spawner spawner;
    private string fullPath;

    // Data container for XML 
    private XMLContainer data;


    // Saves Xml container instance to file format
    void SavetoPath(string path)
    {
        // Create a serializer of type XMLContainer
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        // Open a file stream at path using Create file mode 
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            // Serialize stream to data 
            serializer.Serialize(stream, data);

        }
    }
    // load XMLcontainer from Path (note only run if the file definately exists)
    XMLContainer Load(string path)

    {
        // Create a serializer of type XMLContainer
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        // Open a file stream at path using Create file mode 
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            // Return the de-serialized stream as XMLContainer
            return serializer.Deserialize(stream) as XMLContainer;

        }
    }




    // saves whatever data value is to XML file
    public void Save()
    {
        // SET data to new Data 
        // data = new string Data; 

        // SET objects to objects in spawner
        // GameObject objects = Spawner.objects;

        // SET data.spawners to new SpawnerData[objects.Count]
        // data.spawners = new SpawnerData[objects.Count]

        // FOR i= 0 to objects.count 
        //     for (int i = 0; i < objects.spawnerData.Count;  i++)
        // {

        //}

        // SET (new variable) spawner to new SpawnerData
        // SET item to objects[i]
        // SET spawner's position to items position 
        // SET spawner's rotatin to items rotation 
        // SET data.spawners[i] = spawner;
        // CALL SaveToPath(fullPath)
    }

    // Applies saved data to the scene (using Spawner)
    void Apply()
    {
        // SET spawners to data.spawners
        // FOR i = 0 to spawners.Length
        // SET data to spawners[9] 
        // CALL spawner.Spawn() and pass data.position, data.rotation

    }

    void Awake()
    {
        // SET spawner to Spawner Component

    }


    void Start()
    {
        // SET fullPath to Application.dataPath + "/" + fileName + ".xml"
        // IF file exists at fullPath
        // SET data to Load(FullPath)
        // CALL Apply()

    }

}
