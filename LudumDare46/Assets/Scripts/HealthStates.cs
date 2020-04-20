using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStates : MonoBehaviour
{
    public Texture[] playerImages;
    public Texture[] plantImages;

    public RawImage playerIcon;
    public RawImage plantIcon;

    public int playerHealth = 4;
    public int plantHealth = 4;
    public int maxPlayerHealth = 4;
    public int maxPlantHealth = 4;

    public bool hasFox = false;
    public bool hasFakeOasis = false;
    public bool hasSkeleton = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //use the right condition image depending on health of the player and plant
        playerIcon.texture = playerImages[playerHealth - 1];
        plantIcon.texture = plantImages[plantHealth - 1];

        //limit max health
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        if (plantHealth > maxPlantHealth)
        {
            plantHealth = maxPlantHealth;
        }
        //prevent health from going below 0
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }

        if (plantHealth < 0)
        {
            plantHealth = 0;
        }

        //debugging the health
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            playerHealth -= 1;
            plantHealth -= 1;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerHealth += 1;
            plantHealth += 1;
        }
        */
    }
}
