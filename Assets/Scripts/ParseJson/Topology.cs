using System.Collections.Generic;

//Object class//
public class Topology
    {
    //Data Values//
    private string serial; //Routers serial number
    private string eth_client_idle;
    private string eth_client_target_mac;
    private List<int> mesh_link_cto_rssi = new List<int>(); //what is the signal strength for the connection to extender or router
    private List<string> mesh_link_cto_serial = new List<string>(); //what extenders is this router or extender connected to
    private bool isMaster; //is this the master router
    private List<int> sta_client_rssi = new List<int>(); //what is the signal strength for the device router or extender is connected to 
    private List<int> sta_client_rxpr = new List<int>(); //rxpr for connection (what is rxpr)
    private List<string> sta_client_target_mac = new List<string>(); //target_mac for connection (what is target_mac)
    private List<int> sta_client_txpr = new List<int>(); // txpr for connection (what is txpr)

    // ------------------------------ Constructor ------------------------------
    public Topology(string serial, string eth_client_idle, string eth_client_target_mac)
        {
        this.serial = serial;
        this.eth_client_idle = eth_client_idle;
        this.eth_client_target_mac = eth_client_target_mac;
        }

    // ------------------------------ Setter ------------------------------
    public void set_isMaster(bool isMaster)
        {
        this.isMaster = isMaster;
        }

    // ------------------------------ Add ------------------------------
    public void add_mesh_link_cto_rssi(int rssi)
        {
        mesh_link_cto_rssi.Add(rssi);
        }
    public void add_mesh_link_cto_serial(string serial)
        {
        mesh_link_cto_serial.Add(serial);
        }
    public void add_sta_client_rssi(int rssi)
        {
        sta_client_rssi.Add(rssi);
        }   
    public void add_sta_client_rxpr(int rxpr)
        {
        sta_client_rxpr.Add(rxpr);
        }
    public void add_sta_client_target_mac(string target_mac)
        {
        sta_client_target_mac.Add(target_mac);
        }
    public void add_sta_client_txpr(int txpr)
        {
        sta_client_txpr.Add(txpr);
        }

    // ------------------------------ Getters ------------------------------
    public string get_serial()
        {
        return serial;
        }
    public string get_eth_client_idle()
        {
        return eth_client_idle;
        }
    public string get_eth_client_target_mac()
        {
        return eth_client_target_mac;
        }
    public bool get_isMaster()
        {
        return isMaster;
        }
    public List<int> get_mesh_link_cto_rssi()
        {
        return mesh_link_cto_rssi;
        }
    public List<int> get_sta_client_rssi()
        {
        return sta_client_rssi;
        }
    public List<string> get_mesh_link_cto_serial()
        {
        return mesh_link_cto_serial;
        }
    public List<int> get_sta_client_rxpr()
        {
        return sta_client_rxpr;
        }
    public List<string> get_sta_client_target_mac()
        {
        return sta_client_target_mac;
        }
    public List<int> get_sta_client_txpr()
        {
        return sta_client_txpr;
        }

    // -------------------- Print ------------------------------
    public string print_mesh_link_cto_rssi()
        {
        string t = "";
        for (int i = 0; i < mesh_link_cto_rssi.Count; i++) { t += mesh_link_cto_rssi[i].ToString() + " "; }
        return t;
        }
    public string print_mesh_link_cto_serial()
        {
        string t = "";
        for (int i = 0; i < mesh_link_cto_serial.Count; i++) { t += mesh_link_cto_serial[i] + " "; }
        return t;
        }

    public string print_sta_client_rssi()
        {
        string t = "";
        for (int i = 0; i < sta_client_rssi.Count; i++) { t += sta_client_rssi[i].ToString() + " "; }
        return t;
        }
    public string print_sta_client_rxpr()
        {
        string t = "";
        for (int i = 0; i < sta_client_rxpr.Count; i++) { t += sta_client_rxpr[i].ToString() + " "; }
        return t;
        }
    public string print_sta_client_target_mac()
        {
        string t = "";
        for (int i = 0; i < sta_client_target_mac.Count; i++) { t += sta_client_target_mac[i] + " "; }
        return t;
        }
    public string print_sta_client_txpr()
        {
        string t = "";
        for (int i = 0; i < sta_client_txpr.Count; i++) { t += sta_client_txpr[i].ToString() + " "; }
        return t;
        }
    }
