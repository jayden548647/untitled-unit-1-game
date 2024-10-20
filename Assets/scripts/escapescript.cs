using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escapescript : MonoBehaviour
{
    public Transform player;
    bool escape = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(escape == false)
        {
            transform.position = player.transform.position + new Vector3(0, 12, 500);
        }
        if (escape == true)
        {
            transform.position = player.transform.position + new Vector3(0, 12, -9);
        }
    }

    public void Hurry()
    {
        escape = true;
    }

    public void NoRush()
    {
        escape = false;
    }

    
}
