using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Create a vector3 object to get current Target positions
    public Vector3 targetLoc;

    //ref to animator of the moss sprite
    public Animator animator;

    //get sprite render ref
    public SpriteRenderer spriteRenderer;

    //the common attributes across enemies
    public int hp, speed, gems;

    [SerializeField]
    public Transform locA, locB;

    //to start and initalize the objects
    public void Start()
    {
        Init();
    }

    //to ensure every enemy we create in the future can include this method
    public virtual void Update()
    {
        //call the movement for all enemies
        Movement();
    }

    public virtual void Movement()
    {
        //checks if idle animation is not playing, then start moving
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {

            //flip the moss accordingly to which way its going
            if (targetLoc == locA.position)
            {
                //flip the moss on the x axis
                spriteRenderer.flipX = true;
            }
            else
            {
                //flip the moss on the x axis
                spriteRenderer.flipX = false;
            }

            //if the current position is at point A
            if (transform.position == locA.position)
            {
                //set the target location to point B
                targetLoc = locB.position;

                //call the idle animation
                animator.SetTrigger("Idle");
            }

            //if the current position is at point B
            else if (transform.position == locB.position)
            {
                //set the target location to point A
                targetLoc = locA.position;

                //call the idle animation
                animator.SetTrigger("Idle");
            }

            //move the character to the targetLoc
            transform.position = Vector3.MoveTowards(transform.position, targetLoc, speed * Time.deltaTime);
        }

    }

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
