using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveObjects : MonoBehaviour
{
    /*
    public Button button;
    private bool _mouseState;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;
   
    public Vector3 real_position;
    */
    Vector3 dist;
    float posX;
    float posY;
    float posZ;
    Vector2 prevLocation;
    // Initialize JsonMain script and start parsing Json
    JsonMain jsonMain = new JsonMain();
    List<string> serials = new List<string>();
    public string remember_name;
    bool canMove;
    Vector3 worldPos;
    public Toggle tog;

    // Start is called before the first frame update
    void Start()
    {
        jsonMain.Start();
        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = jsonMain.GetDevices();
        serials = jsonMain.GetSerials();
        tog.isOn = false;
    }


    void OnMouseDown()
    {
        prevLocation = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseDrag()
    {
        
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, Input.mousePosition.z - posZ);
        worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }
    void OnMouseUp()
    {
        if(!tog.isOn)
        {
            transform.position = prevLocation;
        }
        else
        {
            transform.position = new Vector3(worldPos.x, 1.5f, worldPos.z);
        }


    }

    //load will be on spawner and i will access the positions with the serials from the json file. then for this script i will make it where the dragging and dropping
    //from spawner and actually move. Then raymond will save to json. Also move the info panel until you drop probably? 
    /*
       void SaveLocation(string file_path, string serial, double x, double y, double z)
       {
           int index = serials.BinarySearch(serial);
           location_data.serials[index].x = x;
           location_data.serials[index].y = y;
           location_data.serials[index].z = z;

           string json = JsonUtility.ToJson(location_data);
           File.WriteAllText(file_path, json);
       }
       
       if (Input.GetMouseButtonDown(0)) //left click
            {
            RaycastHit hitInfo;
    target = GetClickedObject(out hitInfo); //gets info from what object is clicked
            
            //if you are actually clicking on an object it will allow you to drag it to a new location
            if (target != null)
                {
                real_position = target.transform.position;
                _mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                }
            }
        if (Input.GetMouseButtonUp(0))
            {
            _mouseState = false;
            if(target != null)
                {
                target.transform.position = real_position;
                }
            }

        if (_mouseState)
            {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

//convert the screen mouse position to world point and adjust with offset
var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

//update the position of the object in the world
target.transform.position = curPosition;
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
*/
}
