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
    public Sprite empty;
    public Sprite ethernet;
    bool isShowing;
    public Text score;
    Sprite prevSprite;
    float RSSI;

    public float average(List<Device> connected_to)
        {
        float average = 0;
        for(int i = 0; i < connected_to.Count; i++)
            {
            average += connected_to[i].rssi;
            }
        Debug.Log(average / connected_to.Count);
        return average/connected_to.Count;
        }

    void Start()
        {
        isShowing = false;
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
            

            for(int ii = 0; ii < network_devices[i].get_eth_clients().Count; ii++)
                {
                if(m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_eth_clients()[ii].target_mac)
                    {
                    m_Image.sprite = ethernet;
                    prevSprite = ethernet;
                    }
                }


            for (int ii = 0; ii < network_devices[i].get_mesh_links().Count; ii++)
            {
                
                if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_serial())
                {
                    float min_Rssi = average(network_devices[i].get_mesh_links()[ii].connected_to)/10f;
                    RSSI = min_Rssi;
                    
                    if (min_Rssi  >= -68f)
                        {
                        m_Image.sprite = good;
                        prevSprite = good;

                        }
                    else if (min_Rssi / 10f <= -69f)
                        {
                        m_Image.sprite = medium;
                        prevSprite = medium;
                        }
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
                //Randomly spawns object behind router
                //Excellent: >= -59; -68 <= Good <= -60; -78 <= Acceptable <= -69; Bad < -78
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count; ii++)
                {
                    if (m_Image.gameObject.transform.parent.parent.name == network_devices[i].get_sta_clients()[ii].target_mac)
                    {
                        if(network_devices[i].get_sta_clients()[ii].rssi/10f  >= -68f)
                            {
                            m_Image.sprite = good;
                            prevSprite = good;
                            RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                            }   
                        else if(network_devices[i].get_sta_clients()[ii].rssi / 10f >= -78f && network_devices[i].get_sta_clients()[ii].rssi / 10f <= -69f)
                            {
                            m_Image.sprite = medium;
                            prevSprite = medium;
                            RSSI = network_devices[i].get_sta_clients()[ii].rssi / 10f;
                            }
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
