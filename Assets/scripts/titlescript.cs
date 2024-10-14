using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlescript : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    int rng = Random.Range(1, 3);
    // Start is called before the first frame update
    void Start()
    {
        if(rng == 1)
        {
            anim.SetBool("1", true);
            anim.SetBool("2", false);
            anim.SetBool("3", false);

        }
        if (rng == 2)
        {
            anim.SetBool("2", true);
            anim.SetBool("1", false);
            anim.SetBool("3", false);

        }
        if (rng == 3)
        {
            anim.SetBool("3", true);
            anim.SetBool("2", false);
            anim.SetBool("1", false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -10);

        if (Input.GetKey(KeyCode.Enter))
        {
            Destroy(gameObject);
        }
    }
}
