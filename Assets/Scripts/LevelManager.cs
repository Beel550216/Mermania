using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEditor.Rendering.CameraUI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int sceneCount;
    public bool inWater;

    public GameObject inventory;
    public GameObject pause;
    public GameObject kiosk;
    public GameObject map;

    public int stone = 0;


    [SerializeField] private Transform mainCam;
    [SerializeField] public int depth = -21;

    [SerializeField] private Volume postProcessing;

    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;


    //public List<GameObject> collectibles = new List<GameObject>();
    public List<string> collectibles = new List<string>();

    public List<string> removeCollectible = new List<string>();

    public TMP_Text stoneText;
    public TMP_Text coconutText;
    public TMP_Text combText;

    public float bonus = 1;
    public float moneyAmount;

    public int soldAmount = 0;
    public TMP_Text currentTotalText;
    public TMP_Text moneyTotal;
    void Start()
    {
        SceneCheck();
        //killPlayer = false;
        //CheckForPlayerDeath(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCam.position.y < 34)
        {
            EnableEffects(true);
        }
        else
        {
            EnableEffects(false);
        }
        Debug.Log("Current depth " + depth);
        Debug.Log("Effects " + enabled);

        CheckForKeys();
        CheckPause();

    }


    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneCheck()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            //Do stuff here

            //counter = 0;
            //camCount = 4;               // counter for the camera movement
            //CutsceneList(0);
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            //Do stuff here

            //counter = 0;
            //camCount = 4;               // counter for the camera movement
            //CutsceneList(0);
        }

        print(SceneManager.GetActiveScene().name);

    }


    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Play()
    {
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
    }

    private void EnableEffects(bool enable)
    {
        if (enable)
        {
            RenderSettings.fog = true;
            postProcessing.profile = underwaterPostProcessing;
        }
        else
        {
            RenderSettings.fog = false;
            postProcessing.profile = surfacePostProcessing;
        }

    }

    void CameraMovement()
    {
        //new Vector3(212.3, 33, -185.1);
    }

    public void CheckForKeys()
    {
        Debug.Log("KEY RUNNING");

        if(Input.GetKeyDown(KeyCode.X))
        {
            //inventory = GameObject.FindWithTag("Inventory");

            if(Time.timeScale == 1)
            {
                Debug.Log("PRESSED X KEY");
                UpdateInventory();
                kiosk.SetActive(false);
                inventory.SetActive(true);
                Pause();
            }
            else
            {
                inventory.SetActive(false);
                Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            //inventory = GameObject.FindWithTag("Inventory");

            if(Time.timeScale == 1)
            {
                Debug.Log("PRESSED X KEY");
                map.SetActive(true);
                Pause();
            }
            else
            {
                map.SetActive(false);
                Play();
            }
        }
    }

    public void UpdateInventory()
    {
        int stoneCount = 0;
        int coconutCount = 0;
        int combCount = 0;

        for (int i = 0; i < collectibles.Count; i++)
        {
            if (collectibles[i] == "Stone")
            {
                stoneCount++;
            }
            if (collectibles[i] == "Coconut")
            {
                coconutCount++;
            }
            if (collectibles[i] == "Comb")
            {
                combCount++;
            }
        }

        stoneText.text = stoneCount.ToString();
        coconutText.text = coconutCount.ToString();
        combText.text = combCount.ToString();
        Debug.Log("STONE " + stoneCount);
    }

    public void AddSellItem(string item)
    {
        //int soldAmount;

        removeCollectible.Add(item);
        TotalSell();

    }

    public void TotalSell()
    {
        for (int i = 0; i < removeCollectible.Count; i++)
        {
            if (removeCollectible[i] == "Stone")
            {
                soldAmount = soldAmount + 20;

            }
            if (removeCollectible[i] == "Coconut")
            {
                soldAmount = soldAmount + 30;
            }
            if (removeCollectible[i] == "Comb")
            {
                soldAmount = soldAmount + 150;
            }

        }

        currentTotalText.text = soldAmount.ToString();
    }




    public void SellItem()
    {
        for (int i = 0; i < removeCollectible.Count; i++)
        {
            collectibles.Remove(removeCollectible[i]);
            //selling the collectible removes that collectible from players inventory


            removeCollectible.Remove(removeCollectible[i]);
            //processed this collectiblee (added it to total), so can remove from the main selling list

        }

        moneyAmount = moneyAmount + (soldAmount * bonus);
        moneyTotal.text = moneyAmount.ToString();

        //(Get Amount that the player has) *(sell amount* bonus(base bonus = 1)) → Output to text

    }

    /*public void CheckForPlayerDeath(bool kill)
    {
        if(kill == true) //change this to if player has reset
        {
            Debug.Log("PLAYER DEAD");
            killPlayer = true;
        }
        else
        {
            killPlayer = false;
        }
    }*/

}