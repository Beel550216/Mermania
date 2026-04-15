using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int sceneCount;
    public bool inWater;

    public GameObject inventory;
    public GameObject map;

    public int stone = 0;


    [SerializeField] private Transform mainCam;
    [SerializeField] public int depth = -21;

    [SerializeField] private Volume postProcessing;

    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;


    //public List<GameObject> collectibles = new List<GameObject>();
    public List<string> collectibles = new List<string>();

    public TMP_Text stoneText;
    public TMP_Text coconutText;

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
        }

        stoneText.text = stoneCount.ToString();
        coconutText.text = coconutCount.ToString();
        Debug.Log("STONE " + stoneCount);
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