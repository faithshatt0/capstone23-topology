using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        if (Input.GetMouseButtonDown(2))
            {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo); //gets info from what object is clicked

            //When you click and its not null itll spawn an info panel
            if (target != null)
                {
                if(target.gameObject.transform.Find("Informational Panel") != null && _isOpen == false)
                    {
                    Debug.Log(target.tag);
                    if(target.tag == "router")
                        {
                        target.gameObject.transform.Find("Informational Panel").position = new Vector3(target.gameObject.transform.Find("Informational Panel").position.x, target.gameObject.transform.Find("Informational Panel").position.y, 0);
                        _isOpen = true;
                        }
                    else
                        {
                        target.gameObject.transform.Find("Informational Panel").position = new Vector3(target.gameObject.transform.Find("Informational Panel").position.x, target.gameObject.transform.Find("Informational Panel").position.y, 10);
                        _isOpen = true;
                    }
                   
                    }
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
