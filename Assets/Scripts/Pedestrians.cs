using UnityEngine;
using UnityEngine.AI;

public class Pedestrians : MonoBehaviour
{

    public Transform[] walkPoints;
    public float walkWaitTime = 4f;
    private float waitTimer;

    private int currentWalkIndex;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(walkPoints.Length > 0)
        {
            currentWalkIndex = 0;
            SetNextDestination();
        }
    }


    void Update()
    {
        GoToDestination();
    }


    private void SetNextDestination()
    {
        //if (walkPoints.Length == 0) return;

        agent.SetDestination(walkPoints[currentWalkIndex].position);
        currentWalkIndex = Random.Range(0, walkPoints.Length); //(currentPatrolIndex + 1) % patrolPoints.Length;

        Debug.Log("WALKING TOWARD " + currentWalkIndex);
    }

    private void GoToDestination()
    {
        Debug.Log("WALKING TOWARD " + currentWalkIndex);

        if (agent.remainingDistance < 0.1f)
        {
            SetNextDestination();
            //isWaiting = true;
            //waitTimer = walkWaitTime;
        }
        else
        {
            return;
        }


        if (walkPoints.Length == 0) return;
    }

}
