using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
    {
    // Start is called before the first frame update
    void Start()
        {
        
        }

    // Update is called once per frame
    void Update()
        {
        
        }

    // Functions
    public void ParseJson(JsonParse loaded_data, ref List<Topology> network_devices, ref List<string> serials, ref int num_devices)
        {
        // Topology Objects
        EthConnection[] eth_clients = loaded_data.eth_clients; 
        StaConnection[] sta_clients = loaded_data.sta_clients;
        MeshLink[] mesh_links = loaded_data.mesh_links;

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
            List<string> get_idle;
            List<string> get_target_mac;
            string get_router_serial;
            bool is_more_clients;

            // Initalize
            //  - get_idle          = empty for temp. storage (Empty indicates no clients)
            //  - get_target_mac    = empty for temp. storage (Empty indicates no clients)
            //  - serial            = "Router/Extender" serial #
            //  - clients           = eth_clients[i] -> clients connected to network
            get_idle = new List<string>();
            get_target_mac = new List<string>();
            get_router_serial = eth_clients[i].serial;
            clients = eth_clients[i].clients;
            is_more_clients = (i < clients.Length);

            //  - clients[]
            //      - idle
            //      - target_mac
            for (int j = 0; j < clients.Length; j++) //May have problems in the future bc if data in any but in order
                {
                get_idle.Add(clients[i].idle);
                get_target_mac.Add(clients[i].target_mac);
                }

            // Store
            //  - connected_device      = Device connected to the current "Router/Extender"
            //  - get_router_serial     = "Router/Extender" serial #
            //  - num_devices           = Count connected devices to the Router/Extender
            Topology connected_device = new Topology(get_router_serial, get_idle, get_target_mac);
            network_devices.Add(connected_device);
            serials.Add(get_router_serial);

            num_devices += clients.Length;
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
                // sta_client
                //  - serial
                Sta[] clients;
                string serial;
                int index;

                clients = sta_clients[i].clients;
                serial = sta_clients[i].serial;
                index = serials.BinarySearch(serial);

                for (int t = 0; t < clients.Length; t++)
                    {
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
                    curr_client = clients[t];
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

                    // Add Devices connected wirelessly
                    num_devices += clients.Length;
                }
            }
        
        }

    // Used to ensure Json Values are stored correctly into 'loaded_data' in JsonMain.cs
    public void PrintTopology(JsonParse loaded_data)
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
    }
