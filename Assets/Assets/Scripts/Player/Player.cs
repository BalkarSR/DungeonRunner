using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MonoBehaviour, IDamageable
{
    //get a reference to the rigibody
    private Rigidbody2D playerRigid;

    public int hp { get; set; }

    //bool to check if we need to reset the jump or not
    private bool resetJump = false;

    //the y axis values for jumping
    [SerializeField]
    private float jumpValue = 5.0f;

    [SerializeField]
    //track of how many enemies killed
    public int totalEnemiesKilled = 0;

    //player speed
    [SerializeField]
    private float playerSpeed = 5f;

    //get the ground layer mask (gets the value from the unity GUI)
    [SerializeField]
    private LayerMask groundLayer;

    //get access to animation player object
    private Animation_Player animation_Player;

    //get a reference to Sprite Renderer to control which way the player will face on x axis
    private SpriteRenderer playerSpriteRenderer;

    //to get refernece to the sprite renderer of the sword arc effect
    private SpriteRenderer swordArcRenderer;

    //to get reference to the transform of sword arc effect
    private Transform swordArcTransform;

    //dummy bool, so we can call the ONGround method every frame
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        //set the rigidbody for the player
        playerRigid = GetComponent<Rigidbody2D>();

        //refernce to the Animation_Player class
        animation_Player = GetComponent<Animation_Player>();

        //get the sprite renderer of the Sprite under Player
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        //refer to 1st index in the array of Player objects, Sprite is 0, Sword_Arc is 1
        swordArcRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        swordArcTransform = transform.GetChild(1).GetComponent<Transform>();
        
        //set hp to 4
        hp = 4;
    }

    // Update is called once per frame
    void Update()
    {
        //player movement functionality
        PlayerMovement();

        //if left mouse button is clicked, and player is on the ground then attack
        if(CrossPlatformInputManager.GetButtonDown("A_Button") && ONGround())
        {
            animation_Player.Attack();
        }
    }

    //damage method
    public void Damage()
    {
        //if player is dead, then do nothing
        if(hp < 1) { return; }

        //when player is hit, remove 1 hp
        hp--;

        //update UI display
        UIManager.GetUIManager.UpdateHealthBars(hp);
        //check if player is dead

        //player is dead
        if(hp < 1)
        {
            //call death function to play 
            animation_Player.Death();
            playerSpeed = 0.0f;
            StartCoroutine(DoChangeScene(0, 3f));
        }


    }

    //wait before changing scene
    IEnumerator DoChangeScene(int sceneToChangeTo, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToChangeTo);
    }

    //Player horizontal movements & jump
    void PlayerMovement()
    {
        //Horizontal input for going left or right
        float xInput = CrossPlatformInputManager.GetAxis("Horizontal");

        //so it calls ONGround method every frame
        grounded = ONGround();

        //flip the character/other effects on x axis depending on which way they're going
        Flip(xInput);

        //In order to jump, check if space is pressed and check if the player is on the ground
        if ((CrossPlatformInputManager.GetButtonDown("B_Button") || Input.GetKeyDown("space")) && ONGround())
        {
            Debug.Log("Jumped!");

            //turn jump animation
            animation_Player.Jump(true);

            //player jumps and goes up vertically on the y axis
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpValue);

            //sets jump to true when the player is in the air, then after 1/10 of a second it sets it back to
            StartCoroutine(ResetJump());

        }

        // set the current velocity to the new one(i.e.x, y coordinates)
        //for horizontal movement
        playerRigid.velocity = new Vector2(xInput * playerSpeed, playerRigid.velocity.y);

        //check if x velocity is > 0 then call run animation, otherwise idle
        animation_Player.Run(xInput);
    

    }//end of PlayerMovement

    //flips the character
    void Flip(float xInput)
    {
        //if x > 0 means the player is going right
        if (xInput > 0)
        {
            //flip the character right
            playerSpriteRenderer.flipX = false;

            //flip the sword arc animation 
            swordArcRenderer.flipX = false;
            swordArcRenderer.flipY = false;

            //Get the current position of the sword arc effect
            Vector3 localPos = swordArcRenderer.transform.localPosition;
            localPos.x = 1.01f;
            swordArcRenderer.transform.localPosition = localPos;
        }
        else if (xInput < 0)
        {
            //flip the character left
            playerSpriteRenderer.flipX = true;

            //flip the sword arc animation 
            swordArcRenderer.flipX = true;
            swordArcRenderer.flipY = true;

            //Get the current position of the sword arc effect
            Vector3 localPos = swordArcRenderer.transform.localPosition;
            localPos.x = -1.01f;
            swordArcRenderer.transform.localPosition = localPos;
        }
    }

    //returns true if player is on ground, false otherwise
    bool ONGround()
    {
        //raycast to check what the player object is hitting (ground layer)
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer.value);

        //if player is not hitting anything then it means its on the ground
        if(raycastHit2D.collider != null)
        {
            //only return true if the player hasn't jumped
            if(resetJump == false)
            {
                //turn off jump animation
                animation_Player.Jump(false);
                return true;
            }

        }

        //player is not on the ground (in the air)
        return false;
    }


    //resets the jump
    IEnumerator ResetJump()
    {

        //as soon as you jump its true
        resetJump = true;

        yield return new WaitForSeconds(0.1f);

        //havent jumped yet
        resetJump = false;
    }
}
