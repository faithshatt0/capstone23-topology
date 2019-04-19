using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public int num;
    public Button button;
    public string m_Scene = "viewObject_scene";
    public GameObject m_MyGameObject;
    public Text text;

    private Button real; 

    private void Start()
    {
        //Adds a listener to the to the Go Back button
        real = GetComponent<Button>();
        real.onClick.AddListener(buttonWasClicked);

    }

    //If Go Back button in viewObject_Scene was clicked then destroy game object and reload test_scene
    void buttonWasClicked()
    {

        if (SceneManager.GetSceneAt(0).name.Equals("viewObject_scene"))
        {
            Object.Destroy(m_MyGameObject);

            SceneManager.LoadScene("test_scene");
        }
    }


    public void ChangeToObjScene()
    {

        //When the View assistant button is clicked in test_scenee it will load the new scene
        SceneManager.LoadScene("viewObject_scene");

        var nameOfObj = button.gameObject.transform.parent.parent.name;

        //Change the name of the button
        text.text = "Go Back";

        //Creating a clone of the game object to be used in the new scene
        DontDestroyOnLoad(m_MyGameObject);

        //Locatin for the game object
        float xCord = 35;
        float yCord = -50;
        float zCord = -140;
        m_MyGameObject.transform.position = new Vector3(xCord, yCord, zCord);

    }

}
