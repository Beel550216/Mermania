using UnityEngine;

public class Traders : MonoBehaviour
{
    public GameObject tradingScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("PLAYER ENTERED TRADING ZONE");
            tradingScreen.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Time.timeScale = 1;
            tradingScreen.SetActive(false);

        }
    }

}