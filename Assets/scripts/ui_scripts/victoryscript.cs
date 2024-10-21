using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryscript : MonoBehaviour
{
    bool won = false;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (won == false)
        {
            transform.position = player.transform.position + new Vector3(0, 5, 300);
        }
        if(won == true)
        {
            transform.position = player.transform.position + new Vector3(0, 5, -11);
        }
    }

    public void victory()
    {
        won = true;
    }
}
