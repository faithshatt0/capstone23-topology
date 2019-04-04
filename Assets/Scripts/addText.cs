using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addText : MonoBehaviour
{
    Text infoText;

    void Awake()
        {
        infoText = GetComponent<Text>();
        
        }
    void Update()
        {
        infoText.text = "Meep Morp";      
        }

}
