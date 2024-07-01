using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using System.IO;


[System.Serializable]
public class player
{
    public int HP, ATK, DEF, CP;
}


public class player_controller : MonoBehaviour
{
    public int HP, ATK, DEF, CP, current_HP;
    void Start()
    {
        Setting_PlayerStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting_PlayerStatus()
    {
        Load_Data();
    }

    public void Reduce_PlayerHP(int hp)
    {
        current_HP -= hp;
    }
    private void Load_Data()
    {
        string folderPath = Application.dataPath + "/Playerinfo";
        string filePath = folderPath + "/PlayerData.json";
        player gameData = new player();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, gameData);
            HP = gameData.HP;
            current_HP = HP;
            ATK = gameData.ATK;
            DEF = gameData.DEF;
            CP = gameData.CP;
        }
        else
        {
            HP =  1000;
            current_HP = 1000;
            ATK = 1000;
            DEF = 1000;
            CP = Mathf.CeilToInt(HP * 0.3f + ATK * 0.4f + DEF * 0.3f);
        }
    }
}
