using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public helperscript helper;
    // Start is called before the first frame update
    void Start()
    {
        helper = gameObject.AddComponent<helperscript>();
    }

    // Update is called once per frame
    void Update()
    {
        helper.ExtendedRayCollisionCheck(-0.5f, 0);
        helper.ExtendedRayCollisionCheck(0.5f, 0);
    }
}
