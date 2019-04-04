using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_DInfo : MonoBehaviour
    {
    private GameObject target;
   
    public Canvas myChildObject;
   
    // Start is called before the first frame update
    void Start()
        {
        
        }

    // Update is called once per frame
    void Update()
        {
        
        if (Input.GetMouseButtonDown(0))
            {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo); //gets info from what object is clicked
            if (target != null)
                {

                myChildObject.transform.position = new Vector3(myChildObject.transform.position.x, myChildObject.transform.position.y, myChildObject.transform.position.z + 10000f);
                }
            //moveCan.transform.position = new Vector3(moveCan.transform.position.x, moveCan.transform.position.y, moveCan.transform.position.z + 10000f);
        }
        //if(target != null)

            // Debug.Log(target);
            //target.gameObject.transform.Find("Informational Panel")
            //target.gameObject.transform.Find("Informational Panel").position = new Vector3(target.gameObject.transform.Find("Informational Panel").position.x, target.gameObject.transform.Find("Informational Panel").position.y, target.gameObject.transform.Find("Informational Panel").position.z - 10000f);




            //target.GetComponentInChildren<Canvas>().transform.position = new Vector3(target.GetComponent<Canvas>().transform.position.x, target.GetComponent<Canvas>().transform.position.y, target.GetComponent<Canvas>().transform.position.z + 10000f); ;
            //target.GetComponent<Canvas>().transform.position = new Vector3(target.GetComponent<Canvas>().transform.position.x, target.GetComponent<Canvas>().transform.position.y, target.GetComponent<Canvas>().transform.position.z + 10000f);
            //}


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
