using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;       // UnityWebRequest

/*
 * Purpose: Grab JSON information from the raw json file on our Github repo.
 */

public class GetJsonFromWeb : MonoBehaviour
{
    // Main Objects
    //  - network_devices   = Devices connected to the Router/Extender
    //  - serials           = Router references
    //  - num_devices       = clients[] in eth_clients & sta_clients
    List<Topology> network_devices = new List<Topology>();
    List<string> serials = new List<string>();
    int num_devices = 0;

    // Json Info
    JsonParse topology = new JsonParse();
    LocationsJsonParse router_locations = new LocationsJsonParse();

    // URLs:
    //  - Topology          = eth_clients, mesh_links, sta_clients
    //  - Router Locations  = Router position(x, y, z) indicated by serial #
    const string topology_URL = "https://raw.githubusercontent.com/faithshatt0/capstone23-topology/master/Assets/JsonFiles/json2.json";
    const string router_location_URL = "https://raw.githubusercontent.com/faithshatt0/capstone23-topology/master/Assets/JsonFiles/locations.json";

    public void Start()
    {
        StartCoroutine(GetTopology(topology_URL));
    }

    // Web Requests:
    //  - Topology
    //  - Router Locations
    IEnumerator GetTopology(string uri)
    {
        using (UnityWebRequest web_request = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            while (!web_request.isDone)
            {
                //Debug.Log("Download Stat: " + web_request.downloadProgress);
                yield return web_request.SendWebRequest();
            }

            if (string.IsNullOrEmpty(web_request.error))
            {
                string topology_json = web_request.downloadHandler.text;
                topology = JsonUtility.FromJson<JsonParse>(topology_json);

                // 1. Store devices based on their respective Router/Extender
                OrganizeByRouter(topology);

                PrintTopology();


                // 2. After setting up devices, store locations
                StartCoroutine(GetRouterLocations(router_location_URL));

                Debug.Log("Topology done!");
                Debug.Log(topology_json);
            }
            else
            {
                Debug.Log("Error occurred when trying to read: GetTopology()");
            }
        }
    }

    IEnumerator GetRouterLocations(string uri)
    {
        using (UnityWebRequest web_request = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            while (!web_request.isDone)
            {
                //Debug.Log("Download Stat: " + web_request.downloadProgress);
                yield return web_request.SendWebRequest();
            }

            if (string.IsNullOrEmpty(web_request.error))
            {
                string router_location_json = web_request.downloadHandler.text;
                router_locations = JsonUtility.FromJson<LocationsJsonParse>(router_location_json);

                // 3. Store device locations by serial #
                StoreRouterLocations(router_locations);

                Debug.Log("Locations done!");
                Debug.Log(router_location_json);

                // 4. Pass data off to spawner.cs
            }
            else
            {
                Debug.Log("Error occurred when trying to read: GetRouterLocations()");
            }
        }
    }

    // References Functions in 'Functions.cs'
    void OrganizeByRouter(JsonParse topology_data)
    {
        Functions temp = new Functions();
        temp.OrganizeByRouter(topology_data, ref network_devices, ref serials, ref num_devices);
    }

    void StoreRouterLocations(LocationsJsonParse location_data)
    {
        Functions temp = new Functions();
        temp.StoreRouterLocations(location_data, ref network_devices, serials);
    }

    void PrintTopology()
    {
        Functions temp = new Functions();
        temp.PrintTopology(network_devices);
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

    // Getters
    public List<Topology> GetNetworkDevices()
    {
        return network_devices;
    }
}
