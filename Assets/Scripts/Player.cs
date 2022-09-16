using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables for holding keyboard pressed
    private bool spaceKeyWasPressed;
    //variable for horizontalInput
    private float horizontalInput;
    //variable for holding getRigidBody component
    private Rigidbody rigidbodyComponent;


    // Start is called before the first frame update
    void Start()
    {
        //set the rigidbodyComponent to what is returned by getComponent<rigidbody>();
        rigidbodyComponent = GetComponent<Rigidbody>();
    }//end of voidStart

    // Update is called once per frame
    void Update()
    {
        //Function to check if space key is pressed down, used to create jumping for player
        if (Input.GetKeyDown(KeyCode.Space) == true)//get input from keyboard, looking for space key pressed, if it is do the following
        {
            spaceKeyWasPressed = true;

        }

        //collect horizontal input
        // by default keys a, d, left and right on keyboard
        //by default is 0
        horizontalInput = Input.GetAxis("Horizontal");
    }// end of void Update

    //Fixed Update is called 100 times per second by default, helps keep physics standard
    private void FixedUpdate()
    {
        //if spaceKeyWasPressed is set to true, make player jump
        if (spaceKeyWasPressed == true)
        {
            //if space was pressed, get Rigidbody component from our player
            //add a force to the up using velocity change force mode
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            spaceKeyWasPressed = false;
        }//end of if(spaceKeyWasPressed)

        //function to make horizontal movement, using a d left and right by default
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

    }//end of void FixedUpdate

}//end of class Player
