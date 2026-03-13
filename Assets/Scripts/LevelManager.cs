using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> collectibles = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
