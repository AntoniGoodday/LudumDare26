using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float baseTypingSpeed;
    public float typingSpeed;
    public GameObject proceedButton;
    public bool currentlyTyping = true;
    public int choiceNumber = 0;
    public bool makingChoice = false;


    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Typing());
        baseTypingSpeed = typingSpeed;
    }

    IEnumerator Typing()
    {

        foreach (char letter in sentences[index].ToCharArray())
        {
            if (currentlyTyping)
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
                Debug.Log("typing");
            }
        }
        currentlyTyping = false;
    }

    void LateUpdate()
    {
        //make proceed button visbile if text has stopped typing, invisible when text is still typing
        if (currentlyTyping == false && textDisplay.text == sentences[index])
        {
            proceedButton.GetComponent<RawImage>().enabled = true;
        }
        if (currentlyTyping == true || makingChoice == true)
        {
            proceedButton.GetComponent<RawImage>().enabled = false;
        }

        if (makingChoice == false)
        { 
            //this starts up the typing again after making a button choice
            if (currentlyTyping == false && textDisplay.text != sentences[index])
            {
                textDisplay.text = "";
                currentlyTyping = true;
                StartCoroutine(Typing());
            }

            //continue dialog, make sure there actually is more conversation too
            if (Input.GetButtonDown("Fire1") && currentlyTyping == false && index <= sentences.Length - 1 && textDisplay.text == sentences[index])
            {
                //last index is blank so it looks like the conversation is finished
                index ++;
                textDisplay.text = "";
                currentlyTyping = true;
                StartCoroutine(Typing());
            }

            else

            //skip text if press left click while its still typing
            if (Input.GetButtonDown("Fire1") && currentlyTyping == true)
            {
                StopCoroutine(Typing());
                textDisplay.text = sentences[index];
                currentlyTyping = false;
            }
        }
        //ending dialogue and returning to map screen. When you want dialogue to end, make the index ahead of the current one say "End"
        if (Input.GetButtonDown("Fire1") && currentlyTyping == false && sentences[index + 1] == "End")
        {
            Debug.Log("Return to map");
        }

            //the game will now a choice is active when the string after the current string in the array is null with no text
            if (currentlyTyping == false && textDisplay.text == sentences[index] && sentences[index + 1] == "")
        {
            Debug.Log("Choice");
            makingChoice = true;
        }

    }
}
