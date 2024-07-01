using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class button_controller : MonoBehaviour
{
    public Button btn_paper, btn_scissors, btn_stone, btn_leave;
    public Sprite img_btn_paper_unpressed, img_btn_scissors_unpressed, img_btn_stone_unpressed, img_btn_leave_unpressed;
    public Sprite img_btn_paper_pressed, img_btn_scissors_pressed, img_btn_stone_pressed, img_btn_leave_pressed;
    private bool pressed_btn_paper, pressed_btn_scissors, pressed_btn_stone, pressed_btn_leave;
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
        pressed_btn_paper = false;
        pressed_btn_scissors = false;
        pressed_btn_stone = false;
        pressed_btn_leave = false;

        btn_paper.image.sprite = img_btn_paper_unpressed;
        btn_stone.image.sprite = img_btn_stone_unpressed;
        btn_scissors.image.sprite = img_btn_scissors_unpressed;
        btn_leave.image.sprite =img_btn_leave_unpressed;
    }

    public void Beclicked(String name)
    {
        if(name == "paper")
        {
            pressed_btn_paper = true;
            pressed_btn_scissors = false;
            pressed_btn_stone = false;
            GetComponent<combat>().Update_PlayerAction(0);
        }
        else if(name == "scissors")
        {
            pressed_btn_scissors = true;
            pressed_btn_paper = false;
            pressed_btn_stone = false;
            GetComponent<combat>().Update_PlayerAction(1);
        }
        else if(name == "stone")
        {
            pressed_btn_stone = true;
            pressed_btn_scissors = false;
            pressed_btn_paper = false;
            GetComponent<combat>().Update_PlayerAction(2);
        }
        else if(name == "leave")
        {
            pressed_btn_leave = true;
            GetComponent<combat>().Leave_Game();
        }
        SetImg(name);
    } 

    private void SetImg(String name)
    {
        if(name == "paper")
        {
            btn_paper.image.sprite = img_btn_paper_pressed;
            btn_stone.image.sprite = img_btn_stone_unpressed;
            btn_scissors.image.sprite = img_btn_scissors_unpressed;
        }
        else if(name == "scissors")
        {
            btn_scissors.image.sprite = img_btn_scissors_pressed;
            btn_paper.image.sprite = img_btn_paper_unpressed;
            btn_stone.image.sprite = img_btn_stone_unpressed;
        }
        else if(name == "stone")
        {
            btn_stone.image.sprite = img_btn_stone_pressed;
            btn_paper.image.sprite = img_btn_paper_unpressed;
            btn_scissors.image.sprite = img_btn_scissors_unpressed;
        }
        else if (name == "leave")
        {
            btn_paper.image.sprite = img_btn_paper_unpressed;
            btn_stone.image.sprite = img_btn_stone_unpressed;
            btn_scissors.image.sprite = img_btn_scissors_unpressed;
            btn_leave.image.sprite = img_btn_leave_pressed;
        }
    }
}
