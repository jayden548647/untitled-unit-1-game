using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlescript2 : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -9);

        if (Input.GetKey(KeyCode.Return))
        {
            Destroy(gameObject);
        }
    }
}
