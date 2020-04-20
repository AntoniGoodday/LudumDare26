using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GoInsideScript : MonoBehaviour
{
    [SerializeField]
    int locationSceneNumber = 5;

    public int LocationSceneNumber { get => locationSceneNumber; set => locationSceneNumber = value; }
    public string destinationScene;
    public GameObject playerObject;
    public TextMeshProUGUI buttonText;
    public bool insideScenario = false;

    [SerializeField]
    List<Image> buttonWithShadow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //disable the button if there is no destination scene or while player is moving on map
        if (destinationScene == "" || playerObject.GetComponent<PlayerMapScript>().isMoving == true)
        {
            DisableVisibility();
        }
        else
        if (destinationScene != "" && playerObject.GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().markerActive == true)
        {
            EnableVisibility();
        }
        //the button on the text
        if (playerObject.GetComponent<PlayerMapScript>().CurrentLocation != null)
        {
            buttonText.text = playerObject.GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().destinationTitle;
        }
    }

    public void OnGoInside()
    {
        insideScenario = true;
        if (destinationScene != "Ending")
        {
        SceneManager.LoadScene(destinationScene, LoadSceneMode.Additive);
        //set the markers destination scene as blank so you cant repeat the same scenario over and over
        destinationScene = "";
        }
        else if (destinationScene == "Ending")
        {
            SceneManager.LoadScene("Ending");
        }

    }

    public void EnableVisibility()
    {
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        foreach(Image i in buttonWithShadow)
        {
            i.enabled = true;
        }
    }
    public void DisableVisibility()
    {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        foreach (Image i in buttonWithShadow)
        {
            i.enabled = false;
        }
    }
}
