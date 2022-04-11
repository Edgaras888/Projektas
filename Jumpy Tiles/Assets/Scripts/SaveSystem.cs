using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static string filepath = "/player.game";
    public static void SavePlayer(int highscore, int coins, int selectedSkin, bool[] ownedSkins)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + filepath;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(highscore, coins, selectedSkin, ownedSkins);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + filepath;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            PlayerData failed = new PlayerData(0, 0, 0, new bool[100]);
            return failed;
        }
    }
}
