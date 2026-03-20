using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int sceneCount;
    public bool inWater;

    //public List<GameObject> collectibles = new List<GameObject>();
    public List<string> collectibles = new List<string>();

    void Start()
    {
        SceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
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


}