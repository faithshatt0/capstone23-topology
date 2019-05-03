using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Proyecto26;

/// moveRouter.cs
/// This script is to move the routers because these objects will actually move.
/// 
public class moveRouter : MonoBehaviour
    {
    Vector3 dist; //Distance
    float posX; //x
    float posY; //y
    float posZ; //z

    
    Vector3 prevLocation; //location so object doesn't move unless toggled to where it will
    // Initialize JsonMain script and start parsing Json
    //JsonMain jsonMain = new JsonMain();
    List<Topology> network_devices = new List<Topology>();
    List<string> serials = new List<string>();
    Vector3 worldPos; //helps move object
    public Toggle tog; //Toggle
    LocationsJsonParse location_data;

    private float _sensitivity = 0.01f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "viewObject_scene")
        {
            if (Input.GetMouseButton(1))
            {
                _isRotating = true;
                if (_isRotating)
                {
                    // get mouse offset
                    _mouseOffset = (Input.mousePosition - _mouseReference);
                    // apply rotation

                    _rotation.y = -(_mouseOffset.z + _mouseOffset.x) * _sensitivity; // rotate
                    gameObject.transform.Rotate(_rotation); // store new mouse position

                    return;
                }
            }

            if (!Input.GetMouseButton(1))
            {
                _isRotating = false;
                return;
            }

        }
    }

    // Start is called before the first frame update
    void Start()
        {
        // Retrieve network_devices and serials from JsonMain
        serials = spawner.serials;
        network_devices = spawner.network_devices;

        //If it is a master router it will toggled on automatically -- Rajesh wanted extenders to fixed default to where they can't move in the beginning
        for(int ii = 0; ii < network_devices.Count; ii++)
            {
            if(tog.gameObject.transform.parent.parent.name == serials[ii])
                {
                if(network_devices[ii].get_isMaster() == true)
                    {
                    tog.isOn = true;
                    }
                }
            }
            
        }

    //if mouse clicked on device
    void OnMouseDown()
        {
        if(SceneManager.GetActiveScene().name != "viewObject_scene")
            {
            prevLocation = transform.position; //get device position
            dist = Camera.main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;
            posZ = Input.mousePosition.z - dist.z;

            // Database Overwrite: Locations
            //    1. Delete stored location values ONLY if we're sure the Object's toggle is on
            if (tog.isOn)
            {
                DeleteToDatabase();
            }
            }
        else
            {
            //do rotating here 
            }
       
        }

    //if mouse is dragging on device
    void OnMouseDrag()
        {
        if (SceneManager.GetActiveScene().name != "viewObject_scene")
            {
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, Input.mousePosition.z - posZ);
            worldPos = Camera.main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
            }
        else
            {
            //Do Rotating here
            }
        }   

    //Once you let go of the mouse click
    void OnMouseUp()
        {
        if (SceneManager.GetActiveScene().name != "viewObject_scene")
            {
            //if the toggle button is not on
            if (!tog.isOn)
                {
                transform.position = prevLocation;
                }
            else
            {
                location_data = spawner.location_data;
                transform.position = new Vector3(worldPos.x, 1.5f, worldPos.z);
                //save location here 
                //loop through and get all the locations and then push into json.
                for (int ii = 0; ii < network_devices.Count; ii++)
                    {
                    location_data.serials[ii].x = GameObject.Find(location_data.serials[ii].serial).transform.position.x;
                    location_data.serials[ii].y = GameObject.Find(location_data.serials[ii].serial).transform.position.y;
                    location_data.serials[ii].z = GameObject.Find(location_data.serials[ii].serial).transform.position.z;
                    }

                string json = JsonUtility.ToJson(location_data);

                //gets the locations of the object you touched so you can move the devices its connected to with it
                float xx_router = transform.position.x;
                float yy_router = transform.position.y;
                float zz_router = transform.position.z;

                if (network_devices[serials.IndexOf(transform.name)].get_sta_clients().Count + network_devices[serials.IndexOf(transform.name)].get_eth_clients().Count > 1)
                    {
                    xx_router = xx_router - (5 * network_devices[serials.IndexOf(transform.name)].get_sta_clients().Count + network_devices[serials.IndexOf(transform.name)].get_eth_clients().Count);
                    }

                for (int ii = 0; ii < network_devices[serials.IndexOf(transform.name)].get_sta_clients().Count; ii++)
                    {
                    GameObject sta = GameObject.Find(network_devices[serials.IndexOf(transform.name)].get_sta_clients()[ii].target_mac);
                    sta.transform.position = new Vector3(xx_router, yy_router, zz_router + 10); //this changes the location of the devices that the router is connected to --sta_clients   
                    xx_router += 10;
                    }
                for (int ii = 0; ii < network_devices[serials.IndexOf(transform.name)].get_eth_clients().Count; ii++)
                    {
                    GameObject eth = GameObject.Find(network_devices[serials.IndexOf(transform.name)].get_eth_clients()[ii].target_mac);
                    eth.transform.position = new Vector3(xx_router, yy_router, zz_router + 10); //this changes the location of the devices that the router is connected to --eth_clients
                    xx_router += 10;
                    }

                // Database Overwrite: Locations
                //    2. Saves new locations on Firebase
                PostToDatabase(location_data);
                }
        }
        else
            {
            //rotating here
            }
           
    }

    
    // Firebase Requests
    private void DeleteToDatabase()
    {
        Debug.Log("Deleted");
        RestClient.Delete("https://capstone-topology.firebaseio.com/locations.json/");
    }
    
    private void PostToDatabase(LocationsJsonParse router_locations)
    {
        Debug.Log("Posted");
        RestClient.Post("https://capstone-topology.firebaseio.com/locations.json", router_locations);
    }
}
