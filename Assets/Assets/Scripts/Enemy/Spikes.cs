using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //method to find what the sword is hitting
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //anything our sword hits it prints out its object name
        Debug.Log("Hit " + collision.name);

        //get the player objects collision to the spikes
        Player player = collision.GetComponent<Player>();

        //if its not null, that means we hit the player
        if(player != null)
        {
            player.Damage();
            player.Damage();
            player.Damage();
            player.Damage();
        }

    }

}
