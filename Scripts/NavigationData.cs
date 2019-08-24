using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationData : MonoBehaviour
{
    public static NavigationData instance;
    public Transform[] migrationWaypoints;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    
}
