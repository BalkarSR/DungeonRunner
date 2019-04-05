using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //create an instance of gamemanager that can be used by all classes
    private static GameManager gameManager;

    //getter for the gamemanager
    public static GameManager GetGameManager
    {
        get
        {
            //check for null
            if(gameManager == null)
            {
                //log the error
                Debug.LogError("Game manager is null!");
            }
            return gameManager;
        }
    }

    //assign the gamemanager to this script
    private void Awake()
    {
        gameManager = this;
    }
}
