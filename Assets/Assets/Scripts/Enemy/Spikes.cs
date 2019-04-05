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

        //this checks to see if the object we hit, contains the IDamageable interface
        //therefore it is an enemy
        IDamageable check = collision.GetComponent<IDamageable>();

        if(check != null)
        {
            check.Damage();
            check.Damage();
            check.Damage();
            check.Damage();
        }

    }

}
