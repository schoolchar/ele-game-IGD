using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables
    //sets moveSpeed
    public float moveSpeed;

    //sets drag for player, 0 drag slippery like ice.
    public float groundDrag;

    //gets the orientation 
    public Transform orientation;
    public Transform playerPhy;

    //players height
    public float playerHeight;

    //how high jump
    public float playerJumpForce;

    //how long after jump till can jump again
    public float jumpCooldown;

    //for when in air movement, after jumping
    public float airMultiplier;

    //sets if can jump
    bool readyToJump;

    public KeyCode jumpButton = KeyCode.Space;

    //decides if on ground for jumping on layers
    public LayerMask isGround;

    //if player is touching ground
    bool grounded;

    //up/down left/right inputs
    float hInput;
    float vInput;

    //variable for direction
    Vector3 moveDirection;

    //ref for rigidbody
    Rigidbody rb;

    //Animation
    public Animator animatorLion;
    public Animator animatorSeal;
    public Animator animatorMonkey;
    public Animator animatorElephant;

    //Weapon upgrades
    public GameObject ringOfFire;
    public Knifethrow knifeThrow;
    public LargeHammer hammer;
    public Turret turret;
    public Nuke nuke;

    //Upgrade management
    [SerializeField] private SaveData saveData;

    //Character active
    public GameObject characterActive;

    // on start up, i may be over-commenting
    private void Start()
    {
        InitValues();

       // animatorLion = GameObject.FindGameObjectWithTag("Lion").GetComponent<Animator>();
/*        animatorSeal = GameObject.FindGameObjectWithTag("Seal").GetComponent<Animator>();
        animatorMonkey = GameObject.FindGameObjectWithTag("Monkey").GetComponent<Animator>();
        animatorElephant = GameObject.FindGameObjectWithTag("Elephant").GetComponent<Animator>();*/

        //animatorLion.SetBool("IsMoving", false);
           /* animatorSeal.SetBool("IsMoving", false);
            animatorMonkey.SetBool("IsMoving", false);
            animatorElephant.SetBool("IsMoving", false);*/
    }

    //goes every update
    private void Update()
    {
        inputs();
        speedLimit();

        //makes a raycast to see if touching ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, isGround);
        //Debug.Log("Grounded: " + grounded);

        //makes drag only if touching ground, not in air
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    //also every update but different
    private void FixedUpdate()
    {
        moving();

        if(moveDirection.magnitude >= 0.01f)
        {
            animatorLion.SetBool("IsMoving", true);
            animatorSeal.SetBool("IsMoving", true);
            animatorMonkey.SetBool("IsMoving", true);
            animatorElephant.SetBool("IsMoving", true);
        }
        else
        {
            animatorLion.SetBool("IsMoving", false);
            animatorSeal.SetBool("IsMoving", false);
            animatorMonkey.SetBool("IsMoving", false);
            animatorElephant.SetBool("IsMoving", false);
        }
    }

    public void InitValues()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Set as delegate for event
        PlayerHealth.onPlayerDeath += CALLBACK_ResetWeapons;
        if(saveData.speed.level > 0)
        {
            moveSpeed += saveData.speed.affectOnSpeed;
        }
        //Do not dstroy player
        DontDestroyOnLoad(this.gameObject);
    }

    //gets if player is using horizontal or vertical inputs
    private void inputs()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //if jump button pressed and on ground
        if (Input.GetKey(jumpButton) && grounded && readyToJump)
        {
            readyToJump = false;

            //jump();

            //calls the jumpReset function, delayed by jumpCooldown variable
            Invoke(nameof(jumpReset), jumpCooldown);
        }
    }

    //moves the player by adding force and using the orientation input for direction where to go
    private void moving()
    {
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        //changes amount of force if on air or on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    //limits players speed
    private void speedLimit()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //makes player jump
    private void jump()
    {
        //sets y velocity to 0 so always jumping same height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //up force set by playerJumpForce, impulse so only once
        rb.AddForce(transform.up * playerJumpForce, ForceMode.Impulse);
        //Debug.Log("Jump Force: " + playerJumpForce + ", jump");
    }

    //sets readyToJump to true so player can jump again
    private void jumpReset()
    {
        readyToJump = true;
    }
    /// <summary>
    /// Reset weapons to be off on player death
    /// </summary>
    public void CALLBACK_ResetWeapons()
    {
        //Set all to enabled so that when reloading game, weapon manager can access them
        ringOfFire.SetActive(true);
        ringOfFire.GetComponent<Ringoffire>().fireActive = false;
        knifeThrow.hasKnife = false;
        hammer.hammerActive = false;
        hammer.StopAllCoroutines();
        hammer.interval = -1;
        knifeThrow.enabled = true;
        turret.enabled = true;
        turret.StopAllCoroutines();
        turret.active = false;
        nuke.enabled = true; ;
        nuke.active = false;
        nuke.StopAllCoroutines();
    } //END ResetWeapons()
}
