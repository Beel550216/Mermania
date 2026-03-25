using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int sceneCount;
    public bool inWater;

    [SerializeField] private Transform mainCam;
    [SerializeField] public int depth = -21;

    [SerializeField] private Volume postProcessing;

    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;

    //public List<GameObject> collectibles = new List<GameObject>();
    public List<string> collectibles = new List<string>();

    void Start()
    {
        SceneCheck();
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
    }


    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneCheck()
    {

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
}