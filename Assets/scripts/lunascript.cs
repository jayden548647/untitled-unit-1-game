using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lunascript : MonoBehaviour
{
    public lunascript luna;
    
    Vector2 startplace;
    public bool setting = false;
    // Start is called before the first frame update
    void Start()
    {
        startplace = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (setting == true)
        {
            transform.position = startplace - new Vector2(0, 3f);
            
        }

        if(setting == false)
        {
            transform.position = startplace;
            
        }
    }

    public void sunrise()
    {
        setting = true;
        
    }
    public void moonrise()
    {
        setting = false;
        
    }

    public void split()
    {
        setting = true;
    }
    public void unsplit()
    {
        setting = false;
    }
}
