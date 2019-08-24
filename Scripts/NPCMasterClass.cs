using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMasterClass : MonoBehaviour {

    protected NavMeshAgent agent;
    
    protected int destinationArrayLength;
    protected int currentDestination = 0;

    [SerializeField]
    protected float speed = 1;
    protected float actualSpeed = 0;
    protected float speedMultiplier = 0;
    [SerializeField]
    protected float stoppingDistance = 1;
    [SerializeField]
    protected float waitTime = 0;
    protected float velocityZ = 0;
    protected float velocityZTarget = 0;
    protected float curVelocityZ;
    [SerializeField]
    protected float smoothTime = 0.1f;

    protected bool idle = false;
    protected bool walk = true;
    protected bool run = false;
    protected bool waiting = false;


    protected int CurrentDestination
    {
        get { return currentDestination; }
        set
        {
            currentDestination = value;
            SetDestination();
        }
    }
    protected virtual void SetDestination()
    {
        
    }
    protected virtual void CheckDestinationDistance(Vector3 _position, Vector3 _destination)
    {
        float distance = Vector3.Distance(_position, _destination);
        if (distance <= stoppingDistance)
        {
            ReachedDestination();
        }
    }
    /// <summary>
    /// Used variables: actualSpeed, speed, speedMultiplier, agent.
    /// Purpose: Sets the speed based on the speedMultiplier which may change in Sub-Class.
    /// </summary>
    protected virtual void SetSpeed()
    {
        actualSpeed = speed * speedMultiplier;
        agent.speed = actualSpeed;
    }
    /// <summary>
    /// Used variables: waiting, waitTime.
    /// Purpose: Triggers WaitAtDestination().
    /// </summary>
    protected virtual void ReachedDestination()
    {
        if (!waiting)
        {
            waiting = true;
            StartCoroutine(WaitAtDestination());
        }
    }
    /// <summary>
    /// Used variables: _length, _currentDestination, nextDestination.
    /// Purpose: Recieves the length of the array or list of destinations and the current,
    /// destination and checks if it needs to reset to zero or continue.
    /// </summary>
    protected int CycleDestination(int _length, int _currentDestination)
    {
        int nextDestination = _currentDestination + 1;
        if(nextDestination >= _length)
        {
            return 0;
        }
        else
        {
            return nextDestination;            
        }        
    }
    
    /// <summary>
    /// Used variables: _player, _npc, playerDistance.
    /// Purpose: Checks and returns the distance of the player.
    /// </summary>
    protected virtual float GetPlayerDistance(Vector3 _player, Vector3 _npc)
    {
        float playerDistance = Vector3.Distance(_player, _npc);
        return playerDistance;
    }
    /// <summary>
    /// Used variables: target, _npc.
    /// Purpose: Checks if NPC can see a certain gameobject in the scene with simple raycast.
    /// </summary>
    protected virtual bool CanSee(GameObject target, Vector3 _npc)
    {
        RaycastHit hit;
        if(Physics.Raycast(_npc, target.transform.position - _npc, out hit, 100f))
        {
            
            if(hit.transform.gameObject == target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
  
    /// <summary>
    /// Used variables: waitTime, CurrentDestination, destinationArrayLength, waiting.
    /// Purpose: waits for amount of seconds (waitTime) before calling CycleDestination().
    /// </summary>
    IEnumerator WaitAtDestination()
    {
        yield return new WaitForSeconds(waitTime);
        CurrentDestination = CycleDestination(destinationArrayLength, CurrentDestination);       
        waiting = false;
    }
}
