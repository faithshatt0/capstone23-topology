using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// connectionStatus.cs
/// This script will change the button above the devices to show their connection status. If the button is clicked it will then show the number.
/// The connection will show again if clicked again.
/// 

public class connectionStatus : MonoBehaviour
    {
    Image m_Image;
    //Set this in the Inspector
    public Sprite good;
    public Sprite medium;
    public Sprite bad;
    public Sprite empty;
    public Sprite ethernet;
    bool isShowing;
    public Text score;
    Sprite prevSprite;
    float RSSI;

    //For routers there are multiple rssi's so we just find the average
    public float average(List<Device> connected_to)
        {
        float average = 0;
        for(int i = 0; i < connected_to.Count; i++)
            {
            average += connected_to[i].rssi;
            }
        return average/connected_to.Count;
        }

    void Start()
        {
        isShowing = false; //is the text showing 

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
            for(int ii = 0; ii < network_devices[i].get_eth_clients().Count; ii++)
                {
                //Finds the same target_macs and shows the ethernet image
                if(m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                    {
                    m_Image.sprite = ethernet;
                    prevSprite = ethernet;
                    }
                }

            //For mesh_links
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
                {
                //connects mesh_clients and if the RSSI is within the right range:
                    //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
                if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                    {
                    float min_Rssi = average(network_devices[i].get_mesh_links()[ii].connected_to)/10f; //finds average RSSI
                    RSSI = min_Rssi;
                    //Excellent and Good
                    if (min_Rssi  >= -68f)
                        {
                        m_Image.sprite = good;
                        prevSprite = good;
                        }
                    //Acceptable
                    else if (min_Rssi / 10f <= -69f)
                        {
                        m_Image.sprite = medium;
                        prevSprite = medium;
                        }
                    //Bad
                    else
                        {
                        m_Image.sprite = bad;
                        prevSprite = bad;
                        }
                    }
                }
            
            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
                {
                //Sta_clients
                //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                    {
                    //Connects sta_clients to devices
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                        {
                        //Excellent and Good
                        if(network_devices[i].get_sta_clients()[ii].rssi/10f  >= -68f)
                            {
                            m_Image.sprite = good;
                            prevSprite = good;
                            RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                            }   
                        //Acceptable
                        else if(network_devices[i].get_sta_clients()[ii].rssi / 10f >= -78f && network_devices[i].get_sta_clients()[ii].rssi / 10f <= -69f)
                            {
                            m_Image.sprite = medium;
                            prevSprite = medium;
                            RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                            }
                        //Bad
                        else
                            {
                            m_Image.sprite = bad;
                            prevSprite = bad;
                            RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                            }     
                        }
                    }
                }
            }
        }

    //Once the button is chosen it will change what the Sprite is showing
    public void ButtonInteract()
        {
        if(isShowing == true && prevSprite != ethernet)
            {
            m_Image.sprite = prevSprite;
            score.text = "";
            isShowing = false;
            }
        else if(isShowing == false && prevSprite != ethernet)
            {
            m_Image.sprite = empty;
            score.text = RSSI.ToString();
            isShowing = true;
            }
           
        }

    


}
