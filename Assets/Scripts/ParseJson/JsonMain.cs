using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class JsonMain : MonoBehaviour
    {

    // Main Objects
    //  - network_devices   = Devices connected to the Router/Extender
    //  - serials           = Router references
    //  - num_devices       = clients[] in eth_clients & sta_clients
    List<Topology> network_devices = new List<Topology>();
    List<string> serials = new List<string>();
    int num_devices = 0;
    LocationsJsonParse location_data;
    string locations_file_path;
    
    // Start is called before the first frame update
    public void Start()
        {
        // 1. Read Topology JSON
        // FirebaseGetTopology();
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
                $"Count: {i}\n" +
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
    public LocationsJsonParse GetLocationData()
        {
        return location_data;
        }
    public string GetLocationsFilePath()
        {
        return locations_file_path;
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