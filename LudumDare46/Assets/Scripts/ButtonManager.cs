﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ButtonManager : MonoBehaviour
{
    //manage the 3 buttons this object is parent to
    public GameObject leftButton;
    public GameObject middleButton;
    public GameObject rightButton;

    public TextMeshProUGUI leftButtonText;
    public TextMeshProUGUI middleButtonText;
    public TextMeshProUGUI rightButtonText;

    //when you click a button, it will tell this script what button has been pressed and do the appropriate action
    public int buttonPressed = 0;


    public GameObject dialogManager;

    public GameObject gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.SetActive(false);
        middleButton.SetActive(false);
        rightButton.SetActive(false);

        leftButton.GetComponent<Button>().onClick.AddListener(ClickLeftButton);
        middleButton.GetComponent<Button>().onClick.AddListener(ClickMiddleButton);
        rightButton.GetComponent<Button>().onClick.AddListener(ClickRightButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogManager.GetComponent<DialogManager>().makingChoice == true)
        {
            ShowButtons();
            //get the current scenario details and assign the buttons appropriately
            //after getting current scenario, check what part of the scenario the player is in, so that the buttons show the right choices
            //bandit trio
            if (gameHandler.GetComponent<GameHandler>().currentScenario == "BanditTrio")
            {
                //the first decision to be made, approaching the trio
                if (dialogManager.GetComponent<DialogManager>().choiceNumber == 0)
                {
                    leftButtonText.text = "Say hello.";
                    middleButtonText.text = "Steal their food in secrecy.";
                    rightButtonText.text = "Don't disturb them.";

                    //now for what happens when you actually make the choice
                    //your choice will make the index of the dialogmanagers array move to the appropriate spot depending on decision
                    //say hello
                    if (buttonPressed == 1)
                    {
                        HideButtons();
                        dialogManager.GetComponent<DialogManager>().index = 6;
                    }
                    //steal food
                    if (buttonPressed == 2)
                    {
                        HideButtons();
                        dialogManager.GetComponent<DialogManager>().index = 19;
                    }
                    //dont disturb
                    if (buttonPressed == 3)
                    {
                        HideButtons();
                        dialogManager.GetComponent<DialogManager>().index = 23;
                    }
                }

                //the second decision to be made, joining them for the evening
               else if (dialogManager.GetComponent<DialogManager>().choiceNumber == 1)
                {
                    middleButton.SetActive(false);
                    leftButtonText.text = "Accept the offer.";
                    rightButtonText.text = "Decline.";
                    //accepting the offer
                    if (buttonPressed == 1)
                    {
                        HideButtons();
                        dialogManager.GetComponent<DialogManager>().index = 10;
                    }
                    //declining the offer
                    if (buttonPressed == 3)
                    {
                        HideButtons();
                        dialogManager.GetComponent<DialogManager>().index = 15;
                    }
                }
            }
        }
       
    }

    //when a choice is made, hide the buttons and tell the dialogmanager the choice has been made
    void HideButtons()
    {
        leftButton.SetActive(false);
        middleButton.SetActive(false);
        rightButton.SetActive(false);
        dialogManager.GetComponent<DialogManager>().choiceNumber += 1;
        dialogManager.GetComponent<DialogManager>().makingChoice = false;
        buttonPressed = 0;
    }

    void ShowButtons()
    {
        leftButton.SetActive(true);
        middleButton.SetActive(true);
        rightButton.SetActive(true);
    }

    void ClickLeftButton()
    {
        buttonPressed = 1;
    }
    void ClickMiddleButton()
    {
        buttonPressed = 2;
    }
    void ClickRightButton()
    {
        buttonPressed = 3;
    }
}