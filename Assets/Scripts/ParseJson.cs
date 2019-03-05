using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseJson : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Parsing JSON");

        /* Capstone */
        string json = File.ReadAllText(Application.dataPath + "/json3.json");
        Topology loaded_data = JsonUtility.FromJson<Topology>(json);
        /*  - Ethernet */
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
            }
        }

        /*  - Mesh Links */
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
            }
            //  - isMaster
            //  - serial (mesh_link object / main)
        }
        /*  - Sta (Wireless) */
        StaConnection[] sta_clients = loaded_data.sta_clients;
        if (loaded_data.sta_clients != null)
        {
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
                }
                //  - serial (mesh_link object / main)
            }
        }
        //Organizing Routers into Objects //
        List<Obj> Routers = new List<Obj>();
        List<string> masterSerial = new List<string>(); //Makes it easier to find serial number of each class to put in info into class
        //Puts Eth_clients into class during initialization of class bc its simpler.
        for (int i = 0; i < loaded_data.eth_clients.Length; i++)
        {
            string temp_eth_clients_idle = "";
            string temp_eth_clients_target_mac = "";
            if (i < loaded_data.eth_clients[i].clients.Length) //May have problems in the future bc if data in any but in order
            {
                temp_eth_clients_idle = loaded_data.eth_clients[i].clients[i].idle;
                temp_eth_clients_target_mac = loaded_data.eth_clients[i].clients[i].target_mac;
            }
            Obj tempSerial = new Obj(loaded_data.eth_clients[i].serial, temp_eth_clients_idle, temp_eth_clients_target_mac);
            Routers.Add(tempSerial);
            masterSerial.Add(loaded_data.eth_clients[i].serial);
        }
        //Mesh Links after to check the serial and organize that way!
        for (int i = 0; i < loaded_data.mesh_links.Length; i++)
        {
            int index = masterSerial.BinarySearch(loaded_data.mesh_links[i].serial);
            Routers[index].set_isMaster(loaded_data.mesh_links[i].isMaster);
            for (int t = 0; t < loaded_data.mesh_links[i].connected_to.Length; t++)
            {
                Routers[index].add_mesh_link_cto_rssi(loaded_data.mesh_links[i].connected_to[t].rssi);
                Routers[index].add_mesh_link_cto_serial(loaded_data.mesh_links[i].connected_to[t].serial);
            }
        }

        //sta_clients
        if (loaded_data.sta_clients != null)
        {
            for (int i = 0; i < loaded_data.sta_clients.Length; i++)
            {
                int index = masterSerial.BinarySearch(loaded_data.sta_clients[i].serial);
                for (int t = 0; t < loaded_data.sta_clients[i].clients.Length; t++)
                {
                    Routers[index].add_sta_client_rssi(loaded_data.sta_clients[i].clients[t].rssi);
                    Routers[index].add_sta_client_rxpr(loaded_data.sta_clients[i].clients[t].rxpr);
                    Routers[index].add_sta_client_target_mac(loaded_data.sta_clients[i].clients[t].target_mac);
                    Routers[index].add_sta_client_txpr(loaded_data.sta_clients[i].clients[t].txpr);
                }
            }
        }
        //Testing Class//
        Debug.Log("Testing Class");
        for (int i = 0; i < Routers.Count; i++)
        {
            Debug.Log(Routers[i].get_serial() + "\n Eth_client: " + Routers[i].get_eth_client_idle() + "  " + Routers[i].get_eth_client_target_mac()
                + "\n isMaster: " + Routers[i].get_isMaster() + "\n IS CONNECTED TO MESH_LINKS: " + Routers[i].print_mesh_link_cto_rssi()
                + " " + Routers[i].print_mesh_link_cto_serial() + "\n Sta_clients: " + Routers[i].print_sta_client_rssi() + " " + Routers[i].print_sta_client_rxpr() + " " +
                Routers[i].print_sta_client_target_mac() + " " + Routers[i].print_sta_client_txpr());
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

        public Obj(string serial, string eth_client_idle, string eth_client_target_mac)
        {
            this.serial = serial;
            this.eth_client_idle = eth_client_idle;
            this.eth_client_target_mac = eth_client_target_mac;
        }

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
        public void set_isMaster(bool isMaster)
        {
            this.isMaster = isMaster;
        }
        public bool get_isMaster()
        {
            return isMaster;
        }
        public List<int> get_mesh_link_cto_rssi()
        {
            return mesh_link_cto_rssi;
        }
        public string print_mesh_link_cto_rssi()
        {
            string t = "";
            for (int i = 0; i < mesh_link_cto_rssi.Count; i++) { t += mesh_link_cto_rssi[i].ToString() + " "; }
            return t;
        }
        public void add_mesh_link_cto_rssi(int rssi)
        {
            mesh_link_cto_rssi.Add(rssi);
        }
        public List<string> get_mesh_link_cto_serial()
        {
            return mesh_link_cto_serial;
        }
        public string print_mesh_link_cto_serial()
        {
            string t = "";
            for (int i = 0; i < mesh_link_cto_serial.Count; i++) { t += mesh_link_cto_serial[i] + " "; }
            return t;
        }
        public void add_mesh_link_cto_serial(string serial)
        {
            mesh_link_cto_serial.Add(serial);
        }
        public string print_sta_client_rssi()
        {
            string t = "";
            for (int i = 0; i < sta_client_rssi.Count; i++) { t += sta_client_rssi[i].ToString() + " "; }
            return t;
        }
        public void add_sta_client_rssi(int rssi)
        {
            sta_client_rssi.Add(rssi);
        }
        public List<int> get_sta_client_rssi()
        {
            return sta_client_rssi;
        }
        public string print_sta_client_rxpr()
        {
            string t = "";
            for (int i = 0; i < sta_client_rxpr.Count; i++) { t += sta_client_rxpr[i].ToString() + " "; }
            return t;
        }
        public void add_sta_client_rxpr(int rxpr)
        {
            sta_client_rxpr.Add(rxpr);
        }
        public List<int> get_sta_client_rxpr()
        {
            return sta_client_rxpr;
        }
        public string print_sta_client_target_mac()
        {
            string t = "";
            for (int i = 0; i < sta_client_target_mac.Count; i++) { t += sta_client_target_mac[i] + " "; }
            return t;
        }
        public void add_sta_client_target_mac(string target_mac)
        {
            sta_client_target_mac.Add(target_mac);
        }
        public List<string> get_sta_client_target_mac()
        {
            return sta_client_target_mac;
        }
        public string print_sta_client_txpr()
        {
            string t = "";
            for (int i = 0; i < sta_client_txpr.Count; i++) { t += sta_client_txpr[i].ToString() + " "; }
            return t;
        }
        public void add_sta_client_txpr(int txpr)
        {
            sta_client_txpr.Add(txpr);
        }
        public List<int> get_sta_client_txpr()
        {
            return sta_client_txpr;
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
         * Example Values      
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


    // Update is called once per frame
    void Update()
    {

    }
}
