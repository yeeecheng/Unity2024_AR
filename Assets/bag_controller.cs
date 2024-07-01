using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class bag_controller : MonoBehaviour
{
    public Canvas canvas;
    public Text col1_HP, col1_CP, col1_ATK, col1_DEF, col2_HP, col2_CP, col2_ATK, col2_DEF, col3_HP, col3_CP, col3_ATK, col3_DEF;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Load_Data();
    }


    public void Open_Bag()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Close_Bag()
    {
        canvas.gameObject.SetActive(false);
    }

    private void Load_Data()
    {
        string folderPath = Application.dataPath + "/Monsterinfo";
        string filePath = folderPath + "/MonsterData.json";
        Monster_Data gameData = new Monster_Data();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, gameData);
            for(int i = 0; i < gameData.all_monster_data.Length; i++)
            {
                if(i == 0)
                {

                    col1_CP.text = "CP: " + gameData.all_monster_data[i].CP.ToString();
                    col1_HP.text = "HP: " + gameData.all_monster_data[i].HP.ToString();
                    col1_ATK.text = "ATK: " + gameData.all_monster_data[i].ATK.ToString();
                    col1_DEF.text = "DEF: " + gameData.all_monster_data[i].DEF.ToString();
            
                }
                else if(i == 1)
                {
                    col2_CP.text = "CP: " + gameData.all_monster_data[i].CP.ToString();
                    col2_HP.text = "HP: " + gameData.all_monster_data[i].HP.ToString();
                    col2_ATK.text = "ATK: " + gameData.all_monster_data[i].ATK.ToString();
                    col2_DEF.text = "DEF: " + gameData.all_monster_data[i].DEF.ToString();
                }
                else if(i == 2)
                {
                    col3_CP.text = "CP: " + gameData.all_monster_data[i].CP.ToString();
                    col3_HP.text = "HP: " + gameData.all_monster_data[i].HP.ToString();
                    col3_ATK.text = "ATK: " + gameData.all_monster_data[i].ATK.ToString();
                    col3_DEF.text = "DEF: " + gameData.all_monster_data[i].DEF.ToString();
                    break;
                }
            }
        }
    }
}
