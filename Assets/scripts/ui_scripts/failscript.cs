using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class failscript : MonoBehaviour
{
    bool lost = false;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lost == false)
        {
            transform.position = player.transform.position + new Vector3(0, 5, 300);
        }
    }

    public void failure()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -11);
        lost = true;
    }
}
