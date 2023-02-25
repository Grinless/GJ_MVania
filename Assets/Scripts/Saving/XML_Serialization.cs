using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public static class XMLFileNames
{
    /// <summary>
    /// The string indicating the filename for room alignment files. 
    /// </summary>
    public static string roomTreeFilename = "/savedata.BCAT";

    /// <summary>
    /// The string specifing the folder in which to create alignment data. 
    /// </summary>
    public static string folderName = "/MV_V1";
}

public static class XML_Serialization
{

    /// <summary>
    /// The folder path for serialized data.  
    /// </summary>
    private static string DirPath
    {
        get { return Application.persistentDataPath + XMLFileNames.folderName; }
    }

    /// <summary>
    /// Generic XML Serializer.  
    /// </summary>
    /// <typeparam name="T"> The type of file to be serialized. </typeparam>
    /// <param name="type"> The file reference. </param>
    /// <param name="fileName"> The file name to serialize the data to. </param>
    public static void Serialize<T>(T type, string fileName)
    {
        Debug.Log("Serialization called");
        DirectoryCheck(DirPath);
        XmlSerializer s = new XmlSerializer(typeof(T));
        Stream stream = new FileStream(DirPath + fileName, FileMode.Create);
        s.Serialize(stream, type);
        stream.Close();
    }

    /// <summary>
    /// Generic XML Deserializer. 
    /// </summary>
    /// <typeparam name="T"> The file type to deserialze. </typeparam>
    /// <param name="fileName"> The name of the file to deserialize. </param>
    /// <returns> A deserialized instance of type T. </returns>
    public static T Deserialize<T>(string fileName)
    {
        Debug.Log("Deserialization called");
        DirectoryCheck(DirPath);
        XmlSerializer s = new XmlSerializer(typeof(T));
        Stream stream = new FileStream(DirPath + fileName, FileMode.OpenOrCreate);
        var output = s.Deserialize(stream);
        stream.Close();
        return (T)output;
    }

    /// <summary>
    /// Check if the specified directory exists within the project. 
    /// </summary>
    /// <param name="dirPath"> The path to check. </param>
    private static void DirectoryCheck(string dirPath)
    {
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
    }
}
