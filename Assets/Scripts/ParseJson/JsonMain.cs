using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonMain
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
        // Algorithm:
        //  1. Use JsonUtility to store JSON values in objects -> arrays
        //  2. Store devices based on their respective Router/Extender
        //  3. Test data

        /* Capstone */

        // 1. Read JSON file
        //string file_path = Application.dataPath + "/JsonFiles/json2.json";
        //string json = File.ReadAllText(file_path);

        string json = "{\n  \"eth_clients\": [\n    {\n      \"clients\": [\n        {\n          \"idle\": \"9.10\",\n          \"target_mac\": \"00:40:ad:91:be:a0\",\n          \"hostname\": \"Eth1 Mac\",\n          \"IP_Address\": \"192.822.3.4\"\n        }\n      ],\n      \"serial\": \"5054494e912ce94f\"\n    },\n    {\n      \"clients\": [],\n      \"serial\": \"aw2311813001257\"\n    },\n    {\n      \"clients\": [],\n      \"serial\": \"aw2311813005151\"\n    }\n  ],\n\n  \"mesh_links\": [\n    {\n      \"connected_to\": [\n        {\n          \"rssi\": -660,\n          \"serial\": \"aw2311813001257\"\n        },\n        {\n          \"rssi\": -660,\n          \"serial\": \"aw2311813005151\"\n        }\n      ],\n\n      \"isMaster\": \"True\",\n      \"serial\": \"5054494e912ce94f\",\n      \"hostname\": \"Home Router\",\n      \"IP_Address\": \"192.223.3.4\"\n    },\n    {\n      \"connected_to\": [\n        {\n          \"rssi\": -546,\n          \"serial\": \"5054494e912ce94f\"\n        },\n        {\n          \"rssi\": -546,\n          \"serial\": \"aw2311813005151\"\n        }\n      ],\n\n      \"isMaster\": \"False\",\n      \"serial\": \"aw2311813001257\",\n      \"hostname\": \"Mesh2 Extender\",\n      \"IP_Address\": \"192.224.3.4\"\n    },\n    {\n      \"connected_to\": [\n        {\n          \"rssi\": -570,\n          \"serial\": \"5054494e912ce94f\"\n        },\n        {\n          \"rssi\": -570,\n          \"serial\": \"aw2311813001257\"\n        }\n      ],\n\n      \"isMaster\": \"False\",\n      \"serial\": \"aw2311813005151\",\n      \"hostname\": \"Mesh3 Extender\",\n      \"IP_Address\": \"192.225.3.4\"\n    }\n  ],\n\n  \"sta_clients\": [\n    {\n      \"clients\": [\n        {\n          \"rssi\": -850,\n          \"rxpr\": 0,\n          \"target_mac\": \"00:20:00:be:79:e2\",\n          \"txpr\": 0,\n          \"hostname\": \"Sta1 iPhone\",\n          \"IP_Address\": \"192.222.3.4\"\n        },\n        {\n          \"rssi\": -500,\n          \"rxpr\": 144,\n          \"target_mac\": \"b0:05:94:40:23:47\",\n          \"txpr\": 144,\n          \"hostname\": \"Sta2 Laptop\",\n          \"IP_Address\": \"192.222.3.4\"\n        },\n        {\n          \"rssi\": -550,\n          \"rxpr\": 866,\n          \"target_mac\": \"38:89:2c:1e:40:ff\",\n          \"txpr\": 866,\n          \"hostname\": \"Sta3 Alexa\",\n          \"IP_Address\": \"192.222.3.4\"\n        }\n      ],\n\n      \"serial\": \"5054494e912ce94f\"\n    },\n    {\n      \"clients\": [\n        {\n          \"rssi\": -505,\n          \"rxpr\": 65,\n          \"target_mac\": \"6c:c2:17:4f:c1:81\",\n          \"txpr\": 1,\n          \"hostname\": \"Sta4 Lair\",\n          \"IP_Address\": \"192.222.3.4\"\n        }\n      ],\n\n      \"serial\": \"aw2311813001257\"\n    },\n    {\n      \"clients\": [\n        {\n          \"rssi\": -655,\n          \"rxpr\": 11,\n          \"target_mac\": \"50:a6:7f:d9:10:5a\",\n          \"txpr\": 1,\n          \"hostname\": \"Sta5 Google Home\",\n          \"IP_Address\": \"192.222.3.4\"\n        },\n\n        {\n          \"rssi\": -540,\n          \"rxpr\": 130,\n          \"target_mac\": \"08:05:81:7e:e9:03\",\n          \"txpr\": 144,\n          \"hostname\": \"Sta6 Laptop\",\n          \"IP_Address\": \"192.222.3.4\"\n        }\n      ],\n\n      \"serial\": \"aw2311813005151\"\n    }\n  ]\n}\n";
        JsonParse loaded_data = JsonUtility.FromJson<JsonParse>(json);

        //  - Optional: Print JSON Files
        //PrintJsonParsing(loaded_data);

        // 2. Store devices based on their respective Router/Extender
        OrganizeByRouter(loaded_data);

        // 3. Read Router/Extenders locations JSON file
        //file_path = Application.dataPath + "/JsonFiles/locations.json";
        //json = File.ReadAllText(file_path);
        json = "{\"serials\":[{\"serial\":\"5054494e912ce94f\",\"x\":-30,\"y\":1.5,\"z\":0},{\"serial\":\"aw2311813001257\",\"x\":0,\"y\":1.5,\"z\":0},{\"serial\":\"aw2311813005151\",\"x\":30,\"y\":1.5,\"z\":0}]}";
        location_data = JsonUtility.FromJson<LocationsJsonParse>(json);
        //locations_file_path = file_path;

        //PrintLocationsJsonParse(location_data);

        // 4. Store device locations by serial #
        StoreRouterLocations(location_data);

        //PrintStoredLocations();

        // 5. Test
        //Debug.Log("# of Devices: " + GetNumDevices());
        //Debug.Log("# of Routers: " + GetNumRouters());
    }

    // Used to store hard coded objects
    void StoreHCObjects()
    {
        return;
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