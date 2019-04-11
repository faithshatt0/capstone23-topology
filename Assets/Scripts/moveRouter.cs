using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

/// moveRouter.cs
/// This script is to move the routers because these objects will actually move.
/// 
public class moveRouter : MonoBehaviour
    {
    Vector3 dist; //Distance
    float posX; //x
    float posY; //y
    float posZ; //z

    /// MoveObj positions
    float pX;
    float pY;
    float pZ;
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
        }

    //if mouse clicked on device
    void OnMouseDown()
        {
        prevLocation = transform.position; //get device position
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
        }
    
    //if mouse is dragging on device
    void OnMouseDrag()
        {   
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, Input.mousePosition.z - posZ);
        worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
        }

    //Once you let go of the mouse click
    void OnMouseUp()
        {
        //if the toggle button is not on
        if(!tog.isOn)
            {
            transform.position = prevLocation;
            }
        else
            {
            location_data = jsonMain.GetLocationData();
            locations_file_path = jsonMain.GetLocationsFilePath();
            transform.position = new Vector3(worldPos.x, 1.5f, worldPos.z);
            //save location here 
            int index = serials.BinarySearch(transform.name);
            location_data.serials[index].x = transform.position.x;
            location_data.serials[index].y = transform.position.y;
            location_data.serials[index].z = transform.position.z;

            string json = JsonUtility.ToJson(location_data);
            File.WriteAllText(locations_file_path, json);

            List<Topology> network_devices = jsonMain.GetDevices();
            float xx_router = transform.position.x;
            float yy_router = transform.position.y;
            float zz_router = transform.position.z;

            if (network_devices[serials.IndexOf(transform.name)].get_sta_clients().Count + network_devices[serials.IndexOf(transform.name)].get_eth_clients().Count > 1)
                {
                xx_router = xx_router - (5 * network_devices[serials.IndexOf(transform.name)].get_sta_clients().Count + network_devices[serials.IndexOf(transform.name)].get_eth_clients().Count);
                }             
            
            for(int ii = 0; ii < network_devices[serials.IndexOf(transform.name)].get_sta_clients().Count; ii++)
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
    
    /*
       void SaveLocation(string file_path, string serial, double x, double y, double z)
       {
           
       }
     */
}
