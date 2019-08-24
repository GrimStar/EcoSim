using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCNavigation : MonoBehaviour
{

    public Transform followTarget;
    public Vector3 migrationDestination;
    Vector3 wanderingDestination;
    Vector3 habitatDestination;
    int curDestination = 0;
    Transform curMigrationWaypoint;
    CreatureStatus _creatureStatus;
    CreatureMemory _memory;
    [SerializeField]
    float habitatRadius = 50f;
    [SerializeField]
    float wanderingRadius = 20f;
    //CreatureAttack _attack;
    public NavMeshAgent agent;
    [SerializeField]
    LayerMask mask;
    //public bool isHunting = false;
    // Start is called before the first frame update
    void Start()
    {
        //_attack = GetComponent<CreatureAttack>();
        _memory = GetComponent<CreatureMemory>();

        _creatureStatus = GetComponent<CreatureStatus>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        DestinationControl();
        _creatureStatus.curSpeed = agent.speed;
        if (_creatureStatus.dead)
        { 
            if (agent.speed != 0)
            {
                agent.speed = 0;
            }
        }
    }
    public void SetFollowTarget(Transform _object)
    {
        followTarget = _object;
    }
    public void SetDestination(Transform _destination)
    {
        migrationDestination = _destination.position;
    }
    private void DestinationControl()
    {
        
        if (followTarget != null)
        {
            agent.SetDestination(followTarget.position);
        }
        else if(_memory.isMigrating)
        {
            agent.SetDestination(migrationDestination);
            //CheckMigrationDistance();
        }
        else if(_memory.isWandering)
        {
            if (wanderingDestination != Vector3.zero)
            {
                agent.SetDestination(wanderingDestination);
            }
            else
            {
                GetWanderingDestination();
            }
          
            CheckDistance();
        }
    }
    void GetWanderingDestination()
    {
        Debug.Log("GetWanderingPos");
        FindDestination _findDestination = new FindDestination();
        //Vector3 possibleDestination = FindDestinationWithinRadius(migrationDestination);
        Vector3 possibleDestination = _findDestination.FindDestinationWithinRadius(migrationDestination, wanderingRadius);
        if (possibleDestination != Vector3.zero)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(possibleDestination, path);
            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                wanderingDestination = possibleDestination;
            }
            
        }
    }
    void CheckDistance()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            GetWanderingDestination();



        }
    }
    Vector3 FindDestinationWithinRadius(Vector3 _centerPoint)
    {
        float radius = wanderingRadius;
       
        Vector3 _destination = _centerPoint + Random.insideUnitSphere * radius;
       
        NavMeshHit hit;
        if (NavMesh.SamplePosition(_destination, out hit, radius, 1))
        {
            Debug.Log("FoundWanderingDestination" + hit.position.ToString());
            return hit.position;
        }
        else
        {
            return Vector3.zero;
        }

    }
    Vector3 OldFindDestinationWithinRadius(Vector3 _centerPoint)
    {
        float radius = wanderingRadius;

        Vector3 _destination = _centerPoint + Random.insideUnitSphere * radius;
        RaycastHit rayHit;
        if (Physics.Raycast(_destination, Vector3.down, out rayHit, 9999, mask))
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_destination, out hit, radius, 1))
            {
                return hit.position;
            }
            else
            {
                return Vector3.zero;
            }
        }
        else
        {
            return Vector3.zero;
        }
    }
  
}
