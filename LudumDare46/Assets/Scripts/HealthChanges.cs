using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChanges : MonoBehaviour
{
    //this script changes the health of the player/plant and plays the appropriate sounds when these events happen

    public GameObject healthHandler;
    public GameObject dialogManager;
    public GameObject gameHandler;

    public AudioSource healthAudio;
    public AudioClip fullHeal;
    public AudioClip playerDrink;
    public AudioClip plantDrink;
    public AudioClip badEvent;
    public AudioClip goodEvent;
    public AudioClip skeletonBoneScream;

    public AudioSource otherAudio;
    public AudioClip feastSound;

    public int soundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        //find the character icons object that manages health so that we can make edits to the health
        healthHandler = GameObject.Find("CharacterIcons");
    }

    // Update is called once per frame
    void Update()
    {
        //here, various unique things that change the health occur


        if (gameHandler.GetComponent<GameHandler>().currentScenario == "BanditTrio")
        {
            //healing after having food with the trio
            //also needs to track what index of dialog the sound is played on so we can prevent it from repeating the same sound eevry single frame
            if (dialogManager.GetComponent<DialogManager>().index == 10 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(fullHeal, 1f);
                healthHandler.GetComponent<HealthStates>().playerHealth = healthHandler.GetComponent<HealthStates>().maxPlayerHealth;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
            if (dialogManager.GetComponent<DialogManager>().index == 12 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(plantDrink, 1f);
                healthHandler.GetComponent<HealthStates>().plantHealth = healthHandler.GetComponent<HealthStates>().maxPlantHealth;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
            //the trio kill you
            if (dialogManager.GetComponent<DialogManager>().index == 20 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(badEvent, 1f);
                healthHandler.GetComponent<HealthStates>().playerHealth = 1;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }


        //water puddle
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "WaterPuddle")
        {
            //drink bad water, lose health
            if (dialogManager.GetComponent<DialogManager>().index == 7 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(badEvent, 1f);
                healthHandler.GetComponent<HealthStates>().playerHealth -= 1;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
            //give water to plant
            if (dialogManager.GetComponent<DialogManager>().index == 11 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(plantDrink, 1f);
                if (healthHandler.GetComponent<HealthStates>().plantHealth < healthHandler.GetComponent<HealthStates>().maxPlantHealth)
                {
                    healthHandler.GetComponent<HealthStates>().plantHealth += 1;
                }
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }


        //wasteland figure
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "WastelandFigure")
        {
            //give water
            if (dialogManager.GetComponent<DialogManager>().index == 11 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(badEvent, 1f);
                healthHandler.GetComponent<HealthStates>().plantHealth -= 1;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
            //give food
            if (dialogManager.GetComponent<DialogManager>().index == 17 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(badEvent, 1f);
                healthHandler.GetComponent<HealthStates>().playerHealth -= 1;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }



        //Arcadia
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "Arcadia")
        {
            //rest, plant loses hp, player gains hp
            if (dialogManager.GetComponent<DialogManager>().index == 5 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(goodEvent, 1f);
                healthHandler.GetComponent<HealthStates>().plantHealth -= 1;
                if (healthHandler.GetComponent<HealthStates>().playerHealth < healthHandler.GetComponent<HealthStates>().maxPlayerHealth)
                {
                    healthHandler.GetComponent<HealthStates>().playerHealth += 1;
                }
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }



        //birds
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "Birds")
        {
            //lose health if you chase the birds
            if (dialogManager.GetComponent<DialogManager>().index == 6 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(badEvent, 1f);
                healthHandler.GetComponent<HealthStates>().plantHealth -= 1;
                healthHandler.GetComponent<HealthStates>().playerHealth -= 1;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }

        //this again
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "ThisAgain")
        {
            //fully healed if you go to the real oasis
            if (dialogManager.GetComponent<DialogManager>().index == 4 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(fullHeal, 1f);
                healthHandler.GetComponent<HealthStates>().playerHealth = healthHandler.GetComponent<HealthStates>().maxPlayerHealth;
                healthHandler.GetComponent<HealthStates>().plantHealth = healthHandler.GetComponent<HealthStates>().maxPlantHealth;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }


        //deceased
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "WiseDeceased")
        {
            //play the scream
            if (dialogManager.GetComponent<DialogManager>().index == 7 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(skeletonBoneScream, 1f);
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }

        //grave situation
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "GraveSituation")
        {
            //find the hidden water
            if (dialogManager.GetComponent<DialogManager>().index == 5 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(goodEvent, 1f);
                if (healthHandler.GetComponent<HealthStates>().playerHealth < healthHandler.GetComponent<HealthStates>().maxPlayerHealth)
                {
                    healthHandler.GetComponent<HealthStates>().playerHealth += 1;
                }
                if (healthHandler.GetComponent<HealthStates>().plantHealth < healthHandler.GetComponent<HealthStates>().maxPlantHealth)
                {
                    healthHandler.GetComponent<HealthStates>().plantHealth += 1;
                }
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }



        //mourning
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "Mourning")
        {
            //plant lose 1 health by wasting time at the tree
            if (dialogManager.GetComponent<DialogManager>().index == 5 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(badEvent, 1f);
                healthHandler.GetComponent<HealthStates>().plantHealth -= 1;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }


        //feast
        if (gameHandler.GetComponent<GameHandler>().currentScenario == "Feast")
        {
            //plant lose 1 health by wasting time at the tree
            if (dialogManager.GetComponent<DialogManager>().index == 7 && soundPlayed != dialogManager.GetComponent<DialogManager>().index)
            {
                healthAudio.PlayOneShot(fullHeal, 1f);
                healthHandler.GetComponent<HealthStates>().playerHealth = healthHandler.GetComponent<HealthStates>().maxPlayerHealth;
                soundPlayed = dialogManager.GetComponent<DialogManager>().index;
            }
        }








    }
}
