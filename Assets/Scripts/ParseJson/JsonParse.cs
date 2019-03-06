[System.Serializable]
public class JsonParse
{
    public EthConnection[] eth_clients;    // eth_clients
    public StaConnection[] sta_clients;        // sta_clients
    public MeshLink[] mesh_links;       // mesh_links
}

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

// JSON Objects
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
}

[System.Serializable]
public class MeshLink
{
    public Device[] connected_to;         // connected_to
    public bool isMaster;
    public string serial;
}

[System.Serializable]
public class Sta
{
    /*
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
}

[System.Serializable]
public class Device
{
    public int rssi;
    public string serial;
}
