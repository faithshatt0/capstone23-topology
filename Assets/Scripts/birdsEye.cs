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
        text.text = "Normal";
        }

    // Update is called once per frame
    public void ButtonInteract()
        {
        //Change camera view here not in Start()!
        //Normal View
        if(text.text == "Normal")
            {
            text.text = "Birdseye";
            }
        //Birdseye View
        else
            {
            text.text = "Normal";
            }
        }         
}
