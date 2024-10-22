using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonscript : MonoBehaviour
{
    public lunascript lunascript;
    public moonscript moon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void split()
    {
        lunascript.sunrise();
    }
    public void unsplit()
    {
        lunascript.moonrise();
    }

}
