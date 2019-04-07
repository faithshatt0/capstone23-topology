using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addName : MonoBehaviour
{
    Text infoText;

    void Awake()
        {
        infoText = GetComponent<Text>();

        }
    void Start()
        {
        // Initialize JsonMain script and start parsing Json
        JsonMain jsonMain = new JsonMain();
        jsonMain.Start();

        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = jsonMain.GetDevices();
        List<string> serials = jsonMain.GetSerials();

        
        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
            {
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
            {
                if (infoText.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                {
                    infoText.text = network_devices[i].get_mesh_links()[ii].device_info.hostname;
                }
            }

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
            {
                //Randomly spawns object behind router
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                {
                    if (infoText.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                    {
                        infoText.text = network_devices[i].get_sta_clients()[ii].device_info.hostname;
                    }
                }

            }
            if (network_devices[i].get_eth_clients().Count != 0)
            {

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
