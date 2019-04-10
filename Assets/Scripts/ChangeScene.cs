using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public int num;
    public Button button;
    viewObject test;
    // Start is called before the first frame update
       
    public void ChangeToObjScene()
    {
        SceneManager.LoadScene("viewObject_scene");
        Debug.Log("In Scene");
        if ((test == null) && (GetComponent<viewObject>() != null))
        {
            test = GetComponent<viewObject>();
            test.ViewObject();
        }
        else
        {
            Debug.LogWarning("Missing Component. Please Add one");
        }
    }
 
}
