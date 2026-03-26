using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSController : MonoBehaviour
{
    public static FPSController instance;
    public void Start()
    {
        if (instance != null && instance != this) Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }
        
    }

    void ChangedActiveScene(Scene currentScene, Scene nextScene)
    {
        //if(nextScene.name.Equals("GamePlay"))
        if(nextScene.buildIndex == 2)
        {
            Application.targetFrameRate = 30;
        }
        else
        {
            Application.targetFrameRate = 60;
        }
    }
}
