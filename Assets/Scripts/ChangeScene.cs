using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public int num;
    public Button button;
    public viewObject test;
    // Start is called before the first frame update
    public string m_Scene = "viewObject_scene";
    public GameObject m_MyGameObject;
    public GameObject UIRootObject;
    private AsyncOperation sceneAsync;
    public Text text;

    private void Start()
    {
       // button.enabled = false;
        Debug.Log("start"+SceneManager.GetSceneAt(0).name);
    }


    private void Update()
    {
        //if (SceneManager.GetSceneAt(0).name.Equals("viewObject_scene") && GameObject.Find("Go Back:").;
        //{
        //    Debug.Log("good");
        //}
        //if (button.on == false && SceneManager.GetSceneAt(1).Equals(1))
        //{
        //    Debug.Log("pressed");
        //}
        //else
        //{
        //    Debug.Log("NOPE");
        //}

        // button.onClick.AddListener(buttonWasClicked);

        //if(SceneManager.GetSceneAt(0).Equals(0))
        //{
        //    Debug.Log("djah");
        //    button.onClick.AddListener(buttonWasClicked);
        //}
        //else
        //{
        //    Debug.Log("Wrong scene");
        //}


        if (SceneManager.GetSceneAt(0).name.Equals("viewObject_scene"))
        {
            Debug.Log("update" + SceneManager.GetSceneAt(0).name);

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("yup, pressed");
                buttonWasClicked();
                // button.onClick.AddListener(buttonWasClicked);
            }
        }
    }

    public void buttonWasClicked()
    {
        if (SceneManager.GetSceneAt(0).name.Equals("viewObject_scene"))
        {
            //{
            //    Debug.Log("in button was clicked");

            Object.Destroy(m_MyGameObject);

            SceneManager.LoadScene("test_scene");
        }

    
        //Debug.Log("update" + SceneManager.GetSceneAt(0).name);
        //Debug.Log("in button was clicked");

        //Object.Destroy(m_MyGameObject);

        //SceneManager.LoadScene("test_scene");
    }

    public void ChangeToObjScene()
    {

        //Text   txt;
        //txt = textRef;
        //Debug.Log(txt);
        //mainCam = Camera.mainCamera;
        //mainCam.transform.position = new Vector3(5, 25, -50);

        //GameObject.Find("Main Camera");
       // Debug.Log(GameObject.Find("Main Camera").transform.position.x);
        //.transform.position = new Vector3(5, 25, -50);

        //SceneManager.LoadScene("viewObject_scene");
        var nameOfObj = button.gameObject.transform.parent.parent.name;
        Debug.Log(nameOfObj);
        if (nameOfObj == "50:a6:7f:d9:10:5a")
        {
            text.text = "Go Back";
            Debug.Log("goo dname");

            Scene sceneToLoad = SceneManager.GetSceneAt(0);
            GameObject package = Instantiate(m_MyGameObject) as GameObject;
            DontDestroyOnLoad(m_MyGameObject);

      

            SceneManager.LoadScene("viewObject_scene");
      
            SceneManager.MoveGameObjectToScene(package, sceneToLoad);

            // Button.GetComponentsInChildren().text = "New Super Cool Button Text";

            Debug.Log(GameObject.Find("View Assistant").GetComponentInChildren<Text>().text);
            // = "Go Back";

            //txt.text = "i am a button!";

            float xCord = 35;
            float yCord = -50;
            float zCord = -140;
            m_MyGameObject.transform.position = new Vector3(xCord, yCord, zCord);

        }

        //if (button.enabled == true && SceneManager.GetSceneAt(1).Equals(1))
        //{
        //    Debug.Log("pressed"); 
        //}
        //else
        //{
        //    Debug.Log("NOPE");
        //}

        Debug.Log("In Scene");
        
       

        //GameObject paddleGameObject = GameObject.Find("home_assistant");
        //test = GameObject.Find("home_assistant").GetComponent<viewObject>();
        //test.ViewObject();
        //if ((test == null) && (GetComponent<viewObject>() != null))
        //{
        //    test = GetComponent<viewObject>();
        //    test.ViewObject();
        //}
        //else
        //{
        //    Debug.LogWarning("Missing Component. Please Add one");
        //}
    }

    //IEnumerator LoadYourAsyncScene()
    //{
    //    // Set the current Scene to be able to unload it later
    //    Scene currentScene = SceneManager.GetActiveScene();

    //    // The Application loads the Scene in the background at the same time as the current Scene.
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_Scene, LoadSceneMode.Additive);

    //    // Wait until the last operation fully loads to return anything
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }

    //    // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
    //    SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByName(m_Scene));
    //    // Unload the previous Scene
    //    SceneManager.UnloadSceneAsync(currentScene);
    //}

}
