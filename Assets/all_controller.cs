using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class all_controller : MonoBehaviour
{
    public Canvas home_canavas;
    public Text cp_text, hp_text, atk_text, def_text;
    public GameObject player, monster;
    public player_controller pc;

    private bool has_combating = false;

    public void Go_CombatScene()
    {
        home_canavas.gameObject.SetActive(false);
        player.SetActive(false);
        monster.gameObject.SetActive(true);
        has_combating = true;
    }

    public void Go_Home()
    {
        home_canavas.gameObject.SetActive(true);
        player.SetActive(true);
        monster.gameObject.SetActive(false);
        has_combating = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!has_combating)
        {
            Load_Data();
        }
    }


    private void Load_Data()
    {
        string folderPath = Application.dataPath + "/Playerinfo";
        string filePath = folderPath + "/PlayerData.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, pc);
            cp_text.text = "CP: "+pc.CP.ToString();
            hp_text.text = "HP: " + pc.HP.ToString();
            atk_text.text = "ATK: " + pc.ATK.ToString();
            def_text.text = "DEF: " + pc.DEF.ToString();  
        }
        else
        {
            cp_text.text = "CP: 1000";
            hp_text.text = "HP: 1000";
            atk_text.text = "ATK: 1000";
            def_text.text = "DEF: 1000";
        }
    }
}

