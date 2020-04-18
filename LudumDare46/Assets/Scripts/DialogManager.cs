using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float baseTypingSpeed;
    public float typingSpeed;
    public GameObject parentNPC;
    public bool currentlyTyping = true;


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


    //
    void LateUpdate()
    {
        //repeatedly clear the text if not in conversation if there is text
       // if (parentNPC.GetComponent<NPCBasic>().chatting == false) { textDisplay.text = ""; textDisplay.enabled = false; }

        if (currentlyTyping == false && index == 0)
        {
           // textDisplay.text = "";
            //currentlyTyping = true;
            //StartCoroutine(Typing());
        }


        //continue dialog, make sure there actually is more conversation too
        if (Input.GetButtonDown("Fire1") && currentlyTyping == false && index <= sentences.Length - 1 && textDisplay.text == sentences[index])
        {
            //last index is blank so it looks like the conversation is finished
            //restart convo
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

        if (Input.GetButtonUp("Fire1"))
        {
            //typingSpeed = baseTypingSpeed;
        }

        }
}
