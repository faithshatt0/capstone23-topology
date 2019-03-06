using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseJsonAj : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Algorithm:
        //  1. Use JsonUtility to store JSON values in objects -> arrays
        //  2. Store devices based on their respective Router/Extender
        //  3. Test data

        /* Capstone */

        // 1. Read JSON file
        string file = Application.dataPath + "/JsonFiles/json2.json";
        string json = File.ReadAllText(file);
        JsonParse loaded_data = JsonUtility.FromJson<JsonParse>(json);

        //  - Optional: Print JSON File
        //PrintTopology(loaded_data);

        // Main Objects
        //  - network_devices   = Devices connected to the Router/Extender
        //  - serials           = Router references
        List<Topology> network_devices;
        List<string> serials;

        network_devices = new List<Topology>();
        serials = new List<string>();

        // 2. Store devices based on their respective Router/Extender
        StartParse(loaded_data, ref network_devices, ref serials);

        // 3. Test data
        Debug.Log("Testing Class");
        for (int i = 0; i < network_devices.Count; i++)
        {
        Debug.Log(network_devices[i].get_serial() + "\n Eth_client: " + network_devices[i].get_eth_client_idle() + "  " + network_devices[i].get_eth_client_target_mac()
            + "\n isMaster: " + network_devices[i].get_isMaster() + "\n IS CONNECTED TO MESH_LINKS: " + network_devices[i].print_mesh_link_cto_rssi()
            + " " +network_devices[i].print_mesh_link_cto_serial() + "\n Sta_clients: " + network_devices[i].print_sta_client_rssi() + " " + network_devices[i].print_sta_client_rxpr() + " " +
            network_devices[i].print_sta_client_target_mac() + " " + network_devices[i].print_sta_client_txpr());
        }
    }

    // References Functions in 'Functions.cs'
    void StartParse(JsonParse loaded_data, ref List<Topology> network_devices, ref List<string> serials)
    {
        Functions temp = new Functions();
        temp.parse_json(loaded_data, ref network_devices, ref serials);
    }

    void PrintTopology(JsonParse loaded_data)
    {
        Functions temp = new Functions();
        temp.print_topology(loaded_data);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
