using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helperscript : MonoBehaviour
{
    int enemydir = -1;
    
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D enemy = gameObject.GetComponent<Rigidbody2D>();
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
            hitSomething = true;
            enemydir = -enemydir;
        }
        if (enemydir < 0)
        {
            sr.flipX = true;
            enemy.velocity = new Vector2(-3f, enemy.velocity.y);
        }
        if (enemydir > 0)
        {
            sr.flipX = false;
            enemy.velocity = new Vector2(3f, enemy.velocity.y);
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;


    }
    public bool leftWallCollisionCheck(float xoffs, float yoffs)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D enemy = gameObject.GetComponent<Rigidbody2D>();
        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;


        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.left, rayLength, wallLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
            enemydir = -enemydir;
        }
        if (enemydir < 0)
        {
            sr.flipX = true;
            enemy.velocity = new Vector2(-3f, enemy.velocity.y);
        }
        if (enemydir > 0)
        {
            sr.flipX = false;
            enemy.velocity = new Vector2(3f, enemy.velocity.y);
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.left * rayLength, hitColor);

        return hitSomething;


    }
    public bool rightWallCollisionCheck(float xoffs, float yoffs)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D enemy = gameObject.GetComponent<Rigidbody2D>();
        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;


        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.right, rayLength, wallLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
            enemydir = -enemydir;
        }
        if (enemydir < 0)
        {
            sr.flipX = true;
            enemy.velocity = new Vector2(-3f, enemy.velocity.y);
        }
        if (enemydir > 0)
        {
            sr.flipX = false;
            enemy.velocity = new Vector2(3f, enemy.velocity.y);
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.right * rayLength, hitColor);

        return hitSomething;


    }
    // Start is called before the first frame update
    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        wallLayerMask = LayerMask.GetMask("Wall");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
