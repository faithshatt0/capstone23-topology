using System.Collections.Generic;

public class DeviceInfo
    {
    public string hostname;
    public string ip_addr;
    public Tuple<int, int, int> location;

    public DeviceInfo() 
        {
        hostname = "";
        ip_addr = "";
        location = (0, 0, 0);
        }
    }

public class EthClients
    {
    public DeviceInfo device_info;
    public string serial;           // Router/Extender serial #
    public string idle;
    public string target_mac;

    public EthClient()
        {
        serial = "";
        idle = "";
        target_mac = "";
        }
    }

public class MeshLinks
    {
    public DeviceInfo device_info;
    public List<Device> connected_to;

    public MeshLink() 
        {
        serial = "";
        rssi = 0;
        }
    }

public class StaClients
    {
    public DeviceInfo device_info;
    public int rssi;              // rssi - Signal strength for the device router or extender is connected to 
    public int rxpr;              // rxpr - for connection (what is rxpr)
    public string target_mac;     // tmac - target_mac for connection (what is target_mac)
    public int txpr;              // txpr - for connection (what is txpr)

    public StaClient() 
        {
            rssi = 0;
            rxpr = 0;
            target_mac = "";
            txpr = 0;
        }
    }

//Object class//
public class Topology
    {
    //Data Values//
    //  - Router/Extender devices are connected to
    public bool isMaster;                           // Router?
    public string serial;                           // Router/Extender serial #
    //  - eth_clients
    private List<EthClients> eth_clients;

    //  - mesh_links
    private List<MeshLinks> mesh_links;

    //  - sta_clients
    private List<StaClients> sta_clients;

    // ------------------------------ Constructor -------------------------
    public Topology(List<EthClients> temp)
        {
        this.eth_client = temps;
        }

    // ------------------------------ Setter ------------------------------
    public void set_isMaster(bool isMaster)
        {
        this.isMaster = isMaster;
        }

    // ------------------------------ Add ------------------------------
    //  - Mesh Link
    public void add_mesh_links(MeshLinks temp) 
        {
        mesh_links.Add(temp);
        }
    
    //  - Sta Client
    public void add_sta_clients_rssi(int rssi)
        {
        sta_clients.rssi.Add(rssi);
        }   
    public void add_sta_clients_rxpr(int rxpr)
        {
        sta_clients.rxpr.Add(rxpr);
        }
    public void add_sta_clients_target_mac(string target_mac)
        {
        sta_clients.target_mac.Add(target_mac);
        }
    public void add_sta_clients_txpr(int txpr)
        {
        sta_clients.txpr.Add(txpr);
        }

    // ------------------------------ Getters ------------------------------
    public string get_serial()
        {
        return serial;
        }
    public List<string> get_eth_client_idle(s)
        {
        return eth_client_idles;
        }
    public List<string> get_eth_client_target_mac(s)
        {
        return eth_client_target_macs;
        }
    public bool get_isMaster()
        {
        return isMaster;
        }
    public List<int> get_mesh_links_cto_rssi()
        {
        return mesh_links_cto_rssi;
        }
    public List<int> get_sta_clients_rssi()
        {
        return sta_clients_rssi;
        }
    public List<string> get_mesh_links_cto_serial()
        {
        return mesh_links_cto_serial;
        }
    public List<int> get_sta_clients_rxpr()
        {
        return sta_clients_rxpr;
        }
    public List<string> get_sta_clients_target_mac()
        {
        return sta_clients_target_mac;
        }
    public List<int> get_sta_clients_txpr()
        {
        return sta_clients_txpr;
        }

    // -------------------- Print ------------------------------
    public string print_mesh_links_cto_rssi()
        {
        string t = "";
        for (int i = 0; i < mesh_links_cto_rssi.Count; i++) { t += mesh_links_cto_rssi[i].ToString() + " "; }
        return t;
        }
    public string print_mesh_links_cto_serial()
        {
        string t = "";
        for (int i = 0; i < mesh_links_cto_serial.Count; i++) { t += mesh_links_cto_serial[i] + " "; }
        return t;
        }

    public string print_sta_clients_rssi()
        {
        string t = "";
        for (int i = 0; i < sta_clients_rssi.Count; i++) { t += sta_clients_rssi[i].ToString() + " "; }
        return t;
        }
    public string print_sta_clients_rxpr()
        {
        string t = "";
        for (int i = 0; i < sta_clients_rxpr.Count; i++) { t += sta_clients_rxpr[i].ToString() + " "; }
        return t;
        }
    public string print_sta_clients_target_mac()
        {
        string t = "";
        for (int i = 0; i < sta_clients_target_mac.Count; i++) { t += sta_clients_target_mac[i] + " "; }
        return t;
        }
    public string print_sta_clients_txpr()
        {
        string t = "";
        for (int i = 0; i < sta_clients_txpr.Count; i++) { t += sta_clients_txpr[i].ToString() + " "; }
        return t;
        }
    }
