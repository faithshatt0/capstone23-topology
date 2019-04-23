using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class birdsEye : MonoBehaviour
{
    public Button butt; //connects button we push
    public Camera cam; //attach camera
    public Text text; //attach text
   
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Birdseye";
    }

    // Update is called once per frame
    public void ButtonInteract()
    {
        //Normal View
        if(text.text == "Normal")
        {
            text.text = "Birdseye";

            //Set Main Camera position & rotation back to normal view
            GameObject.Find("Main Camera").transform.position = new Vector3(0, 25, -50);
            GameObject.Find("Main Camera").transform.LookAt(Vector3.zero);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(
                new Vector3(
                    30,
                    GameObject.Find("Main Camera").transform.rotation.eulerAngles.y,
                    GameObject.Find("Main Camera").transform.rotation.eulerAngles.z
                )
            );
        }
        //Birdseye View
        else
        {
            text.text = "Normal";

            //Set Main Camera position & rotation to Birdseye View
            GameObject.Find("Main Camera").transform.position = new Vector3(0, 100, 0);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(
                new Vector3(
                    90,
                    GameObject.Find("Main Camera").transform.rotation.eulerAngles.y,
                    GameObject.Find("Main Camera").transform.rotation.eulerAngles.z
                )
            );
        }
    }         
}
