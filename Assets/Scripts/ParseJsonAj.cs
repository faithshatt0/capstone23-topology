using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseJsonAj : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Algorithm:
        //  1. Use JsonUtility to store JSON values in objects -> arrays
        //  2. Store devices based on their respective Router/Extender
        //  3. Test data

        /* Capstone */

        // 1. Read JSON file
        string file = Application.dataPath + "/JsonFiles/json2.json";
        string json = File.ReadAllText(file);
        Topology loaded_data = JsonUtility.FromJson<Topology>(json);

        //  - Optional: Print JSON File
        //print_topology(loaded_data);

        // Main Objects
        //  - network_devices   = Devices connected to the Router/Extender
        //  - serials           = Router references
        List<Obj> network_devices;
        List<string> serials;

        network_devices = new List<Obj>();
        serials = new List<string>();

        // 2. Store devices based on their respective Router/Extender
        parse_json(loaded_data, ref network_devices, ref serials);

        // 3. Test data
        Debug.Log("Testing Class");
        for (int i = 0; i < network_devices.Count; i++)
        {
        Debug.Log(network_devices[i].get_serial() + "\n Eth_client: " + network_devices[i].get_eth_client_idle() + "  " + network_devices[i].get_eth_client_target_mac()
            + "\n isMaster: " + network_devices[i].get_isMaster() + "\n IS CONNECTED TO MESH_LINKS: " + network_devices[i].print_mesh_link_cto_rssi()
            + " " +network_devices[i].print_mesh_link_cto_serial() + "\n Sta_clients: " + network_devices[i].print_sta_client_rssi() + " " + network_devices[i].print_sta_client_rxpr() + " " +
            network_devices[i].print_sta_client_target_mac() + " " + network_devices[i].print_sta_client_txpr());
        }
    }
//----------------------------------------------------------------------------------------
    //Object class//
    public class Obj
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
        public Obj(string serial, string eth_client_idle, string eth_client_target_mac)
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

    //-------------------------------------------------------------------------------//
    [System.Serializable]
    private class Topology
    {
        public EthConnection[] eth_clients;    // eth_clients
        public StaConnection[] sta_clients;        // sta_clients
        public MeshLink[] mesh_links;       // mesh_links
    }

    [System.Serializable]
    private class EthConnection : Connection<Eth> { }

    [System.Serializable]
    private class StaConnection : Connection<Sta> { }

    [System.Serializable]
    private class Connection<T>
    {
        public T[] clients;
        public string serial;
    }

    // JSON Objects
    [System.Serializable]
    private class Eth
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
    private class MeshLink
    {
        public Device[] connected_to;         // connected_to
        public bool isMaster;
        public string serial;
    }

    [System.Serializable]
    private class Sta
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
    private class Device
    {
        public int rssi;
        public string serial;
    }

    //-------------------------------------------------------------------------------//
    // Functions
    void parse_json(Topology loaded_data, ref List<Obj> network_devices, ref List<string> serials)
    {
        // Topology Objects
        EthConnection[] eth_clients;
        StaConnection[] sta_clients;
        MeshLink[] mesh_links;

        eth_clients = loaded_data.eth_clients;
        sta_clients = loaded_data.sta_clients;
        mesh_links = loaded_data.mesh_links;

        // Eth Clients
        //  - Init. all serials (Router/Extenders)
        for (int i = 0; i < eth_clients.Length; i++)
        {
            // eth_clients
            //  - client
            //      : idle
            //      : target_mac
            //  - serial
            Eth[] clients;
            string get_idle;
            string get_target_mac;
            string get_router_serial;

            bool is_more_clients;

            // Initalize
            //  - get_idle          = empty for temp. storage (Empty indicates no clients)
            //  - get_target_mac    = empty for temp. storage (Empty indicates no clients)
            //  - serial            = "Router/Extender" serial #
            //  - clients           = eth_clients[i] -> clients connected to network
            get_idle = "";
            get_target_mac = "";
            get_router_serial = eth_clients[i].serial;
            clients = eth_clients[i].clients;
            is_more_clients = (i < clients.Length);

            //  - clients[]
            //      - idle
            //      - target_mac
            if (is_more_clients) //May have problems in the future bc if data in any but in order
            {
                get_idle = clients[i].idle;
                get_target_mac = clients[i].target_mac;
            }

            // Store
            //  - connected_device      = Device connected to the current "Router/Extender"
            //  - get_router_serial     = "Router/Extender" serial #
            Obj connected_device = new Obj(get_router_serial, get_idle, get_target_mac);
            network_devices.Add(connected_device);
            serials.Add(get_router_serial);
        }

        //Mesh Links
        //  - Check serials & organizes them
        for (int i = 0; i < mesh_links.Length; i++)
        {
            // eth_clients
            //  - connected_to
            //      : rssi
            //      : serial    (Device)
            //  - isMaster
            //  - serial        (Router/Extender)
            string serial;
            bool isMaster;
            int index;
            Device[] connected_to;

            // Initialize
            //  - serial
            //  - index         = Location of Serial # in 'serials'
            //  - isMaster
            //  - connected_to  = Devices connected to Router/Extender
            serial = mesh_links[i].serial;
            index = serials.BinarySearch(serial);
            isMaster = mesh_links[i].isMaster;
            connected_to = mesh_links[i].connected_to;

            // Store Mesh Link values
            //  - isMaster
            //  - connected_to
            //      : rssi
            //      : serial
            network_devices[index].set_isMaster(isMaster);

            for (int t = 0; t < mesh_links[i].connected_to.Length; t++)
            {
                int device_rssi;
                string device_serial;

                device_rssi = mesh_links[i].connected_to[t].rssi;
                device_serial = mesh_links[i].connected_to[t].serial;

                // Store
                //  - rssi
                //  - serial
                network_devices[index].add_mesh_link_cto_rssi(device_rssi);
                network_devices[index].add_mesh_link_cto_serial(device_serial);
            }
        }

        // Sta Clients
        if (sta_clients != null)
        {
            for (int i = 0; i < sta_clients.Length; i++)
            {
                string serial;
                int index;

                serial = sta_clients[i].serial;
                index = serials.BinarySearch(serial);

                for (int t = 0; t < sta_clients[i].clients.Length; t++)
                {
                    // sta_client
                    //  - clients
                    //      : rssi
                    //      : rxpr
                    //      : target_mac
                    //      : txpr
                    Sta curr_client;
                    int rssi;
                    int rxpr;
                    string target_mac;
                    int txpr;

                    // Initialize
                    //  - Grab current client
                    //  - Store 
                    curr_client = sta_clients[i].clients[t];
                    rssi = curr_client.rssi;
                    rxpr = curr_client.rxpr;
                    target_mac = curr_client.target_mac;
                    txpr = curr_client.txpr;

                    // Store
                    network_devices[index].add_sta_client_rssi(rssi);
                    network_devices[index].add_sta_client_rxpr(rxpr);
                    network_devices[index].add_sta_client_target_mac(target_mac);
                    network_devices[index].add_sta_client_txpr(txpr);
                }
            }
        }
    }

    void print_topology(Topology loaded_data)
    {
        Debug.Log("--- Printing out Topology values ---");

        /*  - Ethernet */
        Debug.Log("Extracting: Ethernet Clients");

        EthConnection[] eth_clients = loaded_data.eth_clients;
        for (int i = 0; i < eth_clients.Length; ++i)
        {
            // eth_client
            EthConnection eth_client = eth_clients[i];
            string serial = eth_client.serial;

            //  - clients[]
            //      - idle
            //      - target_mac
            for (int j = 0; j < eth_client.clients.Length; ++j)
            {
                Eth curr_client = eth_client.clients[j];
                string idle = curr_client.idle;
                string target_mac = curr_client.target_mac;

                Debug.Log(i + "." + j + ") " + "Idle: " + idle + " | target_mac: " + target_mac);
            }

            //  - serial
            Debug.Log(i + ") " + "Serial: " + serial);
        }

        /*  - Mesh Links */
        Debug.Log("Extracting: Mesh Links");

        MeshLink[] mesh_links = loaded_data.mesh_links;
        for (int i = 0; i < mesh_links.Length; ++i)
        {
            // mesh_link
            MeshLink mesh_link = mesh_links[i];
            bool is_master = mesh_link.isMaster;
            string main_serial = mesh_link.serial;

            //  - connected_to[]
            //      - rssi
            //      - serial
            for (int j = 0; j < mesh_link.connected_to.Length; ++j)
            {
                Device curr_device = mesh_link.connected_to[j];
                int rssi = curr_device.rssi;
                string serial = curr_device.serial;

                Debug.Log(i + "." + j + ") " + "rssi: " + rssi + " | target_mac: " + serial);
            }

            //  - isMaster
            //  - serial (mesh_link object / main)
            Debug.Log(i + ") " + "isMaster: " + is_master + " | serial: " + main_serial);
        }

        /*  - Sta (Wireless) */
        Debug.Log("Extracting: Sta Clients");

        StaConnection[] sta_clients = loaded_data.sta_clients;
        for (int i = 0; i < loaded_data.sta_clients.Length; i++)
        {
            // sta_client
            StaConnection sta_client = sta_clients[i];
            string main_serial = sta_client.serial;

            //  - clients[]
            for (int j = 0; j < sta_client.clients.Length; ++j)
            {
                Sta curr_client = sta_client.clients[j];
                int rssi = curr_client.rssi;
                int rxpr = curr_client.rxpr;
                string target_mac = curr_client.target_mac;
                int txpr = curr_client.txpr;

                Debug.Log(i + "." + j + ") " + "rssi: " + rssi + " | rxpr: " + rxpr + " | target_mac: " + target_mac + " | txpr: " + txpr);
            }

            //  - serial (mesh_link object / main)
            Debug.Log(i + ") " + "serial: " + main_serial);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
