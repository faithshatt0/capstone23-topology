﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private bool _mouseState;
    public GameObject platform;
    public GameObject router;
    public GameObject phone;
    public GameObject laptop;
    public GameObject assistant;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;
    public Vector3 real_position;

    // Start is called once at the beginning of the program
    void Start()
        {
        // Initialize JsonMain script and start parsing Json
        JsonMain jsonMain = new JsonMain();
        jsonMain.Start();

        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = jsonMain.GetDevices();
        List<string> serials = jsonMain.GetSerials();

        // Template transform variable for GameObject positioning and rotation
        Transform objTrans = new GameObject().transform;

        // Render scene
        objTrans.position = new Vector3(0, -0.5f, 0);
        Instantiate(platform, objTrans.position, objTrans.rotation);

        List<int> nextCoordinate = new List<int> { 1, 1, 1, 1 };
        Vector3 objPos = new Vector3();

        // Render random device types initialization
        System.Random rnd = new System.Random();
        int rndNum = 0;
        int xx_router;

        //For scaling on where the routers will start
        if (network_devices.Count != 1)
            {
            xx_router = (-9 * network_devices.Count);
            }
        else
            {
            xx_router = 0; 
            }
        
        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
            {

            //Routers or extenders (not sure if extenders look different physically)
            objTrans.position = new Vector3(xx_router, 1.5f, 0);
            
            //For some reason it spawns it backwards sometimes
            Instantiate(router, objTrans.position, new Quaternion(0,0,0,0));
            
            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_client_rssi().Count != 0)
                {
                //will help scale how many sta_clients are connected to show they start out connected to eachother.
                if (network_devices[i].get_sta_client_rssi().Count > 1)
                    {
                    objPos.x = xx_router - (network_devices[i].get_sta_client_rssi().Count * 3);
                    }
                else
                    {
                    objPos.x = xx_router;
                    }
                objPos.z = 10;

                //Randomly spawns object behind router
                for (int ii = 0; ii < network_devices[i].get_sta_client_rssi().Count; ii++)
                    {
                    objTrans.position = objPos;
                    rndNum = rnd.Next(1, 4);
                    switch (rndNum)
                        {
                        case 1:
                            Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                            break;
                        case 2:
                            objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                            Instantiate(laptop, objTrans.position, objTrans.rotation);
                            break;
                        case 3:
                            Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                            break;
                        default:
                            Debug.Log("error rendering object");
                            break;
                        }
                    objPos.x += 9;
                    }
                }
            xx_router += 28;
            }

        }

    void Update()
        {
        
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
        
       }
    


