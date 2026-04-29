using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class Boids : MonoBehaviour
{

    [SerializeField] List<GameObject> fishes = new List<GameObject>();
    //[SerializeField] List<Vector3> m_Fishes = new List<Vector3>();

    //public BoidsBrain brain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SeperationCalc();
    }

    void FixedUpdate()
    {
        Debug.Log("SUM OF FISH" + sumFishPosition);
        CohesionCalc(); // maybe make it so if something changes this is called? It may be currently clogging up the game a bit
    }

    //float sumFishPosition;

    //Boid otherBoid = fishes[i];

    public Vector3 sumFishPosition;

    public void CohesionCalc()
    {
       // Vector3 centerOfMassPoint;
       // var directionTransform;

        float m_FishesNumber = fishes.Count;

        foreach(GameObject fish in fishes) //change to boid BOID SCRIPT
        {
           var directionTransform = fish.transform;
           sumFishPosition += directionTransform.position;

            sumFishPosition += fish.transform.position;
        }  //MAYBE MOVE TO UPDATE

        Vector3 centerOfMass = sumFishPosition / m_FishesNumber;

        //gets mean average

        var m_CenterOfMassDirection = (centerOfMass - transform.position).normalized;
        //moves fish towards the center average point

        float speed = 5f;

        /*foreach(GameObject fish in fishes) //change to boid BOID SCRIPT
        {
            fish.transform.position = Vector3.Lerp(fish.transform.position, m_CenterOfMassDirection, speed * Time.deltaTime);
        }*/
    }

    public void SeperationCalc()
    {
        Vector3 dist = transform.position; //////

        Vector3 seperationDirection = Vector3.zero;

        float nearbyFishRadius = 1; //HAVE THIS CHANGE

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
}
