using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public Rigidbody2D player;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public LayerMask enemyLayerMask;
    public LayerMask doorLayerMask;
    public Animator anim;
    public SpriteRenderer sr;
    bool isGrounded = false;
    bool onLeftWall = false;
    bool onRightWall = false;
    bool wallJumping = false;
    bool isPlaying;
    bool escape = false;
    int collections = 0;
    public int wincon = 0;
    float acceleration = 0;
    float TopSpeed = 20;
    float speed = 8;
    public float timer = 200;
    Vector2 startpos;
   
    

    
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayerMask);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        groundLayerMask = LayerMask.GetMask("Ground");
        wallLayerMask = LayerMask.GetMask("Wall");
        enemyLayerMask = LayerMask.GetMask("Enemy");
        doorLayerMask = LayerMask.GetMask("Door");
        
        player = GetComponent<Rigidbody2D>();
        startpos = transform.position;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "collectable")
        {
            Destroy(other.gameObject);
            collections++;
            print(collections);

        }
        if (other.gameObject.tag == "checkpoint")
        {
            startpos = transform.position;
        }
        if (other.gameObject.tag == "winblock")
        {
            if (wincon == 2)
            {
                Destroy(other.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(collision.gameObject);
            wincon++; 
            startpos = transform.position;
            escape = true;
        }
    }
    

    public void OnTriggerStay2D(Collider2D end)
    {
        if (end.gameObject.tag == "win")
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                if (wincon == 1)
                {
                    transform.position = player.position + new Vector2(0, -190);
                    escape = false;
                }
            }
        }
        if (end.gameObject.tag == "win2")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (wincon == 2)
                {
                    print("Victory");
                    escape = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            acceleration = speed + Time.deltaTime;
            speed = speed + acceleration / 500;
            if (speed > TopSpeed)
            {
                speed = TopSpeed;
            }
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            speed = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        player.gravityScale = 1;
        anim.SetBool("isgrounded", false);
        anim.SetBool("iswalking", false);
        anim.SetBool("isrunning", false);
        anim.SetBool("isjumping", false);
        anim.SetBool("isfalling", false);
        anim.SetBool("isSliding", false);
        anim.SetBool("notmoving", false);

        if(escape == false)
        {
            timer = 200;
        }
        if (isPlaying == true)
        {
            if (player.velocity.x == 0)
            {
                anim.SetBool("notmoving", true);
            }
            if (wallJumping == false)
            {
                if (onLeftWall == false)
                {
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    {

                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            player.velocity = new Vector2(-speed, player.velocity.y);
                            anim.SetBool("isSliding", false);
                            anim.SetBool("isrunning", true);
                            sr.flipX = true;
                        }
                        else
                        {
                            player.velocity = new Vector2(-8f, player.velocity.y);
                            anim.SetBool("isSliding", false);
                            anim.SetBool("iswalking", true);
                            sr.flipX = true;
                        }
                    }
                }
                if (onRightWall == false)
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    {

                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            player.velocity = new Vector2(speed, player.velocity.y);
                            anim.SetBool("isSliding", false);
                            anim.SetBool("isrunning", true);
                            sr.flipX = false;
                        }
                        else
                        {
                            player.velocity = new Vector2(8f, player.velocity.y);
                            anim.SetBool("isSliding", false);
                            anim.SetBool("iswalking", true);
                            sr.flipX = false;
                        }
                    }
                }
                if (player.velocity.x > 0 || player.velocity.x < 0)
                {
                    if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    {
                        anim.SetBool("iswalking", false);
                        anim.SetBool("isrunning", false);
                        anim.SetBool("isSliding", true);
                    }
                }
            }
            if (isGrounded == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (player.velocity.x < -9 || player.velocity.x > 9)
                    {
                        player.velocity = new Vector2(player.velocity.x, 10f);
                    }
                    else
                    {
                        player.velocity = new Vector2(player.velocity.x, 7f);
                        anim.SetBool("isjumping", true);
                    }
                }
                anim.SetBool("isgrounded", true);
            }
            if (isGrounded == false)
            {
                if (player.velocity.y <= 0)
                {
                    anim.SetBool("isjumping", false);
                    anim.SetBool("isfalling", true);
                }
            }

            if (isGrounded == false)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    player.velocity = new Vector2(player.velocity.x, -7f);
                }
            }

            if (onLeftWall == true)
            {
                player.gravityScale = 0.25f;
                anim.SetBool("isjumping", false);
                sr.flipX = false;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    anim.SetBool("isfalling", false);
                    anim.SetBool("isjumping", true);
                    player.velocity = new Vector2(5f, 9f);
                    wallJumping = true;
                }
                if (isGrounded == true)
                {
                    anim.SetBool("notmoving", true);
                }
            }
            if (onRightWall == true)
            {
                player.gravityScale = 0.25f;
                anim.SetBool("isjumping", false);
                sr.flipX = true;

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    anim.SetBool("isfalling", false);
                    anim.SetBool("isjumping", true);
                    player.velocity = new Vector2(-5f, 9f);
                    wallJumping = true;
                }
                if (isGrounded == true)
                {
                    anim.SetBool("notmoving", true);
                }
            }
            DoRayCollisionCheck();
            leftwallCollisionCheck(0, 1f);
            rightwallCollisionCheck(0, 1f);
            ExtendedRayCollisionCheck(-0.75f, 0);
            ExtendedRayCollisionCheck(0.75f, 0);
            leftenemyCollisionCheck(0, 1f);
            rightenemyCollisionCheck(0, 1f);
            downenemyCollisionCheck();
            ExtendedEnemyCollisionCheck(0.5f, 0);
            ExtendedEnemyCollisionCheck(-0.5f, 0);
            doorCollisionCheck();


            if (player.position.y <= -50 && player.position.y >= -100)
            {
                Respawn();
            }
            if(player.position.y <= - 300)
            {
                Respawn();
            }
            if(escape == true)
            {
                timer = timer - Time.deltaTime;
            }
            if (timer < 0)
            {
                Application.Quit();
            }
        }
        if(Input.GetKey(KeyCode.Return))
        {
            isPlaying = true;
        }
    }
    public void DoRayCollisionCheck()
    {
        float rayLength = 0.25f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            isGrounded = true;
            wallJumping = false;
        }
        else
        {
            isGrounded = false;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, Vector2.down * rayLength, hitColor);

    }
    public void leftwallCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f; // length of raycast
        Vector3 offset = new Vector3(xoffs, yoffs, 0);


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.left, rayLength, wallLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            onLeftWall = true;
        }
        else
        {
            onLeftWall = false;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.left * rayLength, hitColor);

    }
    public void rightwallCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f; // length of raycast
        Vector3 offset = new Vector3(xoffs, yoffs, 0);


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.right, rayLength, wallLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            onRightWall = true;
        }
        else
        {
            onRightWall = false;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.right * rayLength, hitColor);

    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.25f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            isGrounded = true;
            wallJumping = false;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;

    }
    public void leftenemyCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f; // length of raycast
        Vector3 offset = new Vector3(xoffs, yoffs, 0);


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.left, rayLength, enemyLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            player.velocity = new Vector2(5f, 9f);
        }
  
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.left * rayLength, hitColor);

    }
    public void rightenemyCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f; // length of raycast
        Vector3 offset = new Vector3(xoffs, yoffs, 0);


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.right, rayLength, enemyLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            player.velocity = new Vector2(-5f, 9f);
        }

        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.right * rayLength, hitColor);

    }
    public void downenemyCollisionCheck()
    {
        float rayLength = 0.5f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, enemyLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            Destroy(hit.transform.gameObject);
            player.velocity = new Vector2(player.velocity.x, 2f);
        }

        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, Vector2.down * rayLength, hitColor);

    }
    public bool ExtendedEnemyCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, enemyLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            Destroy(hit.transform.gameObject);
            player.velocity = new Vector2(player.velocity.x, 2f);

        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;

    }
    public void doorCollisionCheck()
    {
        float rayLength = 2f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength, doorLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;

            if (wincon == 1)
            {
                Destroy(hit.transform.gameObject);
            }
        }

        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, Vector2.left * rayLength, hitColor);

    }
    void Respawn()
    {
        transform.position = startpos + new Vector2(0, 30);
    }
   

}
