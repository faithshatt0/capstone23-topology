using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class viewObject : MonoBehaviour
{
    public Button butt;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ViewObject()
    {

       // Ray ray = Camera.ScreenPointToRay(butt.gameObject.);

        var scaleFactor = 1.5f;          if (Input.GetMouseButtonDown(0)) //left click
        {
            Debug.Log("Working almost");
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);             RaycastHit hit;             if (Physics.Raycast(ray, out hit))             {                 //Get name of object                 var nameOfObj = GetClickedObject(out hit).name;                 var enlargeObject = GameObject.Find(nameOfObj).transform;                  //Move the objects position                  float xCord = 0;                 float yCord = 10;                 float zCord = -20;                 enlargeObject.position = new Vector3(xCord, yCord, zCord);                                 //Makes the object bigger                 float xScale = enlargeObject.localScale.x;                 float yScale = enlargeObject.localScale.y;                 float zScale = enlargeObject.localScale.z;                  enlargeObject.localScale = new Vector3(xScale * 2, yScale * 2, zScale * 2);


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
