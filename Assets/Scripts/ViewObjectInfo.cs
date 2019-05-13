using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// ViewObjectInfo.cs
/// This slide looks at what object is on the scene and then shows the information for that slide.
/// 

public class ViewObjectInfo : MonoBehaviour
    {
    //Text fields 
    public Text infoText; 
    public Text header;

    void Start()
        {
        List<Topology> network_devices = spawner.network_devices;
        //When the View assistant button is clicked in test_scenee it will load the new scene
        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
            {
            //For each router or extender it will print the information within the json
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
                {
                if (ChangeScene.ret == network_devices[i].get_serial())
                    {
                    header.text = network_devices[i].get_mesh_links()[ii].device_info.hostname;
                    infoText.text = "- IP: " + network_devices[i].get_mesh_links()[ii].device_info.ip_addr + "\n" +
                        "- Serial: " + network_devices[i].get_serial() + "\n" +
                        "- Is Master: " + network_devices[i].get_isMaster() + "\n" +
                        "- RSSI's Relative to this Device \n" +
                        "                 ----------\n"; 
                        //Routers have multiple RSSIs so it will show all RSSIs
                        for(int x = 0; x < network_devices[i].get_mesh_links()[ii].connected_to.Count; x++)
                            {
                            infoText.text += network_devices[i].get_mesh_links()[ii].connected_to[x].serial + "'s RSSI: " + network_devices[i].get_mesh_links()[ii].connected_to[x].rssi/10 + "\n";
                            }
                    infoText.text += "                 ----------\n";
                        infoText.text += "- Notes: \n" + network_devices[i].get_mesh_links()[ii].device_info.notes;
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
                        header.text = network_devices[i].get_sta_clients()[ii].device_info.hostname;
                        infoText.text = "- IP: " + network_devices[i].get_sta_clients()[ii].device_info.ip_addr + "\n"
                            + "- Target Mac: " + network_devices[i].get_sta_clients()[ii].target_mac + "\n" +
                            "- RSSI: " + network_devices[i].get_sta_clients()[ii].rssi/10 + "\n" + 
                            "- RXPR: " + network_devices[i].get_sta_clients()[ii].rxpr + "\n" +
                            "- TXPR: " + network_devices[i].get_sta_clients()[ii].txpr + "\n" +
                            "- Notes: \n" + network_devices[i].get_sta_clients()[ii].device_info.notes;
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
                        header.text = network_devices[i].get_eth_clients()[ii].device_info.hostname;
                        infoText.text = "- IP: " + network_devices[i].get_eth_clients()[ii].device_info.ip_addr + "\n"
                            + "- Target Mac: " + network_devices[i].get_eth_clients()[ii].target_mac + "\n" +
                            "- idle: " + network_devices[i].get_eth_clients()[ii].idle + "\n" +
                            "- Notes: \n" + network_devices[i].get_eth_clients()[ii].device_info.notes; ;
                        }
                    }
                }
            }
        }
    }   
