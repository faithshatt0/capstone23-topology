﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

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
    JsonMain jsonMain = new JsonMain();
    List<string> serials = new List<string>();
    Vector3 worldPos; //helps move object
    public Toggle tog; //Toggle
    LocationsJsonParse location_data;
    string locations_file_path;
    
    // Start is called before the first frame update
    void Start()
        {
        jsonMain.Start();
        // Retrieve network_devices and serials from JsonMain
        serials = jsonMain.GetSerials();
        List<Topology> network_devices = jsonMain.GetDevices();

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
                List<Topology> network_devices = jsonMain.GetDevices();
                location_data = jsonMain.GetLocationData();
                locations_file_path = jsonMain.GetLocationsFilePath();
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
                File.WriteAllText(locations_file_path, json);

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
                }
        }
        else
            {
            //rotating here
            }
           
    }
}
