using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class connectionStatus : MonoBehaviour
{
    Image m_Image;
    //Set this in the Inspector
    public Sprite good;
    public Sprite medium;
    public Sprite bad;

    void Start()
    {
        //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
        //Fetch the Image from the GameObject
        m_Image = GetComponent<Image>();

        // Initialize JsonMain script and start parsing Json
        JsonMain jsonMain = new JsonMain();
        jsonMain.Start();

        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = jsonMain.GetDevices();
        List<string> serials = jsonMain.GetSerials();

        //gets each router or extender 
        for (int i = 0; i < network_devices.Count; i++)
        {
            /*
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
            {
                if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                {
                    if (network_devices[i].get_mesh_links()[ii].connected_to[ii].rssi / 10f >= -68f)
                    {
                        m_Image.sprite = good;
                    }
                    else if (network_devices[i].get_sta_clients()[ii].rssi / 10f >= -78f && network_devices[i].get_sta_clients()[ii].rssi / 10f <= -69f)
                    {
                        m_Image.sprite = medium;
                    }
                    else
                    {
                        m_Image.sprite = bad;
                    }
                }
            }
            */

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
            {
                //Randomly spawns object behind router
                //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                {
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                    {
                        if(network_devices[i].get_sta_clients()[ii].rssi/10f  >= -68f)
                            {
                            m_Image.sprite = good;
                            }   
                        else if(network_devices[i].get_sta_clients()[ii].rssi / 10f >= -78f && network_devices[i].get_sta_clients()[ii].rssi / 10f <= -69f)
                           {
                            m_Image.sprite = medium;
                           }
                        else
                            {
                            m_Image.sprite = bad;
                            }     
                    }
                }

            }
            

        }

    }

    
}
