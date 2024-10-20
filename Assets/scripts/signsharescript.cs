using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signsharescript : MonoBehaviour
{
    public signscript sign;
        bool sharing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(sharing == true)
        {
            sign.signflip();
        }
        if(sharing == false)
        {
            sign.signunflip();
        }
    }

    public void share()
    {
        sharing = true;
    }
    public void dontshare()
    {
        sharing = false;
    }

}

