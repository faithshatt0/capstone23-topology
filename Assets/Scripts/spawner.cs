using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Spawner.cs
/// This script spawns the objects on the board. It uses the data from the parsing json files to spawn the number of objects for it
///

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
    public GameObject sta;
    LocationsJsonParse location_data;

    List<string> serials = new List<string>();
    string locations_file_path;

    // Start is called once at the beginning of the program
    void Start()
        {
        // Initialize JsonMain script and start parsing Json
        JsonMain jsonMain = new JsonMain();
        jsonMain.Start();

        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = jsonMain.GetDevices();
        serials = jsonMain.GetSerials();
        location_data = jsonMain.GetLocationData();
        locations_file_path = jsonMain.GetLocationsFilePath();

        // Template transform variable for GameObject positioning and rotation
        Transform objTrans = new GameObject().transform;

        // Render scene
        objTrans.position = new Vector3(0, -0.5f, 0);
        objTrans.rotation = Quaternion.Euler(0, 180, 0);
        Instantiate(platform, objTrans.position, objTrans.rotation);
        Vector3 objPos = new Vector3();

        // Render random device types initialization
        System.Random rnd = new System.Random();
        var rndNum = 0;


        //gets each router or extender
        for (int i = 0; i < network_devices.Count; i++)
            {
            //Routers or extenders (not sure if extenders look different physically)
            //To get location
            //location_data.serials[i].x;
            //location_data.serials[i].y;
            //location_data.serials[i].z;
            //just plug it into objTrans. you don't have to equal bc it will be in the same order and same size
            objTrans.position = new Vector3(location_data.serials[i].x, location_data.serials[i].y, location_data.serials[i].z);
            float xx_router = location_data.serials[i].x;
            //For some reason it spawns it backwards sometimes
            ///
            ///This is where you will have to have an if statement from reading the json on what type of router to spawn!
            ///To do that you will have to create a copy of the GameObject Router in Unity itself and attach a different skin to it
            ///This will give that GameObject the same heirarchy and scripts so it can essentially do the same thing as each router
            ///

            GameObject routers = Instantiate(router, objTrans.position, new Quaternion(0,0,0,0));
            var n = network_devices[i].get_serial();
            routers.transform.name = n;

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
                {
                //will help scale how many sta_clients are connected to show they start out connected to eachother.
                if (network_devices[i].get_sta_clients().Count + network_devices[i].get_eth_clients().Count > 1)
                    {
                    objPos.x = xx_router - (network_devices[i].get_sta_clients().Count + network_devices[i].get_eth_clients().Count * 7);
                    }
                else
                    {
                    objPos.x = xx_router;
                    }
                objPos.z = location_data.serials[i].z + 10;


                int counter = network_devices[i].get_eth_clients().Count; //gets the count of eth_clients
                int iii = 0; //remembers the index for the eth_clients

                //Spawning eth_clients and sta_clients --- sta_clients are first
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count + network_devices[i].get_eth_clients().Count; ii++)
                    {
                    objTrans.position = objPos;

                    //if there are eth clients it will put the eth clients after the sta_clients
                    if (counter > 0 && ii >= network_devices[i].get_sta_clients().Count)
                        {
                        //Phone, Android
                        if (network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("phone") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("android"))
                            {
                            sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                            sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                            counter--;
                            iii++;
                            }
                        //Laptop, Mac, Windows
                        else if(network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("laptop") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("mac") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("windows"))
                            {
                            objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                            sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                            sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                            counter--;
                            iii++;
                            }
                        //Assistant, Alexa, Google
                        else if(network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("assistant") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("alexa") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("google"))
                            {
                            sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                            sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                            counter--;
                            iii++;
                            }
                        //If it isnt any of these names it will render a random object
                        else
                            {
                            rndNum = rnd.Next(1, 4);
                            switch (rndNum)
                                {
                                case 1:
                                    sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                                    sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                                    counter--;
                                    iii++;
                                    break;
                                case 2:
                                    objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                                    sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                                    sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                                    counter--;
                                    iii++;
                                    break;
                                case 3:
                                    sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                                    sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                                    counter--;
                                    iii++;
                                    break;
                                default:
                                    Debug.Log("error rendering object");
                                    break;
                                }
                            }
                        }
                    //if it is a sta_client
                    else
                        {
                        //Phone, Android
                        if(network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("phone") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("android"))
                            {
                            sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                            sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                            }
                        //Laptop, Mac, Windows
                        else if(network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("laptop") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("mac"))
                            {
                            objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                            sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                            sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                            }
                        //Assistant, Alexa, Google
                        else if (network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("assistant") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("home") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("alexa"))
                            {
                            sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                            sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                            }
                        //if it isnt any of names it will render a random object
                        else
                            {
                            rndNum = rnd.Next(1, 4);
                            switch (rndNum)
                                {
                                case 1:
                                    sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                                    sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                                    break;
                                case 2:
                                    objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                                    sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                                    sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                                    break;
                                case 3:
                                    sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                                    sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                                    break;
                                default:
                                    Debug.Log("error rendering object");
                                    break;
                                }
                            }
                        }
                    objPos.x += 9;
                    }
                }
            }
        }
    }
