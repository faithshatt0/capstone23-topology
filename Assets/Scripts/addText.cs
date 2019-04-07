using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addText : MonoBehaviour
{
    Text infoText;
    GameObject myParentObject;

    void Awake()
        {
        infoText = GetComponent<Text>();
        }
    private void Start()
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
            if(infoText.gameObject.transform.parent.parent.parent.name == network_devices[i].get_serial())
                {
                infoText.text = "IP Address: when get it" + "\n" + 
                    "Serial: " + network_devices[i].get_serial() + "\n" +
                    "Is Master: " + network_devices[i].get_isMaster();
                }

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_client_rssi().Count != 0)
                {
    
                //Randomly spawns object behind router
                for (int ii = 0; ii < network_devices[i].get_sta_client_rssi().Count; ii++)
                    {
                    if (infoText.gameObject.transform.parent.parent.parent.name == network_devices[i].get_sta_client_target_mac()[ii])
                    {
                        infoText.text = "IP Address: when get it" + "\n"
                            + network_devices[i].get_sta_client_target_mac()[ii] + "\n" +
                            "RXPR: " + network_devices[i].get_sta_client_rxpr()[ii] + "\n" +
                            "TXPR: " + network_devices[i].get_sta_client_txpr()[ii];
                    }
                }
                }
           
            }

        

    }
   

}
