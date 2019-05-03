using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// ChangeSceneBack.cs
/// This script will bring the scene back to the main_scene once the "Go Back" button is pressed
/// 

public class ChangeSceneBack : MonoBehaviour
    {
    //If Go Back button in viewObject_Scene was clicked then destroy game object and reload main_scene
    public void buttonWasClicked()
        {
        if (SceneManager.GetSceneAt(0).name.Equals("viewObject_scene"))
            {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
                {
                if(o.transform.name != "Main Camera") 
                    Destroy(o);
                }
            SceneManager.LoadScene("main_scene");
            }
        }
    }
