using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player speed!
    private readonly float speed = 5;
    //Player jump height!
    private readonly float jump_height = 4;

    //Used for the ground detection script!
    private readonly float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    //variables for holding keyboard pressed
    private bool spaceKeyWasPressed;
    //variable for horizontalInput
    private float horizontalInput;
    //variable for holding getRigidBody component
    private Rigidbody rigidbodyComponent;
    private ParticleSystem particleComponent;
    private Animator animatorComponent;

    private bool Is_Grounded()
    {
        return Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
    }
    private bool Is_Touching_Wall()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3((horizontalInput * 500) + transform.position.x, transform.position.y, transform.position.z), Color.blue, 15f);
        RaycastHit hit;
        return Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), new Vector3((horizontalInput * 500) + transform.position.x, transform.position.y + 0.1f, transform.position.z), out hit, 0.2f * Mathf.Abs(horizontalInput), groundMask);
    }
    private Vector3 Jump_Formula()
    {
        return new Vector3(0, jump_height, 0);
    }
    private Vector3 Speed_Formula()
    {
        return new Vector3(horizontalInput * speed, rigidbodyComponent.velocity.y, 0);
    }

    void Start()
    {
        //Reduces runtime calls.
        particleComponent = GetComponent<ParticleSystem>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
    }

    void Update()
    {
        //Gets player input.
        Player_Input();
    }

    //
    private void FixedUpdate()
    {
        //Moves the player based on input.
        Player_Move();
        animatorComponent.SetFloat("vertical_velocity", Mathf.Abs(rigidbodyComponent.velocity.y));
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
        Jump();
        if (Is_Touching_Wall())
        {
            horizontalInput = 0;
        }
        else
        {
            Debug.Log("Nope!");
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
            particleComponent.Play();
            //if space was pressed, get Rigidbody component from our player
            //add a force to the up using velocity change force mode
            rigidbodyComponent.AddForce(Jump_Formula(), ForceMode.VelocityChange);
            spaceKeyWasPressed = false;
        }
    }
}//end of class Player