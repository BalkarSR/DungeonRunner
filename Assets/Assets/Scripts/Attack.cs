using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool coolDown = false;

    //method to find what the sword is hitting
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //anything our sword hits it prints out its object name
        Debug.Log("Hit " + collision.name);

        //this checks to see if the object we hit, contains the IDamageable interface
        //therefore it is an enemy
        IDamageable check = collision.GetComponent<IDamageable>();

        //if check is not null, means it hit an object that has IDamageable interface
        if (check != null)
        {
            //if cooldown is not true, it means we can attack (to prevent spam attacks)
            if (!coolDown)
            {
                //call the damage function on the object hit but also check if the object hit isn't already dead
                if(check.hp >= 1)
                {
                    check.Damage();
                }

                coolDown = true;

                //reset the cooldown
                StartCoroutine(CoolDownTimer());
            }
        }
    }

    //timer to change coolDown on false
    IEnumerator CoolDownTimer()
    {
        //wait .5 seconds then set the cooldown to false
        yield return new WaitForSeconds(0.5f);
        coolDown = false;
    }
}
