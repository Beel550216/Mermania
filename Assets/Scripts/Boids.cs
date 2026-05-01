using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class Boids : MonoBehaviour
{

    [SerializeField] List<GameObject> fishes = new List<GameObject>();
    //[SerializeField] List<Vector3> m_Fishes = new List<Vector3>();

    public float sWeight = 1;
    public float cWeight = 1;
    public float aWeight = 1;

    private Vector3 previousDirection;

    //public BoidsBrain brain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SUM OF FISH" + sumFishPosition);
        CohesionCalc();

        AlignmentCalc();

        SeperationCalc();

        FinalDirection();

        //transform.position = Vector3.MoveTowards(transform.position, centerOfMassDirection.transform * speed);
    }

    void FixedUpdate()
    { // maybe make it so if something changes this is called? It may be currently clogging up the game a bit
    }

    void FinalDirection()
    {
        float speed = 200 * Time.deltaTime;

        var direction = (sWeight * seperationDirection) + (cWeight * centerOfMassDirection) + (aWeight * averageHeadingDirection);
               // + borderAvoidanceruleWeight * borderAvoidanceDirection

       // direction = Vector3.Lerp(direction, previousDirection, m_DirectionLowPassFilterCutoff);
        previousDirection = direction;

        foreach(GameObject fish in fishes)
        {
            var lookQuaternion = Quaternion.LookRotation(direction.normalized);
            transform.rotation = lookQuaternion;
            GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        }

        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }
    

    //float sumFishPosition;

    //Boid otherBoid = fishes[i];

    public Vector3 sumFishPosition;

    public Vector3 centerOfMassDirection;

    public Vector3 seperationDirection = Vector3.zero;

    public void CohesionCalc()
    {
       // Vector3 centerOfMassPoint;
       // var directionTransform;

       float fishesNumber = fishes.Count;

        foreach(GameObject fish in fishes) //change to boid BOID SCRIPT
        {
           var directionTransform = fish.transform;
           sumFishPosition += directionTransform.position;

            sumFishPosition += fish.transform.position;
        }  //MAYBE MOVE TO UPDATE

        Vector3 centerOfMass = sumFishPosition / fishesNumber;

        //gets mean average

        centerOfMassDirection = (centerOfMass - transform.position).normalized;
        //moves fish towards the center average point

        //float speed = 5f;

        /*foreach(GameObject fish in fishes) //change to boid BOID SCRIPT
        {
            fish.transform.position = Vector3.Lerp(fish.transform.position, m_CenterOfMassDirection, speed * Time.deltaTime);
        }*/
    }

    public void SeperationCalc()
    {
        Vector3 dist = transform.position; //////

        float nearbyFishRadius = 1f; //HAVE THIS CHANGE

        var nearbyFish = Physics.OverlapSphere(transform.position, nearbyFishRadius);

        foreach (var col in nearbyFish)
        {
            if(transform.position.Equals(col.transform.position))
            continue; //used to skip ahead

            //dist = (col.transform.position - transform.position).normalized;
            dist /= Vector3.Distance(col.transform.position, transform.position);
        }

        seperationDirection -= dist;

    }

    public Vector3 sumHeading;
    public Vector3 averageHeadingDirection;

    public void AlignmentCalc()
    {
        averageHeadingDirection = (transform.position - GetAverageHeading().normalized).normalized;
       // Vector3 centerOfMassPoint;

       Debug.Log("AVERAGE HEADING" + GetAverageHeading());

    }

    public Vector3 GetAverageHeading()
    {
       float fishesNumber = fishes.Count;

        foreach(GameObject fish in fishes) //change to boid BOID SCRIPT
        {
            var directionTransform = fish.transform;
            sumHeading += directionTransform.forward.normalized;
        }  //MAYBE MOVE TO UPDATE

        Vector3 averageHeading = sumHeading / fishesNumber;

        return averageHeading;

    }
}
