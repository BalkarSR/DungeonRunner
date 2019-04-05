using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //UIManager object to be accessed by other classes
    private static UIManager uIManager;

    //img array of all lives
    //images will be assigned in the inspector
    public Image[] healthBars;

    public static UIManager GetUIManager
    {
        get
        {
            //check if its null
            if(uIManager == null)
            {
                //log the error
                Debug.LogError("UIManager is null");
            }
            return uIManager;
        }
    }

    //to see if all enemies have been killed
    public bool HasKilledAllEnemies { get; set; }

    //assign the UImanager to this script
    private void Awake()
    {
        uIManager = this;
    }

    //to update the lives in the HUD
    public void UpdateHealthBars(int livesRemaining)
    {
        //loop through lives to find remaining values 
        for(int i = 0; i <= livesRemaining; i++)
        {
            //do nothing untik we hit the max
            if(i == livesRemaining)
            {
                //hide the lifebar
                healthBars[i].enabled = false; 
            }
        }
    }
}
