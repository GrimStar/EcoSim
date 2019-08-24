using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
}
