using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviewscript : MonoBehaviour
{
    public Transform player;
    int control = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            control = -control;
        }
        if (control == 1)
        {
            transform.position = player.transform.position + new Vector3(0, -3, -11);
        }
        if (control == -1)
        {
            transform.position = player.transform.position + new Vector3(0, 5, 24);
        }
    }
}
