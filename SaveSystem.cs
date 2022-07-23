using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    private static string path = Application.persistentDataPath + "/pd.bevo";
    public static void SavePlayer(Player player) {
        BinaryFormatter fom = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData pd = new PlayerData(player);

        fom.Serialize(stream, pd);
        stream.Close();
    }

    public static PlayerData LoadPlayer() {
        if (File.Exists(path)) {
            BinaryFormatter fom = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData pd = fom.Deserialize(stream) as PlayerData;
            stream.Close();

            return pd;
        } else {
            Debug.LogError("No save data in " + path);
            return null;
        }
    }
}
