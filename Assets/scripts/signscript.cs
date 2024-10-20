using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signscript : MonoBehaviour
{
    public SpriteRenderer sr;
    public signsharescript share;
    // Start is called before the first frame update
    void Start()
    {
        sr.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(sr.flipX == true)
        {
            share.share();
        }
        if(sr.flipX == false)
        {
            share.dontshare();
        }
    }

    public void signflip()
    {
        sr.flipX = true;
    }
    public void signunflip()
    {
        sr.flipX = false;
    }
}
