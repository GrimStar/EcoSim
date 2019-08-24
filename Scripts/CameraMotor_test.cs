using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor_test : MonoBehaviour
{
    [SerializeField]
    private bool followOnSelect = false;

    private Interactable selectedInteractable;
    private Transform selectedTransform;


    private Vector3 prevFreePos;    
    private Vector3 targetPosition;

    [HideInInspector]
    public float curFollowOffset;
    [HideInInspector]
    public bool isFollowingInteractable = false;
    private Vector3 targetRef;
    [SerializeField]
    private float smoothTime = 1f;

    void Awake()
    {
        curFollowOffset = transform.position.y;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isFollowingInteractable)
        {
            FollowInteractable();
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (isFollowingInteractable)
            {
                ReturnToFreePosition();
            }
            else
            {
                ReturnToFollowPosition();
            }            
        }       
        Move();
    }
    void ReturnToFreePosition()
    {
        if (selectedTransform != null)
        {
            isFollowingInteractable = false;
            Vector3 freePosition = new Vector3(prevFreePos.x, prevFreePos.y, prevFreePos.z);
            targetPosition = freePosition;
        }
        
    }
    void ReturnToFollowPosition()
    {
        if (selectedTransform != null)
        {
            StartFollowingTarget();
        }
    }
    public void Zoom(float _currentZoomY, float _currentZoomZ, float _followOffset)
    {
        curFollowOffset = _followOffset;
        if (!isFollowingInteractable)
        {
            Vector3 _zoomPos = new Vector3(transform.position.x, _currentZoomY, _currentZoomZ);
            SetFreePosition(_zoomPos);
        }
    }
    public void FollowZoom(float _offset)
    {
        curFollowOffset = _offset;
    }
    public void SetFollowTarget(Interactable _interactable)
    {
        selectedInteractable = _interactable;
        if (followOnSelect)
        {
            StartFollowingTarget();
        }
    }
    public void SetFollowTarget(Transform _interactable)
    {
        selectedTransform = _interactable;
        if (followOnSelect)
        {
            StartFollowingTarget();
        }
    }
    public void SetFreePosition(Vector3 _worldPos)
    {
        isFollowingInteractable = false;
        targetPosition = _worldPos;
    }
   
    void StartFollowingTarget()
    {
        prevFreePos = transform.position;
        curFollowOffset = transform.position.y - selectedTransform.position.y;
        isFollowingInteractable = true;
    }
    void FollowInteractable()
    {
        targetPosition = CalculateFollowPosition(selectedTransform);
    }
    void Move()
    {       
        Vector3 _nextPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref targetRef, smoothTime);
        transform.position = _nextPosition;
    }
    Vector3 CalculateFollowPosition(Transform _target)
    {
        float _curFollowOffset = curFollowOffset - _target.position.y;
        Vector3 followPosition = new Vector3(_target.position.x, _target.position.y + curFollowOffset, _target.position.z + -curFollowOffset);
        return followPosition;
    }
}
