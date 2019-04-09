using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getRssiNumber : MonoBehaviour
{
    Image m_Image;
    //Set this in the Inspector
    public Sprite empty;
    public Text score;
    public bool isShowing;

    public float min_rssi(List<Device> connected_to)
    {
        int min = 1000000000;
        for (int i = 0; i < connected_to.Count; i++)
        {
            if (min < connected_to[i].rssi)
            {
                min = connected_to[i].rssi;
            }
        }
        return min / 10f;
    }

    // Start is called before the first frame update
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
        }

    // Update is called once per frame
    void Update()
        {
        
        }
    

    
}
