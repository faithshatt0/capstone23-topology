using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addName : MonoBehaviour
{
    Text infoText;

    void Awake()
    {
        infoText = GetComponent<Text>();

    }
    void Update()
    {
        infoText.text = "Router Meme";
    }
}
