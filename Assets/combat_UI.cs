using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class combat_UI : MonoBehaviour
{
    public Text monster_CP_text, player_CP_text, TimerText, RoundText;
    public GameObject monster, combat_manager, player;
    monster_controller mc;
    player_controller pc;
    public Slider monster_healthBar, player_healthBar;

    private bool hasScanned = false;
    private bool Isbattling = false; //Isbattling use to configure if battling


    void Start()
    {
        mc = monster.GetComponent<monster_controller>();
        pc = player.GetComponent<player_controller>();
        // monster_CP_text.enabled = false;
        // player_CP_text.enabled = false;

        
    }

    void Update()
    {
        if (combat_manager.GetComponent<combat>().Chk_NewGame())
        {

            // first scanned 
            if (monster.activeSelf && !hasScanned && !Isbattling)
            {
                Debug.Log("scann");
                hasScanned = true;
                monster_CP_text.text = $"CP: {mc.CP}";
                player_CP_text.text = $"CP: {pc.CP}";
                combat_manager.GetComponent<combat>().Update_Battling_status(true);
            }
            else
            {
                Update_HealthBar();
                Update_Round();
                Update_Timer();
                UpdateIsBattling(combat_manager.GetComponent<combat>().Get_Battling_status());
            }

        }
    }

    public void change_status(bool new_status)
    {
        monster.SetActive(new_status);
        player.SetActive(new_status);
        gameObject.SetActive(new_status);
    }
    public void UpdateIsBattling(bool isBattling)
    {
        Isbattling = isBattling;

        if (!Isbattling)
        {
            hasScanned = false; // if the battle is end and scan the card again the number will random again
        }
    }

    public void Update_HealthBar()
    {

        // Debug.Log("UI current HP " + mc.current_HP.ToString());
        monster_healthBar.maxValue = mc.HP;
        monster_healthBar.value = mc.current_HP;
        player_healthBar.maxValue = pc.HP;
        player_healthBar.value = pc.current_HP;
    }

    public void Update_Round()
    {
        RoundText.text = $"Round: {combat_manager.GetComponent<combat>().round}";
    }

    public void Update_Timer()
    {
        TimerText.text = $"Timer: " + combat_manager.GetComponent<combat>().combat_timer.ToString("#0.00");
    }
}
