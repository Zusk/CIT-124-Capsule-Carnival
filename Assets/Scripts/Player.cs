using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is just a 'little' bigger then the one that was given to the class. Wanted to
//really refine and improve the movement of the player character!
public class Player : MonoBehaviour
{
    //Player speed!
    private readonly float speed = 5;
    //Player jump height!
    private readonly float jump_height = 4;

    //A bool to check if the player is dead
    public bool isAlive = true;

    //The layermask we use to check for when it comes to collison detection
    public LayerMask groundMask;
    public GameObject deathParticle;

    //variables for holding keyboard pressed
    private bool spaceKeyWasPressed;
    //variable for horizontalInput
    private float horizontalInput;

    //Variable for holding score
    public int score = 0;
    //variable for our components
    public GameObject canvas;
    public TextMeshProUGUI ui_text;
    public Rigidbody rigidbodyComponent;
    private ParticleSystem particleComponent;
    private Animator animatorComponent;
    private AudioSource audioComponent;
    public GameObject playerModel;
    private GameObject playerCam;
    public GameObject gameOverText;
    public AudioClip jumpClip;
    public AudioClip collectClip;
    public AudioClip deathClip;

    //A constant to define wall check distance.
    private readonly float horizontalWallCheckDistBase = 0.2f;
    //Used for the ground detection script!
    private readonly float groundCheckDistance = 0.2f;

    //When a rigibody unity object is 'standing' on a slope, the engine considers it
    //to be having lots of vertical velocity despite the object not actually moving.
    //As we need to get a accurate vertical velocity for our jumping animation, we use this to
    //print the absolute value of the vertical velocity, but return 0 if we are grounded.
    private float y_Velocity()
    {
        if (Is_Grounded())
        {
            return 0f;
        }
        return Mathf.Abs(rigidbodyComponent.velocity.y);
    }
    private bool Is_Grounded()
    {
        //Mostly as-is from the example code. Usually it would have been easier to include a character controller component and use
        //unity's built-in IsGrounded check, but this is sufficient with how we are using it!
        return Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
    }
    private bool Is_Touching_Wall()
    {
        //This is slightly more complicated, but I will explain it here!
        //With this bool, we need to predict if your current horizontal momentum, if carried out, would push us into a wall.
        //How we do this is fire a short ray from where we are towards our current move direction ( Our current position modified by our horizontalInput )
        //If we hit a wall with this short ray, we know we are walking into a wall!

        return Physics.Raycast(
            //First line is its origin. It starts at your current transform position, but does it slightly above where your feet are - as to avoid the floor.
            new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z),
            //Second line is our ray's intended destination. horizontalInput looks as if it lerps between current and intended destination, so we multiply it a ton to get
            //Its ultimate direction.
            new Vector3(transform.position.x + (horizontalInput * 500), transform.position.y + 0.1f, transform.position.z),
            //Third line is _, as we don't need to access the hit info here.
            out _,
            //Fourth line is the short distance we are checking by multiplied by the absolute value of our horizontalInput - scaling from 0 to 1 usually.
            horizontalWallCheckDistBase * Mathf.Abs(horizontalInput), groundMask);
    }
    private Vector3 Jump_Formula()
    {
        //This is very simple, just add the jump height to y with addforce!
        return new Vector3(0, jump_height, 0);
    }
    private Vector3 Speed_Formula()
    {
        //Speed formula!
        //X is a value based on your speed; a constant that defines how fast you move generally times your horizontal input.
        //Y is just what your vertical velocity is already, for vertical velocity we use add force.
        //Z is 0 always.
        return new Vector3(speed * horizontalInput, rigidbodyComponent.velocity.y, 0);
    }
    //Just plays a certain clip!
    public void PlayClip(AudioClip clip)
    {
        audioComponent.clip = clip;
        audioComponent.Play();
    }

    void Start()
    {
        //Reduces runtime calls by building references to our components here at runtime, rather then in our loops.
        canvas = GameObject.Find("Canvas");
        ui_text = GameObject.Find("PointsDisplay").transform.GetComponent<TextMeshProUGUI>();
        particleComponent = GetComponent<ParticleSystem>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        audioComponent = GetComponent<AudioSource>();
        playerCam = transform.GetChild(0).gameObject;
        playerModel = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        //Gets player input. We will use this later!
        Player_Input();
    }

    //
    private void FixedUpdate()
    {
        //Moves the player based on input!
        Player_Move();
        //Sets the animators state based on your current  vertical velocity.
        //It converts your velocity to an absolute value because we want how fast you are moving, either up or down.
        animatorComponent.SetFloat("vertical_velocity", y_Velocity());

        //Handles player death here!
        if(isAlive == false)
        {
            PlayerDeath();
        }
    }

    //Called when 'isAlive' is false, always from touching a evilcube
    private void PlayerDeath()
    {
        //Gets this instance of the player character
        Player playerComp = GetComponent<Player>();
        //Play the death noise!
        PlayClip(deathClip);
        //hides the player model
        playerModel.SetActive(false);
        //shows the game over text
        gameOverText.SetActive(true);
        //Creates the particle
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        //Does some handling for the rigibody component so that it stops moving
        rigidbodyComponent.useGravity = false;
        rigidbodyComponent.velocity = new Vector3(0, 0, 0);
        //Disables the player component
        playerComp.enabled = false;
        //starts a coroutine to reset the level in a certain amount of time ( After the game over text is done )
        StartCoroutine(LevelLoad());
    }

    //Yield can only be done from IEnumerator's, so lets use this!
    IEnumerator LevelLoad()
    {
        //Wait 2 seconds...
        yield return new WaitForSeconds(2);
        //Then reload the scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void Player_Input()
    {
        //Function to check if space key is pressed down, used to create jumping for player
        //Edited to be "GetButtonDown", GetKeyDown is mostly depreciated for this sort of call. 
        if (Input.GetButtonDown("Jump") == true)//get input from keyboard, looking for space key pressed, if it is do the following
        {
            spaceKeyWasPressed = true;
        }

        //collect horizontal input
        // by default keys a, d, left and right on keyboard
        //by default is 0
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void Player_Move()
    {
        //Figure out if we are jumping first!
        Jump();
        //Using translate to move your character leads to jittering movement if you don't include an additional layer of wall detection
        //on top of your standard physics check. This is to help with that!
        if (Is_Touching_Wall())
        {
            horizontalInput = 0;
        }
        //Old Approach:
        //rigidbodyComponent.velocity = Speed_Formula();

        //New Approach!
        //Move the player through translation, rather then addforce.
        //This works better with faster speeds, as our character controller uses over the
        //default example provided.
        transform.Translate(Speed_Formula() * Time.deltaTime);
    }

    void Jump()
    {
        if (spaceKeyWasPressed == true && Is_Grounded())
        {
            //Play the jump noise!
            PlayClip(jumpClip);
            //Play the particle component! The particle component is a non-looping component which has a emission.
            particleComponent.Play();
            //if space was pressed, get Rigidbody component from our player
            //add a force to the up using velocity change force mode
            rigidbodyComponent.AddForce(Jump_Formula(), ForceMode.VelocityChange);
            spaceKeyWasPressed = false;
        }
    }
}//end of class Player