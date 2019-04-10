using System.IO;
using System.Collections;
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
    public GameObject sta;

    // Save to locations.json
    //  - Saving/Writing x, y, z to an object's coordinates
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

        //y iS ALWAys gonna be 1.5f
        // xx_router will be from json file now
        // zz_router will also be from json file


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
            GameObject routers = Instantiate(router, objTrans.position, new Quaternion(0,0,0,0));
            var n = network_devices[i].get_serial();
            routers.transform.name = n;
            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
                {
                //will help scale how many sta_clients are connected to show they start out connected to eachother.
                if (network_devices[i].get_sta_clients().Count > 1)
                    {
                    objPos.x = xx_router - (network_devices[i].get_sta_clients().Count * 3);
                    }
                else
                    {
                    objPos.x = xx_router;
                    }
                objPos.z = 10;

                //Randomly spawns object behind router
                //Debug.Log(network_devices[i].serial + " | " + network_devices[i].get_sta_clients().Count); + network_devices[i].get_eth_clients().Count 
                int counter = network_devices[i].get_eth_clients().Count;
                int iii = 0;
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
            xx_router += 33;
            }
        }

    void Update()
    {
    }

   

}
    


