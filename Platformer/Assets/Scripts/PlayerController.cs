using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    Vector3 cameraInitialPosition;
    float shakeMagnetude = 0.05f, shakeTime = 0.5f;
    Camera mainCamera; 
    //walking speed
    public float walkSpeed = 5;

    //rotation speed
    public float rotationSpeed = 100;

    //sinking speed
    public float sinkingSpeed = 0.3f;

    //jumping speed
    public float jumpForce = 6;

    //coin playing sound
    public AudioSource coinSound;

    //jump sound
    public AudioSource jumpSound;
    public AudioSource jumpLandingSound;

    //player movesound
    public AudioSource playerWalkSound;

    //death sounds
    public AudioSource deathByLava;
    public AudioSource deathBySkull;

    // Rigidbody component
    Rigidbody rb;

    //Collider component
    Collider col;

    //to keep track of key pressing
    bool pressedJump = false;

    //size of the player
    Vector3 size;

    //is player allowed to move
    bool playerAllowedToMove = true;

    //lava check
    bool playerFellInLava = false;

    //skull check
    bool playerSkullImpact = false;

    //check whats player walking on
    string playerWalkingOnTag;

    //check if in the air or not
    bool wasGrounded = true;
    bool didPressedJump;


    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        // Grab our component
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        // get player size
        size = col.bounds.size;

	}
	
	// Update is called once per frame
	void FixedUpdate() {
        LandingAfterJumpHandler();
        FallHandler();
        WalkHandler();
        JumpHandler();
        LavaHandler();
        SkullImpactHandler();
        //an accessory variable for the LandingAfterJumpHandler
        wasGrounded = CheckGrounded();
	}

    //shaking the screen when landing after a jump
    void LandingAfterJumpHandler() {
        if (pressedJump)
        {
            didPressedJump = true;
        }
        bool isgrounded = CheckGrounded();
        if (isgrounded && !wasGrounded && didPressedJump)
        {
            if (playerWalkingOnTag == "RockyGround" ||
            playerWalkingOnTag == "BrickGround" ||
            playerWalkingOnTag == "TimberGround")
            {
                if (!jumpLandingSound.isPlaying)
                {
                    jumpLandingSound.Play();
                };
                ShakeIt(0.01f, 0.1f);
                didPressedJump = false;
            }
        }
    }

    //death when character falls out of the map
    void FallHandler()
    {
        if 
        (gameObject.transform.position.y < -30)
        {
            GameManager.instance.GameOver("You fell into the abyss.");
        }
        else if(gameObject.transform.position.y < -5)
        {
            if (!deathByLava.isPlaying) { deathByLava.Play(); }
        }
    }

    //takes care of the walking logic
    void WalkHandler()
    {
        if (playerAllowedToMove)
        {
   
            // Input on x (Horizontal)
            float hAxis = Input.GetAxis("Horizontal");
            // Input on z (Vertical)
            float vAxis = Input.GetAxis("Vertical");

            // Player is grounded
            bool isGrounded = CheckGrounded();

            // It's two if statements so the player can move to both directions, seemingly simultaniusly
            if (hAxis != 0)
            {
                transform.Rotate(0, hAxis * rotationSpeed * Time.deltaTime, 0);
                // Play moving sound
                if (isGrounded)
                {
                    if (!playerWalkSound.isPlaying) { playerWalkSound.Play(); }
                }
            }
            if (vAxis != 0)
            {
                // Calculate the new position
                Vector3 newPos = transform.position + transform.forward * Time.deltaTime * walkSpeed * vAxis;
                // Move
                rb.MovePosition(newPos);
                // Play moving sound
                if (isGrounded)
                {
                    if (!playerWalkSound.isPlaying) { playerWalkSound.Play(); }
                }
            }
            else
            {
                playerWalkSound.Stop();
            }
        }
    }

    // takes care of the jumping logic
    void JumpHandler()
    {
        // Input on the Jump axcis
        float jAxis = Input.GetAxis("Jump");

        //If the key has been pressed
        if (jAxis > 0)
        {
            print("Is jumping!!!!!!!!");
            playerWalkSound.Stop();
            bool isGrounded = CheckGrounded();
            //make sure we are not already jumping
            if (!pressedJump && isGrounded)
            {
                //ShakeIt();
                pressedJump = true;
                // jumping vector
                Vector3 jumpVector = new Vector3(0, jAxis * jumpForce, 0);

                //apply force
                rb.AddForce(jumpVector, ForceMode.VelocityChange);

                //play sound
                jumpSound.Play();
            } 
        }
        else
        {
            //set flag to false
            pressedJump = false;
        }
    }

    void LavaHandler()
    {
        if (playerFellInLava)
        {
            if (!deathByLava.isPlaying) { deathByLava.Play(); }
            rb.transform.Rotate(Vector3.up, 10f * Time.deltaTime);
            // shrink logic
            if (rb.transform.localScale.y > 0.5f)
            {
                transform.localScale += new Vector3(0 , -0.5F * Time.deltaTime, 0);
            }
            else
            {
                GameManager.instance.GameOver("The floor is lava!");
            }
        }
    }

    void SkullImpactHandler()
    {
        if (playerSkullImpact)
        {

            if (rb.transform.localScale.y > 0.1f)
            {
                if (!deathBySkull.isPlaying) { deathBySkull.Play(); }
                mainCamera.transform.localPosition += new Vector3(0, 2f * Time.deltaTime, 0);
                transform.localScale += new Vector3(0, -1F * Time.deltaTime, 0);
            }
            else
            {
                GameManager.instance.GameOver("A skull schooled you... auch!");
            }
        }
    }

    private bool CheckGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            playerWalkingOnTag = hit.transform.gameObject.tag;
            return true;
        }
        else
        {
            playerWalkingOnTag = "Nothing";
            return false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            playerAllowedToMove = false;
            playerFellInLava = true;
        }
        else if (other.CompareTag("Skull"))
        {
            playerAllowedToMove = false;
            playerSkullImpact = true;
            //GameManager.instance.GameOver("A skull schooled you... auch!");
        }
        else if (other.CompareTag("Coin"))
        {
            // Increase our score
            GameManager.instance.IncreaseScore(1);
            
            // Play sound
            coinSound.Play();
            
            //Destroy coin
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Goal"))
        {
            print("You made it!");
            //send player to the next level
            GameManager.instance.IncreaseLevel();
        }
    }

    public void ShakeIt(float shakeMagnetude, float shakeTime)
    {
        this.shakeMagnetude = shakeMagnetude;
        this.shakeTime = shakeTime;
        cameraInitialPosition = mainCamera.transform.localPosition;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = UnityEngine.Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = UnityEngine.Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.localPosition = cameraInitialPosition;
    }
}
