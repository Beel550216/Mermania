using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int sceneCount;
    public bool inWater;

    public GameObject inventory;
    public GameObject pause;
    public GameObject kiosk;
    public GameObject map;
    public GameObject deadScreen;

    public int stone = 0;


    [SerializeField] private Transform mainCam;
    public GameObject mainCamGO;
    [SerializeField] public int depth = -21;

    [SerializeField] private Volume postProcessing;

    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;

    public GameObject audioManagerObject;
    public AudioManager audioManager;

    //public List<GameObject> collectibles = new List<GameObject>();
    public List<string> collectibles = new List<string>();

    public List<string> removeCollectible = new List<string>();

    public TMP_Text stoneText;
    public TMP_Text coconutText;
    public TMP_Text combText;

    public float bonus = 1;
    public float moneyAmount;

    public float soldAmount = 0;
    public float buyAmount = 0;
    public TMP_Text currentTotalText;
    public TMP_Text moneyTotal;
    
    
    public float profitBuyPrice = 1000;
    public float waterBuyPrice = 1000;
    public float speedBuyPrice = 1000;
    public float toolBuyPrice = 1000;

    public int stoneCount = 0;
    public int coconutCount = 0;
    public int combCount = 0;

    public List<TMP_Text> buyItemsList = new List<TMP_Text>();

    public List<GameObject> tutorialList = new List<GameObject>();
    public List<string> tutorialTextList = new List<string>();
    private int tutorialTextNum = 0;
    public TMP_Text tutorialText;

    void Start()
    {
        GameObject mainCamGO = GameObject.FindGameObjectWithTag("MainCamera");
        mainCam = mainCamGO.GetComponent<Transform>();
        //mainCamGO = GetComponent(FindGameObjectWithTag("MainCamera"));
        //mainCam = gameObject.FindWithTag("Main Camera").Transform;
        SceneCheck();
        //killPlayer = false;
        //CheckForPlayerDeath(false);
        TutorialList();
        TutorialText();

        // /audioManager = audioManagerObject.GetComponent<AudioManager>();
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
            //audioManager.PlayBGM(1);
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
            RenderSettings.fogDensity = 0.01f;
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
        stoneCount = 0;
        coconutCount = 0;
        combCount = 0;

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

        Debug.Log(stoneCount);
        coconutText.text = coconutCount.ToString();
        combText.text = combCount.ToString();
        Debug.Log("STONE " + stoneCount);
    }
    
    public int stoneSellCount = 1;
    public int coconutSellCount = 1;

    public void AddSellItem(string item)
    {
        //int soldAmount;

        //if(collectibles)
        UpdateInventory();

            if (item == "Stone" && stoneSellCount <= stoneCount)
            {
                stoneSellCount++;

                //if(stoneSellCount <= stoneCount)
               // {
                    //soldAmount = soldAmount + 20;
                    Debug.Log("STONE COUNT" + stoneSellCount);
                    Debug.Log("CURRENT STONE COUNT" + stoneCount);
                    Succeeded(item);
               // }

            }
            if (item == "Coconut" && coconutSellCount <= coconutCount) //STOPS INFINTELY BUYING
            {
                coconutSellCount++;
                Debug.Log("COCONUT COUNT" + coconutSellCount);
                Debug.Log("CURRENT COCONUT COUNT" + coconutCount);
                Succeeded(item);

                //soldAmount = soldAmount + 30;
            }
            if (item == "Comb")
            {
                //soldAmount = soldAmount + 150;
            }
            else
            {
                Debug.Log("TRANSACTION FAILED :(");
                //return;
                Failed();
            }

    }

    public void Failed()
    {
        Debug.Log("FAILED :(");
    }

    public void Succeeded(string item)
    {
        removeCollectible.Add(item);
        //Debug.Log("STONE TOTAL COUNT: " + removeCollectible.Count);
        TotalSell();
        Debug.Log("SUCCEEDED :)");
    }

     public void TotalSell()
    {
        for (int i = 0; i < removeCollectible.Count; i++) //WAS ONLY DOING LESS THAN
        {
            if (removeCollectible[i] == "Stone")
            {
                soldAmount = soldAmount + 20;
                Debug.Log("STONE SOLD");

            }
            if (removeCollectible[i] == "Coconut")
            {
                soldAmount = soldAmount + 30;
                Debug.Log("COCONUT SOLD");
            }
            if (removeCollectible[i] == "Comb")
            {
                soldAmount = soldAmount + 150;
            }

        }

        soldAmount = soldAmount * bonus;
        soldAmount = Mathf.Round(soldAmount * 100f)/ 100f;

        currentTotalText.text = soldAmount.ToString();
    }



    public void BuyItem(string item)
    {
        //int soldAmount;

        //removeCollectible.Add(item);
        //TotalSell();

        //if(moneyAmount >= buyAmount)

        if (item == "Speed Boost")
        {
            buyAmount = speedBuyPrice;
            //CheckFunds(speedBuyPrice);

            if(CheckFunds(speedBuyPrice) == true)
            {
                speedBuyPrice = speedBuyPrice * 1.5f;
                RemoveMoney(buyAmount);
            }

            buyItemsList[0].text = speedBuyPrice.ToString();
        }
        if (item == "Water Boost")
        {
            buyAmount = waterBuyPrice;

            if (CheckFunds(waterBuyPrice) == true)
            {
                waterBuyPrice = waterBuyPrice * 1.5f;
                RemoveMoney(buyAmount);
            }

            buyItemsList[1].text = waterBuyPrice.ToString();
        }
        if (item == "Tool Boost")
        {
            buyAmount = toolBuyPrice;

            if (CheckFunds(speedBuyPrice) == true)
            {
                toolBuyPrice = toolBuyPrice * 1.5f;
                RemoveMoney(buyAmount);
            }

            buyItemsList[2].text = toolBuyPrice.ToString();
        }
        if (item == "Profit Boost")
        {
            buyAmount = profitBuyPrice;

            if (CheckFunds(profitBuyPrice) == true)  //if players funds are >= the amount that this costs, they can buy it
            {
                bonus = bonus * 1.5f;

                profitBuyPrice = profitBuyPrice * 1.5f;    //price for that item increases
                RemoveMoney(buyAmount);
            }

            buyItemsList[3].text = profitBuyPrice.ToString();
        }


    }

    private bool CheckFunds(float price)
    {
        if(moneyAmount >= price)
        {
            return true;
        }
        return false;
    }

    private void RemoveMoney(float buyAmount)
    {
        moneyAmount = moneyAmount - buyAmount;
        moneyTotal.text = moneyAmount.ToString();
    }

    private void RemoveCollectible(string collectible)
    {
        collectibles.Remove(collectible);
        //selling the collectible removes that collectible from players inventory


        removeCollectible.Remove(collectible);
        //processed this collectiblee (added it to total), so can remove from the main selling list
    }




    public void SellItem()
    {
        for (int i = 0; i < removeCollectible.Count; i++)
        {
            Debug.Log("TOTAL: " + i);
            Debug.Log("TOTAL FR: " + removeCollectible.Count);
            if (removeCollectible[i] == "Stone" && stoneCount > 0) // && stoneCount > 0
            {
                stoneCount--;

                collectibles.Remove(removeCollectible[i]);
                removeCollectible.Remove(removeCollectible[i]);
                //RemoveCollectible(removeCollectible[i]);
            }
            if (removeCollectible[i] == "Coconut" && coconutCount > 0)
            {
                coconutCount--;
                collectibles.Remove(removeCollectible[i]);
                RemoveCollectible(removeCollectible[i]);
            }
            if (removeCollectible[i] == "Comb" && combCount > 0)
            {
                combCount--;
                collectibles.Remove(removeCollectible[i]);
                RemoveCollectible(removeCollectible[i]);
            }
            else
            {
                //change text to display "ERROR" or like "not enough funds :("
                Debug.Log("ERROR: PLAYER DOESN'T HAVE THIS ITEM!");
                //break;
            }

            Debug.Log("TOTAL LOOP COMPLETED");


        }

        moneyAmount = moneyAmount + (Mathf.Round(soldAmount * bonus * 100f) /100f); //editied this
        moneyTotal.text = moneyAmount.ToString();

        soldAmount = 0;
        currentTotalText.text = soldAmount.ToString();

        stoneSellCount = 1;
        coconutSellCount = 1;
        Debug.Log("NEW STONE AMOUNT:" + stoneSellCount);

        UpdateInventory(); //NEEDS TO UPDATE OTHERWISE IT WILL COUNT THE ITEMS THAT YOU JUST SOLD
        //(Get Amount that the player has) *(sell amount* bonus(base bonus = 1)) → Output to text

    }

    public void Dead()
    {
        deadScreen.SetActive(true);
        Time.timeScale = 0;

    }

    public int tutorialNum;

    public void TutorialText()
    {
        string text = tutorialTextList[tutorialNum];
        tutorialText.text = text;

        tutorialNum++;
        Debug.Log("TUTORIAL Num " + tutorialNum);

        Pause();

        //if(tutorialNum == 1)
        //{
           // Play();
        //}
        if(tutorialNum == 3)
        {
            Pause();
        }

    }

    public void TutorialList()
    {
        tutorialTextList.Add("Welcome to the Mermania tutorial!");
        tutorialTextList.Add("Use the WASD keys to move");
        tutorialTextList.Add("Press the shift key to sprint");

        tutorialTextList.Add("You will start to dry out on land");
        tutorialTextList.Add("Keep track of the water meter!");

        tutorialTextList.Add("The water meter will replenish in water");
        tutorialTextList.Add("Your mermaid form will show in water");
        tutorialTextList.Add("To swim upwards press the space bar");

        tutorialTextList.Add("Collect items by walking into them");
        tutorialTextList.Add("These items can be traded for clams at kiosks");

        tutorialTextList.Add("Clams allow you to buy upgrades");

        tutorialTextList.Add("Your inventory can be viewed by pressing X");
        tutorialTextList.Add("The world map can be viewed by pressing M");

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