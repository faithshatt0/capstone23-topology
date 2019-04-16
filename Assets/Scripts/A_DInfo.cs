using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// A_DInfo.cs
/// This script is being used to move the informational panels back and forth as you click on the objects
/// Why moving Informational panel instead of using .active() function
/// When we use the .active() function for a gameobject it will disappear like you want, but if you bring it back to being active it will be super blurry. 
/// We don't want it to be blurry so it is just moved to a far off location to give the impression of appearing and disappearing.
/// 
public class A_DInfo : MonoBehaviour
    {
    private GameObject target;
    private bool _isOpen;
    public Canvas myChildObject;
    public Button butt;

    // Start is called before the first frame update
    void Start()
        {
        _isOpen = false; //flag to make only one info panel open
        }

    // Update is called once per frame
    public void ButtonInteract()
        {
    
        //Finds the informational panel in the heirarchy of the object
        if (butt.gameObject.transform.parent.parent.Find("Informational Panel") != null && _isOpen == false)
            {
            if (butt.transform.parent.parent.tag == "router") //brings informational panel location to router
                {
                butt.gameObject.transform.parent.parent.Find("Informational Panel").position = new Vector3(butt.gameObject.transform.parent.parent.Find("Informational Panel").position.x, butt.gameObject.transform.parent.parent.Find("Informational Panel").position.y, butt.gameObject.transform.parent.parent.Find("Informational Panel").parent.position.z);
                _isOpen = true;
                }
            else //brings the informational panel to the devices
                {
                butt.gameObject.transform.parent.parent.Find("Informational Panel").position = new Vector3(butt.gameObject.transform.parent.parent.Find("Informational Panel").position.x, butt.gameObject.transform.parent.parent.Find("Informational Panel").position.y, butt.gameObject.transform.parent.parent.Find("Informational Panel").parent.position.z);
                _isOpen = true;
                }
            }

        //if the object is clicked again it will move the informational panel to a far off location to give impression of spawning
        else if (butt.gameObject.transform.parent.parent.Find("Informational Panel") != null && _isOpen == true)
            {
            _isOpen = false;
            butt.gameObject.transform.parent.parent.Find("Informational Panel").position = new Vector3(butt.gameObject.transform.parent.parent.Find("Informational Panel").position.x, butt.gameObject.transform.parent.parent.Find("Informational Panel").position.y, -10000);
            }
        }
    }