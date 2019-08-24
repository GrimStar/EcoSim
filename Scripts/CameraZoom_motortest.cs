using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CameraMotor_test))]
public class CameraZoom_motortest : MonoBehaviour
{
    [SerializeField]
    private float speed = 110;
    [SerializeField]
    private float minZoom = 3;
    [SerializeField]
    private float maxZoom = 150;
    private CameraMotor_test _motor;

    public float distanceScale
    {
        get
        {
            return Mathf.InverseLerp(0, maxZoom, transform.position.y);
        }
    }
    private void Awake()
    {
        _motor = GetComponent<CameraMotor_test>();
    }

    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.mouseScrollDelta != Vector2.zero)
            {

                float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * (speed * distanceScale);

                float curFollowOffset = _motor.curFollowOffset;
                curFollowOffset -= zoomAmount;
                curFollowOffset = Mathf.Clamp(curFollowOffset, minZoom, maxZoom);

                Vector3 currentPos = transform.position;
                float currentZoomY = currentPos.y;
                float currentZoomZ = currentPos.z;

                currentZoomY -= zoomAmount;
                currentZoomZ += zoomAmount;

                if (currentZoomY <= minZoom)
                {
                    float dif = currentZoomY - minZoom;
                    currentZoomY = minZoom;
                    currentZoomZ -= dif;

                }
                else if (currentZoomY >= maxZoom)
                {
                    float dif = currentZoomY - maxZoom;
                    currentZoomY = maxZoom;
                    currentZoomZ += dif;
                }

                if (_motor != null)
                {
                    _motor.Zoom(currentZoomY, currentZoomZ, curFollowOffset);
                }
            }
        }
    }
}
