namespace pacman;
using System.IO;
using System.Xml.Serialization;
public class XmlManager
{


public void SaveGame(SaveData saveData, string filePath)
{
    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
    using (FileStream fs = new FileStream(filePath, FileMode.Create))
    {
        serializer.Serialize(fs, saveData);
    }
}
public SaveData LoadGame(string filePath)
{
    if (File.Exists(filePath))
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            return (SaveData)serializer.Deserialize(fs);
        }
    }
    return null;
}


}