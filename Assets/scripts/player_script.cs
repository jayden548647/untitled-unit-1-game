using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public Rigidbody2D player;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public LayerMask enemyLayerMask;
    public Animator anim;
    public SpriteRenderer sr;
    bool isGrounded = false;
    bool onLeftWall = false;
    bool onRightWall = false;
    bool wallJumping = false;

    
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
        groundLayerMask = LayerMask.GetMask("Ground");
        wallLayerMask = LayerMask.GetMask("Wall");
        enemyLayerMask = LayerMask.GetMask("Enemy");
        player = GetComponent<Rigidbody2D>();
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

        if (wallJumping == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    player.velocity = new Vector2(-12f, player.velocity.y);
                    anim.SetBool("isrunning", true);
                    sr.flipX = true;
                }
                else
                {
                    player.velocity = new Vector2(-8f, player.velocity.y);
                    anim.SetBool("iswalking", true);
                    sr.flipX = true;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    player.velocity = new Vector2(12f, player.velocity.y);
                    anim.SetBool("isrunning", true);
                    sr.flipX = false;
                }
                else
                {
                    player.velocity = new Vector2(8f, player.velocity.y);
                    anim.SetBool("iswalking", true);
                    sr.flipX = false;
                }
            }
            else
            {
                if(player.velocity.x > 0 || player.velocity.x < 0)
                {
                    anim.SetBool("isSliding", true);
                }
            }
        }
        if (isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
            if(Input.GetKey(KeyCode.S))
            {
                player.velocity = new Vector2(player.velocity.x, -7f);
            }
        }

        if(onLeftWall == true)
        {
            player.gravityScale = 0.25f;
            sr.flipX = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                player.velocity = new Vector2(5f, 9f);
                wallJumping = true;
            }
        }
        if (onRightWall == true)
        {
            player.gravityScale = 0.25f;
            sr.flipX = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
          
                player.velocity = new Vector2(-5f, 9f);
                wallJumping = true;
            }
        }
        DoRayCollisionCheck();
        leftwallCollisionCheck();
        rightwallCollisionCheck();
        ExtendedRayCollisionCheck(-0.5f, 0);
        ExtendedRayCollisionCheck(0.5f, 0);
        leftenemyCollisionCheck();
        rightenemyCollisionCheck();
        downenemyCollisionCheck();

    }
    public void DoRayCollisionCheck()
    {
        float rayLength = 1f; // length of raycast


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
    public void leftwallCollisionCheck()
    {
        float rayLength = 1f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength, wallLayerMask);

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
        Debug.DrawRay(transform.position, Vector2.left * rayLength, hitColor);

    }
    public void rightwallCollisionCheck()
    {
        float rayLength = 1f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.right, rayLength, wallLayerMask);

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
        Debug.DrawRay(transform.position, Vector2.right * rayLength, hitColor);

    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f; // length of raycast
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
    public void leftenemyCollisionCheck()
    {
        float rayLength = 1f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength, enemyLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            player.velocity = new Vector2(5f, 9f);
        }
  
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, Vector2.left * rayLength, hitColor);

    }
    public void rightenemyCollisionCheck()
    {
        float rayLength = 1f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.right, rayLength, enemyLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            player.velocity = new Vector2(-5f, 9f);
        }

        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position, Vector2.right * rayLength, hitColor);

    }
    public void downenemyCollisionCheck()
    {
        float rayLength = 1f; // length of raycast


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
        Debug.DrawRay(transform.position, Vector2.right * rayLength, hitColor);

    }
    public bool ExtendedEnemyCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f; // length of raycast
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


}
