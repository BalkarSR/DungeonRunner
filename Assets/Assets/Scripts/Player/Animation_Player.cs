using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Player : MonoBehaviour
{

    //get access to the animator handle
    private Animator playerAnimation;

    //get referenece to Sword_Arc
    private Animator swordEffectAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //we want the children access (Player -> Sprite)
        playerAnimation = GetComponentInChildren<Animator>();

        //we want the Player -> Sword_Arc (we have to get the 1st index of the array as 0 is Sprite, 1 is Sword_Arc)
        swordEffectAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //every time the character runs, the running animation switches on
    public void Run(float x)
    {
        //gets the value of x velocity, and changes the move attribute of the animation to it
        playerAnimation.SetFloat("Move", Mathf.Abs(x));
    }

    //to play jump animation
    public void Jump(bool jump)
    {
        playerAnimation.SetBool("Jump", jump);
    }

    //play attack animation
    public void Attack()
    {
        playerAnimation.SetTrigger("Attack");
        swordEffectAnimation.SetTrigger("SwordEffect");
    }

    //play death animation
    public void Death()
    {
        playerAnimation.SetTrigger("Death");
    }
}
