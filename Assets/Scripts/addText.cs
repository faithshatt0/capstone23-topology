using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// addText.cs
/// This script is used to add text to each of the informational panels so it displays the information given from the json file
/// 
public class addText : MonoBehaviour
    {
    Text infoText;
    void Awake()
        {
        infoText = GetComponent<Text>();
        }   

    private void Start()
        {
        // Retrieve network_devices and serials from spawner.cs
        List<Topology> network_devices = spawner.network_devices;
        List<string> serials = spawner.serials;

        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
            {
            //For each router or extender it will print the information within the json
            for(int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
                {
                if (infoText.gameObject.transform.parent.parent.parent.name == network_devices[i].get_serial())
                    {
                    infoText.text = "IP" + network_devices[i].get_mesh_links()[ii].device_info.ip_addr + "\n" +
                        "Serial: " + network_devices[i].get_serial() + "\n" +
                        "Is Master: " + network_devices[i].get_isMaster() + "\n";
                    }
                }
            
            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
                {
                //Prints all of sta_clients info 
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                    {
                    if (infoText.gameObject.transform.parent.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                        {
                        infoText.text = "IP" + network_devices[i].get_sta_clients()[ii].device_info.ip_addr + "\n"
                            + network_devices[i].get_sta_clients()[ii].target_mac + "\n" +
                            "RXPR: " + network_devices[i].get_sta_clients()[ii].rxpr + "\n" +
                            "TXPR: " + network_devices[i].get_sta_clients()[ii].txpr;
                        }
                    }

                }
            //Checks to see if eth_clients exist
            if (network_devices[i].get_eth_clients().Count != 0)
                {
                //Gets all of eth_clients info
                for (int ii = 0; ii < network_devices[i].get_eth_clients().Count; ii++)
                    {
                    if (infoText.gameObject.transform.parent.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                        {
                        infoText.text = "IP" + network_devices[i].get_eth_clients()[ii].device_info.ip_addr + "\n"
                            + network_devices[i].get_eth_clients()[ii].target_mac + "\n" +
                            "idle: " + network_devices[i].get_eth_clients()[ii].idle + "\n";   
                        }
                    }

                }

            }
        }
    }
   

