using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CameraMotor_test))]
public class CameraDrag_motortest : MonoBehaviour
{

    [SerializeField]
    private float speed = 60;
    private CameraZoom_motortest zoomHandler;
    private CameraMotor_test _motor;

    private bool dragging;

    private void Awake()
    {
        zoomHandler = GetComponent<CameraZoom_motortest>();
        _motor = GetComponent<CameraMotor_test>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            dragging = true;
        }

        if (dragging)
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 currentPos = transform.position;
                float xAxis = Input.GetAxis("Mouse X");
                float zAxis = Input.GetAxis("Mouse Y");
                if (zoomHandler != null)
                {
                    currentPos.x -= xAxis * (speed * zoomHandler.distanceScale);
                    currentPos.z -= zAxis * (speed * zoomHandler.distanceScale);
                }
                if (zAxis != 0 || xAxis != 0)
                {
                    if (_motor != null)
                    {
                        _motor.SetFreePosition(currentPos);
                    }
                }
            }
            else
            {
                dragging = false;
            }
        }
    }
}
