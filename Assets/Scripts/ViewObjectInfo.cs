﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ViewObjectInfo : MonoBehaviour
{
    public Text infoText;
    public Text header;
    

    void Start()
        {

        
        // Initialize JsonMain script and start parsing Json
        JsonMain jsonMain = new JsonMain();
        jsonMain.Start();
        List<Topology> network_devices = jsonMain.GetDevices();
        //When the View assistant button is clicked in test_scenee it will load the new scene


        header.text = ChangeScene.ret;

        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
        {
            //For each router or extender it will print the information within the json
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
            {
                if (ChangeScene.ret == network_devices[i].get_serial())
                {
                    infoText.text = "IP: " + network_devices[i].get_mesh_links()[ii].device_info.ip_addr + "\n" +
                        "Serial: " + network_devices[i].get_serial() + "\n" +
                        "Is Master: " + network_devices[i].get_isMaster() + "\n" +
                        "Notes: " + network_devices[i].get_mesh_links()[ii].device_info.notes;
                    Debug.Log(network_devices[i].get_mesh_links()[ii].device_info.notes);
                }
            }

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
            {
                //Prints all of sta_clients info 
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                {
                    if (ChangeScene.ret == network_devices[i].get_sta_clients()[ii].target_mac)
                    {
                        infoText.text = "IP" + network_devices[i].get_sta_clients()[ii].device_info.ip_addr + "\n"
                            + network_devices[i].get_sta_clients()[ii].target_mac + "\n" +
                            "RXPR: " + network_devices[i].get_sta_clients()[ii].rxpr + "\n" +
                            "TXPR: " + network_devices[i].get_sta_clients()[ii].txpr + "\n" +
                            "Notes: " + network_devices[i].get_sta_clients()[ii].device_info.notes;
                    }
                }

            }
            //Checks to see if eth_clients exist
            if (network_devices[i].get_eth_clients().Count != 0)
            {
                //Gets all of eth_clients info
                for (int ii = 0; ii < network_devices[i].get_eth_clients().Count; ii++)
                {
                    if (ChangeScene.ret == network_devices[i].get_eth_clients()[ii].target_mac)
                    {
                        infoText.text = "IP" + network_devices[i].get_eth_clients()[ii].device_info.ip_addr + "\n"
                            + network_devices[i].get_eth_clients()[ii].target_mac + "\n" +
                            "idle: " + network_devices[i].get_eth_clients()[ii].idle + "\n" +
                            "Notes: " + network_devices[i].get_eth_clients()[ii].device_info.notes; ;
                    }
                }

            }

        }
        
    }

}
