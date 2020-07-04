using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource coinAudioSource;
    public float walkspeed = 0.001f;
    public float jumpSpeed = 7f;
    public bool moveright;
    public bool moveup;
    public float hmove;

    // access the HUD
    public HudManager hud;

    //to keep our rigid body
    Rigidbody rb;

    //to keep the collider object
    Collider coll;

    //flag to keep track of whether a jump started
    

    // Use this for initialization
    void Start()
    {
        //get the rigid body component for later use
        rb = GetComponent<Rigidbody>();

        //get the player collider
        coll = GetComponent<Collider>();

        //refresh the HUD
        hud.Refresh();
    }

   
      
    public void right()
    {
        
        moveright = true;
        moveup = false;
        

    }
    public void up()
    {
       
        moveright = false;
        moveup = true;
       
    }
   
    public void Update()
    {
        playermovement();
      
    }
    public void playermovement()
    {
        
        if (moveright)
        {
            hmove = walkspeed;
            rb.velocity = new Vector3(hmove, 0f, 0f);
            
            
        }
        else if (moveup)
        {
            hmove = walkspeed;
            rb.velocity = new Vector3(0f, hmove, 0f);
        }
        
    }
    public void FixedUpdate()
    {
        moveup = false;
       
        
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if we ran into a coin
        if (collider.gameObject.tag == "Coin")
        {
            print("Grabbing coin..");

            // Increase score
            GameManager.instance.IncreaseScore(1);

            //refresh the HUD
            hud.Refresh();

            // Play coin collection sound
            coinAudioSource.Play();

            // Destroy coin
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            // Game over
            print("game over");

            SceneManager.LoadScene("Game Over");
        }
        else if (collider.gameObject.tag == "Goal")
        {
            print("goal reached");

            // Increase level
            GameManager.instance.IncreaseLevel();
        }

    }
}
