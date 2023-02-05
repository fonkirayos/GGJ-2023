using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : MonoBehaviour
{

    public bool bmoving = false;
    public bool bidle = true;
    public bool bjumping = false;
    public bool bFloor = false;
    public bool jumpPending = false;
    public bool pauseCooldown = false;
    public bool bHasWatercan = false;

    public int mushroomCollected = 0;
    internal CStateMachine<CPlayerController> stateMachine;
    internal PIdleState idleState;
    internal PMovingState movingState;
    internal PJumpingState jumpingState;

    internal Rigidbody2D rigidbody;
    internal SpriteRenderer spriteRenderer;

    internal float killY = -15f;

    internal float moveSpeed = 10f;
    internal float moveJumpSpeed = 7f;
    internal float maxVelocity = 9f;
    internal float maxVelocityJump = 9f;
    internal float horizontalAxis = 0f;
    internal float jumpImpulse = 7f;


    internal int maxjumps = 3;
    internal int jumpsLeft;
    CPlayerStats playerStats;

    public AudioSource jumpSound;
    public AudioSource hurtSound;
    public AudioSource checkpointSound;
    public AudioSource pickUpSound;
    public AudioSource WatercanSound;
    public CUIPause pause;
    int framesStopped = 0;
    int frameCooldown = 140;
    internal Vector3 respawnPos;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new CStateMachine<CPlayerController>();
        idleState = new PIdleState(stateMachine);
        movingState = new PMovingState(stateMachine);
        jumpingState = new PJumpingState(stateMachine);
        stateMachine.setCurrentState(idleState, this);


        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        jumpsLeft = maxjumps;


    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.isPause)
        {
            if(framesStopped < frameCooldown)
            {
                framesStopped++;
                
            }
            else
            {
                stateMachine.update(this);
                pauseCooldown = false;
                
            }
            
            if (transform.position.y <= killY)
                respawn();
            
        }
        else
        {
            pauseCooldown = true;
            framesStopped = 0;
        }
        
        
    }

    private void FixedUpdate()
    {
       
        stateMachine.fixedUpdate(this);
        
    }

    public void Move(ref float contextSpeed, ref float maxContextSpeed)
    {
        bmoving = true;

         rigidbody.velocity += new Vector2(contextSpeed * Time.fixedDeltaTime * horizontalAxis, 0f);

        if (horizontalAxis > 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;



        if (Mathf.Abs(rigidbody.velocity.x) > maxContextSpeed)
        {
            if (rigidbody.velocity.x > 0)
                rigidbody.velocity = new Vector2(maxContextSpeed, rigidbody.velocity.y);
            else
                rigidbody.velocity = new Vector2(-maxContextSpeed, rigidbody.velocity.y);
        }
    }

    public void Jump()
    {
        jumpsLeft--;
        Debug.Log(jumpsLeft);

        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
        rigidbody.AddForce(new Vector2(0f, 350f));
        playJumpSound();
    }

    public void resetJumps()
    {
        jumpsLeft = maxjumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Floor")
        //{
        //    resetJumps();
        //    // stateMachine.setCurrentState(idleState, this);
        //    bFloor = true;
        //}
        
    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Floor")
        //{         
        //    bFloor = true;
        //}

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Floor")
        //{
        //    bFloor = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "Respawn")
        //{
        //    respawnPos = collision.transform.position;
        //    playCheckpointSound();
        //}
        if (collision.gameObject.tag == "Trap")
        {
            playHurtSound();
            respawn();
        } 

        if (collision.gameObject.tag == "Floor")
        {
            resetJumps();
            bFloor = true;
            
        }
        if (collision.gameObject.tag == "Mushroom")
        {
            mushroomCollected++;
            playPickupSound();
            Destroy(collision.gameObject);

        }
        if(collision.gameObject.tag == "Watercan")
        {
            bHasWatercan = true;
            playWatercanSound();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            
            bFloor = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {

            bFloor = false;

        }

    }
    public void respawn()
    {
        rigidbody.velocity = Vector2.zero;
        this.transform.position = respawnPos;
    }

    public void checkFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down, 5f);

        if(hit.collider.tag == "Floor")
        {
            Debug.Log("hit floor");
            resetJumps();
            bFloor = true;
            stateMachine.setCurrentState(idleState, this);
        }
        else
        {
            bFloor = false;
            Debug.Log("hit none");
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 ray = transform.position;
        ray.y -= 0.2f;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, ray);
    }

    public void playJumpSound()
    {
        jumpSound.Play();
    }

    public void playHurtSound()
    {
        hurtSound.Play();
    }

    public void playCheckpointSound()
    {
        checkpointSound.Play();
    }

    public void playPickupSound()
    {
        pickUpSound.Play();
    }

    public void playWatercanSound()
    {
        WatercanSound.Play();
    }
}
