using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject gameHandler;

    public AudioSource dialogSound;
    public AudioClip typeNoise;


    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        dialogSound = GetComponent<AudioSource>();
        dialogSound.clip = typeNoise;
        StartCoroutine(Typing());
        baseTypingSpeed = typingSpeed;
    }

    IEnumerator Typing()
    {
        dialogSound.Play(0);
        foreach (char letter in sentences[index].ToCharArray())
        {
            if (currentlyTyping)
            {
                textDisplay.text += letter;
                //dialogSound.PlayOneShot(typeNoise, 1f);
                yield return new WaitForSeconds(typingSpeed * 1.7f);
                Debug.Log("typing");
            }
        }
        dialogSound.Pause();
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

        //ending dialogue and returning to map screen. When you want dialogue to end, make the index ahead of the current one say "End"
        if (Input.GetButtonDown("Fire1") && currentlyTyping == false && textDisplay.text == sentences[index] && sentences[index + 1] == "End")
        {
            //check if player alive
            if (SceneManager.GetActiveScene().name != "Ending" && SceneManager.GetActiveScene().name != "Intro" && SceneManager.GetActiveScene().name != "GameOver")
            {
                //going from dialogue to game over screen if you are dead
                if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().playerHealth <= 1)
                {
                    Debug.Log("Lose");
                    SceneManager.LoadScene("GameOver");
                }
                if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().plantHealth <= 1)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
            //check if on ending scene
            if (SceneManager.GetActiveScene().name == "Ending" || SceneManager.GetActiveScene().name == "GameOver")
                {
                    SceneManager.LoadScene("CreditsScene");
                }
                //if on intro scene, go to map
                if (SceneManager.GetActiveScene().name == "Intro")
                {
                    SceneManager.LoadScene("MapScene");
                }
                if (SceneManager.GetActiveScene().name != "Ending" && SceneManager.GetActiveScene().name != "Intro" && SceneManager.GetActiveScene().name != "GameOver")
                {
                    SceneManager.UnloadSceneAsync(gameHandler.GetComponent<GameHandler>().currentScenario);
                    Debug.Log("Return to map");
                    GameObject.Find("PlayerMarker").GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().destinationScene = "";
                    GameObject.Find("PlayerMarker").GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().DisableMarkers();
                    GameObject.Find("PlayerMarker").GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().ToggleMarkers();
                    GameObject.Find("GoInside").GetComponent<GoInsideScript>().insideScenario = false;
                }
            
        }

   else

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
                if (SceneManager.GetActiveScene().name != "Ending" && SceneManager.GetActiveScene().name != "Intro" && SceneManager.GetActiveScene().name != "GameOver")
                {
                    //going from dialogue to game over screen if you are dead
                    if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().playerHealth <= 1)
                    {
                        Debug.Log("Lose");
                        SceneManager.LoadScene("GameOver");
                    }
                    if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().plantHealth <= 1)
                    {
                        SceneManager.LoadScene("GameOver");
                    }
                }
                //last index is blank so it looks like the conversation is finished
                index++;
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
            //the game will now a choice is active when the string after the current string in the array is null with no text
            if (currentlyTyping == false && textDisplay.text == sentences[index] && sentences[index + 1] == "")
        {
            Debug.Log("Choice");
            makingChoice = true;
        }

    }
}
