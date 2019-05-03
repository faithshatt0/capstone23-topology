using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

/// Spawner.cs
/// This script spawns the objects on the board. It uses the data from the parsing json files to spawn the number of objects for it
///

public class spawner : MonoBehaviour
    {
    public GameObject platform;
    public GameObject router;
    public GameObject phone;
    public GameObject laptop;
    public GameObject assistant;
    public Vector3 screenSpace;
    public Vector3 offset;
    public Vector3 real_position;
    public GameObject sta;

    // Main Objects
    //  - network_devices   = Devices connected to the Router/Extender
    //  - serials           = Router references
    //  - num_devices       = clients[] in eth_clients & sta_clients
    public static List<Topology> network_devices = new List<Topology>();
    public static List<string> serials = new List<string>();
    public static int num_devices = 0;
    public static LocationsJsonParse location_data;

    private string topology_json = "";
    private string locations_json = "";
    
    // Start is called once at the beginning of the program
    void Start()
        {
        // 1. Read Topology Data
        //    - Then, Locations Data
        //FirebaseTesting();
        FirebaseGetData();

        /*
        string file_path = Application.dataPath + "/JsonFiles/json2.json";
        string json = File.ReadAllText(file_path);
        JsonParse topology_data = JsonUtility.FromJson<JsonParse>(json);
        
        Debug.Log(json);
        
        OrganizeByRouter(topology_data);
        
        // 3. Read Router/Extenders locations JSON file
        file_path = Application.dataPath + "/JsonFiles/locations.json";
        json = File.ReadAllText(file_path);
        location_data = JsonUtility.FromJson<LocationsJsonParse>(json);

        Debug.Log(json);
        
        // 4. Store device locations by serial #
        StoreRouterLocations(location_data);
        
        SpawnObjects();
        */
        }
    
    
    void FirebaseTesting()
    {
        string folder = "topology";

        Debug.Log("Testing");
        
        RestClient.Get<CapstoneTopology>("https://capstone-topology.firebaseio.com/.json").Then(response =>
        {
            Debug.Log(response);
            topology_json = JsonUtility.ToJson(response.topology);
            locations_json = JsonUtility.ToJson(response.locations);
            Debug.Log(topology_json);
            Debug.Log(locations_json);
        });
    }

    // Firebase Request
    //    - GET
    void FirebaseGetData()
    {
        RestClient.Get<CapstoneTopology>("https://capstone-topology.firebaseio.com/.json").Then(response =>
        {
            Debug.Log("Got Response");
            // 1. Store JSON values into variables
            //    - Format is in JsonParse found in ParseJson/JsonParse.cs
            string topology_json = JsonUtility.ToJson(response.topology);
            string locations_json = JsonUtility.ToJson(response.locations);

            string file_path = Application.dataPath + "/JsonFiles/json2.json";
            string json = File.ReadAllText(file_path);
            Debug.Log("OG: " + json + "\n\nDB: " + topology_json + "\n\n");

            JsonParse topology_data = JsonUtility.FromJson<JsonParse>(json);

            // 3. Store devices based on their respective Router/Extender
            OrganizeByRouter(topology_data);
            
            // 4. Store JSON values into variables
            //    - Format is in LocationsJsonParse found in ParseJson/JsonParse.cs
            file_path = Application.dataPath + "/JsonFiles/locations.json";
            json = File.ReadAllText(file_path);
            Debug.Log("OG: " + json + "\n\nDB: " + locations_json + "\n\n");
            LocationsJsonParse locations_data = JsonUtility.FromJson<LocationsJsonParse>(json);

            // 5. Store device locations by serial #
            StoreRouterLocations(locations_data);
        
            Debug.Log(network_devices.Count);
        
            // 6.
            SpawnObjects();
        });
    }
    
    void FirebaseGetTopology()
    {
        string folder = "topology";

        RestClient.Get<JsonParse>("https://capstone-topology.firebaseio.com/.json").Then(response =>
        {
            // 1. Store JSON values into variables
            //    - Format is in JsonParse found in ParseJson/JsonParse.cs
            topology_json = JsonUtility.ToJson(response);
            FirebaseGetLocations();
        });
    }

    void FirebaseGetLocations()
    {
        string folder = "locations";

        RestClient.Get<FirebaseLocations>("https://capstone-topology.firebaseio.com/" + folder + ".json").Then(response =>
        {
            // 2. Read in Locations of Router/Extenders
            //    - Note: Getting the locations is dependent on getting the topology first.
            locations_json = JsonUtility.ToJson(response);
            location_data = response.locations;
            
            Debug.Log(locations_json);
            
            StoreData();
        });
    }

    void StoreData()
    {
        Debug.Log(topology_json);
        Debug.Log(locations_json);
        JsonParse topology_data = JsonUtility.FromJson<JsonParse>(topology_json);

        // 3. Store devices based on their respective Router/Extender
        OrganizeByRouter(topology_data);
            
        // 4. Store JSON values into variables
        //    - Format is in LocationsJsonParse found in ParseJson/JsonParse.cs
        LocationsJsonParse locations_data = JsonUtility.FromJson<LocationsJsonParse>(locations_json);

        // 5. Store device locations by serial #
        StoreRouterLocations(locations_data);
        
        Debug.Log(network_devices.Count);
        
        // 6.
        SpawnObjects();
    }
    
    // References Functions in 'Functions.cs'
    void OrganizeByRouter(JsonParse loaded_data)
    {
        Functions.OrganizeByRouter(loaded_data, ref network_devices, ref serials, ref num_devices);
    }

    void StoreRouterLocations(LocationsJsonParse location_data)
    {
        Functions.StoreRouterLocations(location_data, ref network_devices, serials);
    }

    void SpawnObjects()
    {
        Debug.Log("Spawning Objects!");
        // Template transform variable for GameObject positioning and rotation
        Transform objTrans = new GameObject().transform;

        // Render scene
        objTrans.position = new Vector3(0, -0.5f, 0);
        objTrans.rotation = Quaternion.Euler(0, 180, 0);
        Instantiate(platform, objTrans.position, objTrans.rotation);
        Vector3 objPos = new Vector3();

        // Render random device types initialization
        System.Random rnd = new System.Random();
        var rndNum = 0;

        Debug.Log("Network Devices Size: " + network_devices.Count);

        //gets each router or extender
        for (int i = 0; i < network_devices.Count; i++)
            {
                Debug.Log("Sta Client Size " + i + ": " + network_devices[i].get_sta_clients().Count);
            //Routers or extenders (not sure if extenders look different physically)
            //To get location
            //location_data.serials[i].x;
            //location_data.serials[i].y;
            //location_data.serials[i].z;
            //just plug it into objTrans. you don't have to equal bc it will be in the same order and same size
            objTrans.position = new Vector3(location_data.serials[i].x, location_data.serials[i].y, location_data.serials[i].z);
            float xx_router = location_data.serials[i].x;
            //For some reason it spawns it backwards sometimes
            ///
            ///This is where you will have to have an if statement from reading the json on what type of router to spawn!
            ///To do that you will have to create a copy of the GameObject Router in Unity itself and attach a different skin to it
            ///This will give that GameObject the same heirarchy and scripts so it can essentially do the same thing as each router
            ///

            GameObject routers = Instantiate(router, objTrans.position, new Quaternion(0,0,0,0));
            var n = network_devices[i].get_serial();
            routers.transform.name = n;

            //if there are no sta_clients it will skip and save time
            if (network_devices[i].get_sta_clients().Count != 0)
                {
                //will help scale how many sta_clients are connected to show they start out connected to eachother.
                if (network_devices[i].get_sta_clients().Count + network_devices[i].get_eth_clients().Count > 1)
                    {
                    objPos.x = xx_router - (network_devices[i].get_sta_clients().Count + network_devices[i].get_eth_clients().Count * 7);
                    }
                else
                    {
                    objPos.x = xx_router;
                    }
                objPos.z = location_data.serials[i].z + 10;


                int counter = network_devices[i].get_eth_clients().Count; //gets the count of eth_clients
                int iii = 0; //remembers the index for the eth_clients

                //Spawning eth_clients and sta_clients --- sta_clients are first
                for (int ii = 0; ii < network_devices[i].get_sta_clients().Count + network_devices[i].get_eth_clients().Count; ii++)
                    {
                    objTrans.position = objPos;

                    //if there are eth clients it will put the eth clients after the sta_clients
                    if (counter > 0 && ii >= network_devices[i].get_sta_clients().Count)
                        {
                        //Phone, Android
                        if (network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("phone") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("android"))
                            {
                            sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                            sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                            counter--;
                            iii++;
                            }
                        //Laptop, Mac, Windows
                        else if(network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("laptop") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("mac") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("windows"))
                            {
                            objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                            sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                            sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                            counter--;
                            iii++;
                            }
                        //Assistant, Alexa, Google
                        else if(network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("assistant") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("alexa") || network_devices[i].get_eth_clients()[iii].device_info.hostname.ToLower().Contains("google"))
                            {
                            sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                            sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                            counter--;
                            iii++;
                            }
                        //If it isnt any of these names it will render a random object
                        else
                            {
                            rndNum = rnd.Next(1, 4);
                            switch (rndNum)
                                {
                                case 1:
                                    sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                                    sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                                    counter--;
                                    iii++;
                                    break;
                                case 2:
                                    objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                                    sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                                    sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                                    counter--;
                                    iii++;
                                    break;
                                case 3:
                                    sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                                    sta.transform.name = network_devices[i].get_eth_clients()[iii].target_mac;
                                    counter--;
                                    iii++;
                                    break;
                                default:
                                    Debug.Log("error rendering object");
                                    break;
                                }
                            }
                        }
                    //if it is a sta_client
                    else
                        {
                        //Phone, Android
                        if(network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("phone") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("android"))
                            {
                            sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                            sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                            }
                        //Laptop, Mac, Windows
                        else if(network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("laptop") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("mac"))
                            {
                            objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                            sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                            sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                            }
                        //Assistant, Alexa, Google
                        else if (network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("assistant") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("home") || network_devices[i].get_sta_clients()[ii].device_info.hostname.ToLower().Contains("alexa"))
                            {
                            sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                            sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                            }
                        //if it isnt any of names it will render a random object
                        else
                            {
                            rndNum = rnd.Next(1, 4);
                            switch (rndNum)
                                {
                                case 1:
                                    sta = Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                                    sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                                    break;
                                case 2:
                                    objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                                    sta = Instantiate(laptop, objTrans.position, objTrans.rotation);
                                    sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                                    break;
                                case 3:
                                    sta = Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                                    sta.transform.name = network_devices[i].get_sta_clients()[ii].target_mac;
                                    break;
                                default:
                                    Debug.Log("error rendering object");
                                    break;
                                }
                            }
                        }
                    objPos.x += 9;
                    }
                }
            }
    }
}
