﻿using System.Collections;
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

    private void PrintEthClients(List<EthClients> eth_clients)
        {
        int counter = 0;
        Debug.Log("Printing Eth Clients");
        foreach (var client in eth_clients)
        {
            Debug.Log(
                $"{counter} client\n" + 
                $"{client.idle}\n" +
                $"{client.target_mac}\n" +
                $"{client.device_info.hostname}\n" +
                $"{client.device_info.ip_addr}\n" +
                $"{client.device_info.location}\n\n"
            );
        }
    }

    // Functions
    public void PrintTopology(List<Topology> network_devices)
        {
        foreach (var dev in network_devices)
            {
            Debug.Log($"Serial: {dev.serial}");
            PrintEthClients(dev.get_eth_clients());
            }
        }

    }

    public void OrganizeByRouter(JsonParse loaded_data, ref List<Topology> network_devices, ref List<string> serials, ref int num_devices)
        {
        // Topology Objects
        EthConnection[] eth_clients = loaded_data.eth_clients;
        StaConnection[] sta_clients = loaded_data.sta_clients;
        MeshLink[] mesh_links = loaded_data.mesh_links;

        // Eth Clients
        //  - Init. all serials (Router/Extenders)
        if (eth_clients != null) 
            {
            for (int i = 0; i < eth_clients.Length; i++)
                {
                // eth_clients
                //  - client ('temp')
                //      : idle
                //      : target_mac
                //      : hostname
                //      : IP_Address
                //  - serial
                Eth[] clients;
                List<EthClients> store_temp;
                string get_router_serial;

                // Initalize
                //  - serial            = "Router/Extender" serial #
                //  - clients           = eth_clients[i] -> clients connected to network
                store_temp = new List<EthClients>();
                get_router_serial = eth_clients[i].serial;
                clients = eth_clients[i].clients;

                //  - clients[]
                //      - idle
                //      - target_mac
                //      - hostname
                //      - IP_Address
                Debug.Log(clients.Length);
                for (int j = 0; j < clients.Length; j++) //May have problems in the future bc if data in any but in order
                    {
                    EthClients temp = new EthClients();
                    temp.idle = clients[j].idle;
                    temp.target_mac = clients[j].target_mac;
                    temp.device_info.hostname = clients[j].hostname;
                    temp.device_info.ip_addr = clients[j].IP_Address;
                    
                    //  - Store current eth_client
                    store_temp.Add(temp);
                    }

                // Store
                //  - connected_device      = Device connected to the current "Router/Extender"
                //  - get_router_serial     = "Router/Extender" serial #
                //  - num_devices           = Count connected devices to the Router/Extender
                Topology connected_device = new Topology(store_temp);
                network_devices.Add(connected_device);
                serials.Add(get_router_serial);

                num_devices += clients.Length;
                }
            }

        /*
        // Mesh Links
        //  - Check serials & organizes them
        if (mesh_links != null) 
            {
            for (int i = 0; i < mesh_links.Length; i++)
                {
                // mesh_links
                //  - connected_to
                //      : rssi
                //      : serial    (Other Router/Extenders)
                //  - isMaster
                //  - serial        (Main Router/Extender)
                //  - hostname
                //  - IP_Address
                MeshLinks temp;
                int index;              // Used for "Binary Search"
                string main_serial;

                // Initialize
                //  - connected_to  = Devices connected to Router/Extender
                //  - hostname
                //  - IP_Address
                //  - serial
                //  - index         = Location of Serial # in 'serials'
                temp.connected_to.AddRange(mesh_links[i].connected_to);
                temp.device_info.hostname = mesh_links[i].hostname;
                temp.device_info.ip_addr = mesh_links[i].IP_Address;
                main_serial = mesh_links[i].serial;
                index = serials.BinarySearch(main_serial);

                // Store Mesh Link values
                //  - isMaster
                //  - connected_to
                //      : rssi
                //      : serial
                network_devices[index].set_isMaster(mesh_links[i].isMaster);
                
                // Store 
                network_devices[index].add_mesh_links(temp);
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
                    StaClients temp;
                    Sta curr_client;

                    // Initialize
                    //  - Grab current client
                    //  - Store 
                    curr_client = clients[t];
                    temp.rssi = curr_client.rssi;
                    temp.rxpr = curr_client.rxpr;
                    temp.target_mac = curr_client.target_mac;
                    temp.txpr = curr_client.txpr;

                    // Store
                    network_devices[index].add_sta_clients(temp);
                    }

                    // Add Devices connected wirelessly
                    num_devices += clients.Length;
                }
            }
             */
        }

    // Used to ensure Json Values are stored correctly into 'loaded_data' in JsonMain.cs
    public void PrintJsonParsing(JsonParse loaded_data)
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
