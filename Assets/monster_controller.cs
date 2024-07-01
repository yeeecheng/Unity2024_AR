using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster_Data
{
    public monster[] all_monster_data;
}

[System.Serializable]
public class monster
{
    public int HP, ATK, DEF, CP, current_HP;
    public string monster_type = "skeleton";
}

public class monster_controller : MonoBehaviour
{
    public int HP, ATK, DEF, CP, current_HP;
    public string monster_type = "skeleton";
    
    void Start()
    {
        Setting_MonsterStatus();
    }

    void Update()
    {
        
    }

    public void Setting_MonsterStatus()
    {
        HP = Random.Range(20, 30);
        current_HP = HP;
        ATK = Random.Range(20, 30);
        DEF = Random.Range(20, 30);
        CP = Mathf.CeilToInt(HP * 0.3f + ATK * 0.4f + DEF * 0.3f);
    }

    public void Reduce_MonsterHP(int hp)
    {
        current_HP -= hp;
        Debug.Log("current HP " + current_HP.ToString());
    }


}
