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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGoInside()
    {
        SceneManager.LoadScene(LocationSceneNumber);
    }

    public void ToggleVisibility()
    {
        gameObject.GetComponent<Image>().enabled = !gameObject.GetComponent<Image>().enabled;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = !gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled;
    }
}
