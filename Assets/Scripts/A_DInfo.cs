using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// A_DInfo.cs
/// This script is being used to move the informational panels back and forth as you click on the objects
/// Why moving Informational panel instead of using .active() function
/// When we use the .active() function for a gameobject it will disappear like you want, but if you bring it back to being active it will be super blurry. 
/// We don't want it to be blurry so it is just moved to a far off location to give the impression of appearing and disappearing.
public class A_DInfo : MonoBehaviour
    {
    private GameObject target;
    private bool _isOpen;
    public Canvas myChildObject;
    
    // Start is called before the first frame update
    void Start()
        {
        _isOpen = false; //flag to make only one info panel open
        }
    
    // Update is called once per frame
    void Update()
        {
        //if the scroll wheel on the mouse is clicked 
        if (Input.GetMouseButtonDown(2))
            {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo); //gets info from what object is clicked

            //When you click and its not null itll spawn an info panel
            if (target != null)
                {
                //Finds the informational panel in the heirarchy of the object
                if(target.gameObject.transform.Find("Informational Panel") != null && _isOpen == false)
                    {
                    if(target.tag == "router") //brings informational panel location to router
                        {
                        target.gameObject.transform.Find("Informational Panel").position = new Vector3(target.gameObject.transform.Find("Informational Panel").position.x, target.gameObject.transform.Find("Informational Panel").position.y, 0);
                        _isOpen = true;
                        }
                    else //brings the informational panel to the devices
                        {
                        target.gameObject.transform.Find("Informational Panel").position = new Vector3(target.gameObject.transform.Find("Informational Panel").position.x, target.gameObject.transform.Find("Informational Panel").position.y, 10);
                        _isOpen = true;
                        }
                   
                    }

                //if the object is clicked again it will move the informational panel to a far off location to give impression of spawning
                else if (target.gameObject.transform.Find("Informational Panel") != null && _isOpen == true)
                    {
                    _isOpen = false;
                    target.gameObject.transform.Find("Informational Panel").position = new Vector3(target.gameObject.transform.Find("Informational Panel").position.x, target.gameObject.transform.Find("Informational Panel").position.y, -10000);
                }

            }
        }
    



    }
    //Get information on gameobject by clicking on it
    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }

}
