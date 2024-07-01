
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class combat : MonoBehaviour
{
    public GameObject player, monster;
    public float combat_timer, new_game_delay_timer;
    public int round = 1;
    public Canvas canvas;
    private bool isBattling = false;

    public Animator monster_animator, player_animator;
    public audio_controller combat_bgm;

    private float combat_timer_upper = 6.0f, new_game_delay_timer_upper = 10.0f;
    private int player_action, monster_action;
    private monster_controller mc;
    private player_controller pc;
    void Start()
    {
        mc = monster.GetComponent<monster_controller>();
        pc = player.GetComponent<player_controller>();
        New_CombatGame();
        // first game don't need wait
        new_game_delay_timer = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {

        if (!Chk_NewGame())
        {
            new_game_delay_timer -= Time.deltaTime;
            //Debug.Log(new_game_delay_timer.ToString("#0.00"));
            if(new_game_delay_timer <= 0.0f)
            {
                canvas.GetComponent<combat_UI>().change_status(true);
                combat_bgm.SoundEffectPlay();
            }
        }
        else
        {
            if (isBattling)
            {
                combat_timer -= Time.deltaTime;
                // combat
                if (combat_timer <= 0)
                {
                    // monster 3 choose 1 algorithm
                    Update_MonsterAction();
                    Debug.Log(player_action.ToString() + " " + monster_action.ToString());
                    // combat result
                    Get_CombatRes();
                    // check end game
                    if (!Check_EndGame())
                    {
                        // next round();
                        Next_Round();
                    }
                }
            }
        }
    

    }

    public void Update_Battling_status(bool status)
    {
        isBattling = status;
    }

    public bool Get_Battling_status()
    {
        return isBattling;
    }

    public bool Chk_NewGame()
    {
        return (new_game_delay_timer <= 0); 
    }

    public void Leave_Game()
    {
        Debug.Log("leave");
        // fill HP
        pc.current_HP = pc.HP;
        // create new monster
        mc.Setting_MonsterStatus();
        // reset combat status
        New_CombatGame();
        // delay
        canvas.GetComponent<combat_UI>().change_status(false);
        combat_bgm.SoundEffectStop();
        
    }

    private void Save_MonsterData()
    {
        string folderPath = Application.dataPath + "/Monsterinfo";
        string filePath = folderPath + "/MonsterData.json";

        // 創建資料夾如果它不存在
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        monster m_data = new monster();
        m_data.HP = mc.HP;
        m_data.CP = mc.CP;
        m_data.ATK = mc.ATK;
        m_data.DEF  = mc.DEF;
        m_data.current_HP = mc.HP;

        // 讀取現有的資料
        Monster_Data all_monster_data = new Monster_Data();
        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            all_monster_data = JsonUtility.FromJson<Monster_Data>(existingJson);
        }

        if (all_monster_data.all_monster_data == null)
        {
            all_monster_data.all_monster_data = new monster[0];
        }
        List<monster> temp = new List<monster>(all_monster_data.all_monster_data);
        temp.Insert(0, m_data);
        all_monster_data.all_monster_data = temp.ToArray();
        // 將更新後的陣列寫回檔案
        string json = JsonUtility.ToJson(all_monster_data);

        File.WriteAllText(filePath, json);

        Debug.Log("Monster data appended to JSON.");
    }


    private void Save_PlayerData()
    {
        string json = JsonUtility.ToJson(pc);
        string folderPath = Application.dataPath + "/Playerinfo";
        Debug.Log(folderPath);
        string filePath = folderPath + "/PlayerData.json";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        File.WriteAllText(filePath, json);

        Debug.Log("Player data saved to JSON.");
    }
    private bool Check_EndGame()
    {
        if(mc.current_HP > 0 && pc.current_HP > 0){
            return false;
        }
        if(mc.current_HP <= 0)
        {
            monster_animator.SetTrigger("die");
            // player status level up
            pc.HP += mc.HP / round;
            pc.ATK += mc.ATK / round;
            pc.DEF += mc.DEF / round;
            pc.CP = Mathf.CeilToInt(pc.HP * 0.3f + pc.ATK * 0.4f + pc.DEF * 0.3f);

            // get monster 
            Save_MonsterData();

        }
        else if (pc.current_HP <= 0)
        {
            player_animator.SetTrigger("die");
            // player status level up
            pc.HP -= mc.HP / round;
            pc.ATK -= mc.ATK / round;
            pc.DEF -= mc.DEF / round;
            pc.CP = Mathf.CeilToInt(pc.HP * 0.3f + pc.ATK * 0.4f + pc.DEF * 0.3f);
        }
        Save_PlayerData();
        // fill HP
        pc.current_HP = pc.HP;
        // create new monster
        mc.Setting_MonsterStatus();
        // reset combat status
        New_CombatGame();
        // delay
        canvas.GetComponent<combat_UI>().change_status(false);
        combat_bgm.SoundEffectStop();
        return true;
    }
    private void Update_MonsterAction()
    {
        monster_animator.SetTrigger("choose_attack");
        monster_action = Random.Range(0, 2);
    }
    public void Update_PlayerAction(int action)
    {
        player_animator.SetTrigger("choose_attack");
        player_action = action;
    }
    public void New_CombatGame()
    {
        round = 1;
        player_action = -1;
        monster_action = -1;
        combat_timer = 0;
        isBattling = false;
        new_game_delay_timer = new_game_delay_timer_upper;
    }
    private void Get_CombatRes()
    {
        // paper: 0, scissor: 1, stone: 2


        if (player_action == -1)
        {
            // player time out.
            Debug.Log("monster win this round");
            pc.Reduce_PlayerHP(mc.ATK);
            monster_animator.SetTrigger("attack");
            player_animator.SetTrigger("take_attack");
            return;
        }

 
        if (player_action == monster_action)
        {
            monster_animator.SetTrigger("draw");
            player_animator.SetTrigger("draw");
            // draw -> do nothing.
            Debug.Log("Draw this round");
        }
        else if ((player_action == 0 && monster_action == 2) || (player_action == 1 && monster_action == 0) || (player_action == 2 && monster_action == 1))
        {
            // player win
            Debug.Log("player win this round");
            int reduce_hp = pc.ATK - mc.DEF;

            if(reduce_hp < 0)
            {
                reduce_hp = 1;
            }
            Debug.Log(reduce_hp.ToString());
            // attack / receive damage anime
            mc.Reduce_MonsterHP(reduce_hp);
            monster_animator.SetTrigger("take_attack");
            player_animator.SetTrigger("attack");
        }
        else
        {
            // monster win
            Debug.Log("monster win this round");
            int reduce_hp = mc.ATK - pc.DEF;

            if (reduce_hp < 0)
            {
                reduce_hp = 1;
            }

            pc.Reduce_PlayerHP(reduce_hp);
            monster_animator.SetTrigger("attack");
            player_animator.SetTrigger("take_attack");
        }

    }
    
    private void Next_Round()
    {
        player_action = -1;
        monster_action = -1;
        round += 1;
        combat_timer = combat_timer_upper;
        GetComponent<button_controller>().Reset_Btn();
    }

    

}
