/*
 * File used when calling 
 *  JsonUtility.FromJson<>(json_file) in JsonMain.cs
 *  to store the JSON values.
 */

[System.Serializable]
public class JsonParse
    {
    public EthConnection[] eth_clients;    // eth_clients
    public StaConnection[] sta_clients;        // sta_clients
    public MeshLink[] mesh_links;       // mesh_links
    }

// JSON Connections
[System.Serializable]
public class EthConnection : Connection<Eth> { }

[System.Serializable]
public class StaConnection : Connection<Sta> { }

[System.Serializable]
public class Connection<T>
    {
    public T[] clients;
    public string serial;
    }

// JSON Objects w/in 'Connection'
[System.Serializable]
public class Eth
    {
    /*
      Example Values      
        'idle': '9.10',
        'target_mac': '00:40:ad:91:be:a0'
    */
    public string idle;         // Made into string to store exact numbers = "9.10" vs. if double -> value = 9.1000000012
    public string target_mac;
    public string hostname;
    public string IP_Address;
    public int ImageType;
    public int Code;
    public string notes;
    }

[System.Serializable]
public class MeshLink
    {
    public Device[] connected_to;
    public bool isMaster;
    public string serial;
    public string hostname;
    public string IP_Address;
    public int ImageType;
    public int Code;
    public string notes;
}

[System.Serializable]
public class Sta
    {
    /*`
     Example Values
        'rssi': -300,
        'rxpr': 130,
        'target_mac': 'd8:31:34:00:bd:8f',
        'txpr': 144
    */
    public int rssi;
    public int rxpr;
    public string target_mac;
    public int txpr;
    public string hostname;
    public string IP_Address;
    public int ImageType;
    public int Code;
    public string notes;
}

[System.Serializable]
public class Device
    {
    public int rssi;
    public string serial;
    }

// For: locations.json
[System.Serializable]
public class LocationsJsonParse
{
    public Serial[] serials;
}

[System.Serializable]
public class Serial
{
    public string serial;
    public float x;
    public float y;
    public float z;
}

// Used for Firebase Parsing
[System.Serializable]
public class CapstoneTopology
{
    public LocationsJsonParse locations;
    public JsonParse topology;
}

// Used for Firebase Parsing
[System.Serializable]
public class FirebaseLocations
{
    public LocationsJsonParse locations;
}