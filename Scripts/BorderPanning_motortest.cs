using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMotor_test))]
public class BorderPanning_motortest : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float borderWeight = 20f;
    private CameraMotor_test _motor;
    private bool isPanning = false;

    private void Awake()
    {
        _motor = GetComponent<CameraMotor_test>();
    }

    private void Update()
    {
        Vector3 target = transform.position;

        if (Input.mousePosition.y >= Screen.height - borderWeight && Input.mousePosition.y <= Screen.height)
        {
            target = target + (Vector3.forward * speed);
            isPanning = true;
        }
        else if (Input.mousePosition.y <= borderWeight && Input.mousePosition.y >= 0)
        {
            target = target + (Vector3.back * speed);
            isPanning = true;
        }          
        if (Input.mousePosition.x >= Screen.width - borderWeight && Input.mousePosition.x <= Screen.width)
        {
            target = target + (Vector3.right * speed);
            isPanning = true;
        }
        else if (Input.mousePosition.x <= borderWeight && Input.mousePosition.x >= 0)
        {
            target = target + (Vector3.left * speed);
            isPanning = true;
        }
        if(target == transform.position)
        {
            isPanning = false;
        }
        if (isPanning)
        {
            if (_motor != null)
            {
                _motor.SetFreePosition(target);
            }
        }
    }
}
