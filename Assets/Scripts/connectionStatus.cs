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
    public Sprite red;
    public Sprite orange;
    public Sprite green;
    public Sprite ethernet;
    public Sprite batteryLow;
    bool isShowing;
    bool isEthClient;
    public Text score;
    Sprite prevSprite;
    float RSSI;

    //ImageType: 1 = RSSI
    // - RSSI will determine Sprite
    //---- Code will be -1
    //ImageType: 2 = Error
    // - Code will determine Sprite
    //---- Example: Code 1 will be battery
    //ImageType: 3 = Color of Device Status
    // - Code will determine Sprite
    //---- 1: Green, 2: Orange, 3: Red


    //For routers there are multiple rssi's so we just find the average
    public float average(List<Device> connected_to)
        {
        float average = 0;
        for (int i = 0; i < connected_to.Count; i++)
            {
            average += connected_to[i].rssi;
            }
        return average / connected_to.Count;
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
            //Ethernet
            for (int ii = 0; ii < network_devices[i].get_eth_clients().Count; ii++)
                {
                //Ethernet has no RSSI so it shows the Ethernet Symbol
                if (network_devices[i].get_eth_clients()[ii].device_info.image_type == 1)
                    {
                    //Finds the same target_macs and shows the ethernet image
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                        {
                        m_Image.sprite = ethernet;
                        prevSprite = ethernet;
                        isEthClient = true;
                        }
                    }

                //Error/Image
                else if (network_devices[i].get_eth_clients()[ii].device_info.image_type == 2)
                    {
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                        {
                        isEthClient = true;
                        switch (network_devices[i].get_eth_clients()[ii].device_info.code)
                            {
                            //Example Image/Error Image Code
                            case 1:
                                m_Image.sprite = batteryLow;
                                prevSprite = batteryLow;
                                break;
                            default:
                                m_Image.sprite = empty;
                                prevSprite = empty;
                                break;
                            }
                        }
                    }

                //Color
                else if (network_devices[i].get_eth_clients()[ii].device_info.image_type == 3)
                    {
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                        {
                        isEthClient = true;
                        switch (network_devices[i].get_eth_clients()[ii].device_info.code)
                            {
                            //Color Code
                            case 1:
                                m_Image.sprite = green;
                                prevSprite = green;
                                break;
                            case 2:
                                m_Image.sprite = orange;
                                prevSprite = orange;
                                break;
                            case 3:
                                m_Image.sprite = red;
                                prevSprite = red;
                                break;

                            default:
                                m_Image.sprite = empty;
                                prevSprite = empty;
                                break;
                            }
                        }
                    }
                }

            //For mesh_links
            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
                {
                //RSSI
                if (network_devices[i].get_mesh_links()[ii].device_info.image_type == 1)
                    {
                    //connects mesh_clients and if the RSSI is within the right range:
                    //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                        {
                        float min_Rssi = average(network_devices[i].get_mesh_links()[ii].connected_to) / 10f; //finds average RSSI
                        RSSI = min_Rssi;
                        //Excellent and Good
                        if (min_Rssi >= -68f)
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

                //Error/Image Code
                else if (network_devices[i].get_mesh_links()[ii].device_info.image_type == 2)
                    {
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                        {
                        float min_Rssi = average(network_devices[i].get_mesh_links()[ii].connected_to) / 10f; //finds average RSSI
                        RSSI = min_Rssi;
                        switch (network_devices[i].get_mesh_links()[ii].device_info.code)
                            {
                            //Example Image/Error Image Code
                            case 1:
                                m_Image.sprite = batteryLow;
                                prevSprite = batteryLow;
                                break;
                            default:
                                m_Image.sprite = empty;
                                prevSprite = empty;
                                break;
                            }
                        }
                    }

                //Color
                else if (network_devices[i].get_mesh_links()[ii].device_info.image_type == 3)
                    {
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                        {
                        float min_Rssi = average(network_devices[i].get_mesh_links()[ii].connected_to) / 10f; //finds average RSSI
                        RSSI = min_Rssi;
                        switch (network_devices[i].get_mesh_links()[ii].device_info.code)
                            {
                            //Color Code
                            case 1:
                                m_Image.sprite = green;
                                prevSprite = green;
                                break;
                            case 2:
                                m_Image.sprite = orange;
                                prevSprite = orange;
                                break;
                            case 3:
                                m_Image.sprite = red;
                                prevSprite = red;
                                break;

                            default:
                                m_Image.sprite = empty;
                                prevSprite = empty;
                                break;
                            }
                        }
                    }
                }

           
            //Sta_clients
            //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
            for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                {
                //RSSI
                if (network_devices[i].get_sta_clients()[ii].device_info.image_type == 1)
                    {
                    //Connects sta_clients to devices
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                        {
                        //RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                        //Excellent and Good
                        if (network_devices[i].get_sta_clients()[ii].rssi / 10f >= -68f)
                            {
                            m_Image.sprite = good;
                            prevSprite = good;
                            RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                            }
                            //Acceptable
                        else if (network_devices[i].get_sta_clients()[ii].rssi / 10f >= -78f && network_devices[i].get_sta_clients()[ii].rssi / 10f <= -69f)
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
                else if (network_devices[i].get_sta_clients()[ii].device_info.image_type == 2)
                    {
                    RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                        {
                        switch (network_devices[i].get_sta_clients()[ii].device_info.code)
                            {
                            //Example Image/Error Image Code
                            case 1:
                                m_Image.sprite = batteryLow;
                                prevSprite = batteryLow;
                                RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                                break;
                            default:
                                m_Image.sprite = empty;
                                prevSprite = empty;
                                RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                                break;
                            }
                        }
                    }

                    //Color
                else if (network_devices[i].get_sta_clients()[ii].device_info.image_type == 3)
                    { 
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                        {
                        RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                        switch (network_devices[i].get_sta_clients()[ii].device_info.code)
                            {
                            //Color Code
                            case 1:
                                m_Image.sprite = green;
                                prevSprite = green;
                                RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                                break;
                            case 2:
                                m_Image.sprite = orange;
                                prevSprite = orange;
                                RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                                break;
                            case 3:
                                m_Image.sprite = red;
                                prevSprite = red;
                                RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                                break;

                            default:
                                m_Image.sprite = empty;
                                prevSprite = empty;
                                RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                                break;
                            }
                        }
                    }
                }
            }
        }

    public void ButtonInteract()
        {
        //To make sure eth_clients don't show anything when the button is pressed
        if (isShowing == true && isEthClient == false)
            {
            m_Image.sprite = prevSprite;
            score.text = "";
            isShowing = false;
            }
        else if (isShowing == false && isEthClient == false)
            {
            m_Image.sprite = empty;
            score.text = RSSI.ToString();
            isShowing = true;
            }
        }
    }