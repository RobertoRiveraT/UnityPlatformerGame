using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;



[System.Serializable]
public static class SaveSystem 
{
    public static bool died;

    public static void Salvar(PlayerController player){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        Game data = new Game(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Game Cargar(){
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Game data = formatter.Deserialize(stream) as Game;
            stream.Close();
            return data;
        }else{
            Debug.Log("Save File not found");
            return null;
        }
    }
}
