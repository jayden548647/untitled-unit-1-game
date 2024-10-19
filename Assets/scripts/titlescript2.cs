using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlescript2 : MonoBehaviour
{
    public Transform player;
    bool starting = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (starting == true)
        {
            transform.position = player.transform.position + new Vector3(0, 5, -9);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.position = player.transform.position + new Vector3(0, 5, 300);
            starting = false;
        }
    }
    public void defeat()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -9);
    }
}
