using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServicesManager
{
    public const string MainScene = "MainScene";
    
    private static ServicesManager _instance;
    public static ServicesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServicesManager();
            }

            return _instance;
        }
    }
    
    //Set logic services
    
    //AutoSet of Mono services
    protected UIStackController _UIStackController;

    public UIStackController UIStackController
    {
        get
        {
            if (_UIStackController == null)
            {
                _UIStackController = GameObject.FindObjectOfType<UIStackController>();
            }

            return _UIStackController;
        }
    }

    protected ServicesManager()
    {
       
    }

    

    public void GoToMainScene()
    {
        UIStackController.PopAll();
        SceneManager.LoadScene(MainScene);
    }

    public void ReloadCurrentLevel()
    {
        UIStackController.PopAll();
        SceneManager.LoadScene(string.Empty);
    }
}