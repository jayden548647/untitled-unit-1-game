using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_script : MonoBehaviour
{
    public Transform player;
    int scanlines = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            scanlines = -scanlines;
        }

        if (scanlines == 1)
        {
            transform.position = player.transform.position + new Vector3(0, 5, -12);
        }
        if(scanlines == -1)
        {
            transform.position = player.transform.position + new Vector3(0, 5, 15);
        }
        
    }
}
