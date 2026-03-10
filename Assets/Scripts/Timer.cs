using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
   [SerializeField] public float remainingTime;
   public bool inWater;
   private float maxTime = 300f;

   void Start()
   {
    remainingTime = maxTime;
   }


    void Update()
    {
        print("checking");

        if(inWater == true)
        {
            remainingTime = maxTime;
            //Time.timeScale = 0;
        }
        if(inWater == false)
        {
            print(inWater);

            //Time.timeScale = 1;
            if( remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            inWater = true;
            print("inWater");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            inWater = false;
            print("outWater");
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Play()
    {
        Time.timeScale = 1;
    }
}