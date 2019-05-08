using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// addName.cs
/// This script adds the name of each object onto the informational panel --- This is similar to addText.cs since they do the same thing, but add different text
/// This script changes the Header to the hostnames of the object on the informational panel.
public class addName : MonoBehaviour
    {
    Text infoText; //get the textbox within the header

    void Awake()
        {
        infoText = GetComponent<Text>(); //gets the component of the text box
        }
    void Start()
        {
        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = spawner.network_devices;
        List<string> serials = spawner.serials;

        
        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
            {
            //mesh_links --- routers connected to extenders 
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
                {
                if (infoText.gameObject.transform.parent.parent.name == network_devices[i].get_serial()) //finds the names of the serials and changes it
                    {
                    infoText.text = network_devices[i].get_mesh_links()[ii].device_info.hostname;
                    }
                }

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
                {
                //Will find the target mac and connect the name
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                    {
                    if (infoText.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                        {
                        infoText.text = network_devices[i].get_sta_clients()[ii].device_info.hostname;
                        }
                    }

                }

            //Checks for eth_clients to make sure its not empty
            if (network_devices[i].get_eth_clients().Count != 0)
                {
                //Looks for eth_clients target_mac
                for (int ii = 0; ii < network_devices[i].get_eth_clients().Count; ii++)
                    {
                    if (infoText.gameObject.transform.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                        {
                        infoText.text = network_devices[i].get_eth_clients()[ii].device_info.hostname;
                        }
                    }

                }

            }
        }
    }