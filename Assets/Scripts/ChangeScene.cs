﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// ChangeScene.cs
/// This script will move the scene to the viewObject scene and will alter the gameobect that we clicked on. 
/// The info panel and the rssi button will move to a far off location so the object is just the object. 
/// 

public class ChangeScene : MonoBehaviour
    {
    public Button button;
    public string m_Scene = "viewObject_scene";
    public GameObject m_MyGameObject;
    public static string ret;

    public void ChangeToObjScene()
        {
        SceneManager.LoadScene("viewObject_scene");
        //Creating a clone of the game object to be used in the new scene
        DontDestroyOnLoad(m_MyGameObject);
        ret = m_MyGameObject.name;
        //Locatin for the game object
        float xCord = -5;
        float yCord = 10;
        float zCord = 15;
        m_MyGameObject.transform.position = new Vector3(xCord, yCord, zCord);
        m_MyGameObject.transform.localScale += new Vector3(2, 2, 2);
        m_MyGameObject.transform.Find("Informational Panel").transform.position = new Vector3(0, -10000, 0);
        m_MyGameObject.transform.Find("RSSI Info").transform.position = new Vector3(0, -10000, 0);
        }
    }
