using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonMain : MonoBehaviour
    {

    // Main Objects
    //  - network_devices   = Devices connected to the Router/Extender
    //  - serials           = Router references
    //  - num_devices       = clients[] in eth_clients & sta_clients
    List<Topology> network_devices = new List<Topology>();
    List<string> serials = new List<string>();
    int num_devices = 0;

    // Start is called before the first frame update
    public void Start()
        {
        // Algorithm:
        //  1. Use JsonUtility to store JSON values in objects -> arrays
        //  2. Store devices based on their respective Router/Extender
        //  3. Test data

        /* Capstone */

        // 1. Read JSON file
        string file = Application.dataPath + "/JsonFiles/new_json.json";
        string json = File.ReadAllText(file);
        JsonParse loaded_data = JsonUtility.FromJson<JsonParse>(json);

        //  - Optional: Print JSON Files
        //PrintJsonParsing(loaded_data);

        // 2. Store devices based on their respective Router/Extender
        StartParse(loaded_data);
        
        // 3. Test data
        //Debug.Log("# of Devices: " + num_devices);
    }

    // References Functions in 'Functions.cs'
    void StartParse(JsonParse loaded_data)
        {
        Functions temp = new Functions();
        temp.OrganizeByRouter(loaded_data, ref network_devices, ref serials, ref num_devices);
        }
    void PrintTopology(JsonParse loaded_data)
        {
        Functions temp = new Functions();
        temp.PrintTopology(loaded_data);
        }

    // Allows public access to the network_devices and serial Lists
    public List<Topology> GetDevices()
        {
        return network_devices;
        }
    public List<string> GetSerials ()
        {
        return serials;
        }

    // Indicates # of devices in the Topology
    //  - Routers/Extenders
    //  - # of Devices connected to the Routers/Extenders
    int GetNumRouters()
        {
        return serials.Count;
        }
    int GetNumDevices()
        {
        return num_devices;
        }

    // Update is called once per frame
    void Update()
        {

        }
}