using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioArt : MonoBehaviour
{

    public RawImage myArt;
    public float imageAlpha;
    public GameObject dialogManager;
    public GameObject gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        myArt = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        //if a fadeout of the art has started, finish it
        if (myArt.color.a <= 0.9f)
        {
            myArt.color = new Color(1, 1, 1, myArt.color.a - Time.deltaTime * 3);
        }

        //here, various unique things that change the art during a scene will occur
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "BanditTrio")
        {
            //the trio leave after you join their little feast
            if (dialogManager.GetComponent<DialogManager>().index == 11 && myArt.color.a == 1)
            {
                //start the fadeout by making it slightly transparent, then the update function will take care of the rest of the fadeout
                myArt.color = new Color(1, 1, 1, 0.9f);
            }
        }
     }
}
