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
        string file_path = Application.dataPath + "/JsonFiles/json2.json";
        string json = File.ReadAllText(file_path);
        JsonParse loaded_data = JsonUtility.FromJson<JsonParse>(json);

        //  - Optional: Print JSON Files
        //PrintJsonParsing(loaded_data);

        // 2. Store devices based on their respective Router/Extender
        OrganizeByRouter(loaded_data);

        // 3. Read Router/Extenders locations JSON file
        file_path = Application.dataPath + "/JsonFiles/locations.json";
        json = File.ReadAllText(file_path);
        LocationsJsonParse location_data = JsonUtility.FromJson<LocationsJsonParse>(json);

        //PrintLocationsJsonParse(location_data);

        // 4. Store device locations by serial #
        StoreRouterLocations(location_data);

        //PrintStoredLocations();
        string serial = "5054494e912ce94f";
        SaveLocation(location_data, file_path, serial, 0, 1, 2);

        // 5. Test
        Debug.Log("# of Devices: " + GetNumDevices());
        Debug.Log("# of Routers: " + GetNumRouters());
    }

    // References Functions in 'Functions.cs'
    void OrganizeByRouter(JsonParse loaded_data)
        {
        Functions temp = new Functions();
        temp.OrganizeByRouter(loaded_data, ref network_devices, ref serials, ref num_devices);
        }

    void StoreRouterLocations(LocationsJsonParse location_data)
        {
        Functions temp = new Functions();
        temp.StoreRouterLocations(location_data, ref network_devices, serials);
        }

    void SaveLocation(LocationsJsonParse location_data, string file_path, string serial, double x, double y, double z)
    {
        int index = serials.BinarySearch(serial);
        location_data.serials[index].x = x;
        location_data.serials[index].y = y;
        location_data.serials[index].z = z;

        string json = JsonUtility.ToJson(location_data);
        File.WriteAllText(file_path, json);
    }


    // Print functions for Debugging Purposes
    void PrintJsonParsing(JsonParse loaded_data)
        {
        Functions temp = new Functions();
        temp.PrintJsonParsing(loaded_data);
        }

    void PrintTopology()
        {
        Functions temp = new Functions();
        temp.PrintTopology(network_devices);
        }

    void PrintLocationsJsonParse(LocationsJsonParse location_data)
        {
        for (int i = 0; i < location_data.serials.Length; ++i)
            {
            Debug.Log(
                $"Count: {i + 1}\n" +
                $"Serial: {location_data.serials[i].serial}\n" +
                $"Location: ({location_data.serials[i].x}, {location_data.serials[i].y}, {location_data.serials[i].z})\n"
            );
            }
        }

    void PrintStoredLocations()
        {
        foreach (var dev in network_devices)
            {
            Debug.Log(
                $"Serial: {dev.get_serial()} | " +
                $"Locations: ({dev.get_location().Item1}, {dev.get_location().Item2}, {dev.get_location().Item3})"
            );
            }
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