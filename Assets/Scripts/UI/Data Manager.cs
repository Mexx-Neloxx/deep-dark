using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void Save(GameData data)
    {
        var path = Path.Combine(Application.persistentDataPath, "playerData.xml");
        Debug.Log(path);
        var serializer = new XmlSerializer(typeof(GameData));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }

    public GameData Load()
    {

        var path = Path.Combine(Application.persistentDataPath, "playerData.xml");
        Debug.Log(path);
        var serializer = new XmlSerializer(typeof(GameData));
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as GameData;
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    [Serializable]
    public class GameData
    {
        public static bool NeedLoadGame;
        public Vector3 PlayerPosition;
        public Quaternion LookPosition;

    }
}
