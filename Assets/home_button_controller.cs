using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class home_button_controller : MonoBehaviour
{
    public Button btn_info, btn_bag, btn_combat;
    public Sprite img_btn_info_unpressed, img_btn_bag_unpressed, img_btn_combat_unpressed;
    public Sprite img_btn_info_pressed, img_btn_bag_pressed, img_btn_combat_pressed;
    private bool pressed_btn_info, pressed_btn_bag, pressed_btn_combat;
    void Start()
    {
        Reset_Btn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset_Btn()
    {
        pressed_btn_info = false;
        pressed_btn_bag = false;
        pressed_btn_combat = false;

        btn_info.image.sprite = img_btn_info_unpressed;
        btn_bag.image.sprite = img_btn_bag_unpressed;
        btn_combat.image.sprite = img_btn_combat_unpressed;
    }

    public void Beclicked(String name)
    {
        if (name == "info")
        {
            pressed_btn_info = true;
        }
        else if (name == "bag")
        {
            if(pressed_btn_info == false)
            {
                gameObject.GetComponent<bag_controller>().Open_Bag();
                pressed_btn_info = true;
            }
            else if (pressed_btn_info)
            {
                gameObject.GetComponent<bag_controller>().Close_Bag();
                pressed_btn_info = false;
            }
           

        }
        else if(name == "combat")
        {
            
            gameObject.GetComponent<all_controller>().Go_CombatScene();
            gameObject.GetComponent<audio_controller>().SoundEffectStop();
        }
        SetImg(name);
    }

    private void SetImg(String name)
    {
        if (name == "info")
        {
            btn_info.image.sprite = img_btn_info_pressed;
            btn_bag.image.sprite = img_btn_bag_unpressed;
            btn_combat.image.sprite = img_btn_combat_unpressed;
        }
        else if (name == "bag")
        {
            if (pressed_btn_info)
            {
                btn_bag.image.sprite = img_btn_bag_pressed;
                btn_info.image.sprite = img_btn_info_unpressed;
                btn_combat.image.sprite = img_btn_combat_unpressed;
            }
            else
            {
                btn_bag.image.sprite = img_btn_bag_unpressed;
                btn_info.image.sprite = img_btn_info_unpressed;
                btn_combat.image.sprite = img_btn_combat_unpressed;
            }
            
        }
        else if (name == "combat")
        {
            
            btn_bag.image.sprite = img_btn_bag_unpressed;
            btn_info.image.sprite = img_btn_info_unpressed;
        }
    }
}
