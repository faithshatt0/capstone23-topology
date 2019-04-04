using System;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInfo
    {
    public string hostname = "";
    public string ip_addr = "";
    }

public class EthClients
    {
    public DeviceInfo device_info = new DeviceInfo();
    public string idle = "";
    public string target_mac = "";
    }

public class MeshLinks
    {
    public DeviceInfo device_info = new DeviceInfo();
    public List<Device> connected_to = new List<Device>();
    }

public class StaClients
    {
    public DeviceInfo device_info = new DeviceInfo();
    public int rssi = 0;              // rssi - Signal strength for the device router or extender is connected to 
    public int rxpr = 0;              // rxpr - for connection 
    public string target_mac = "";    // tmac - target_mac for connection
    public int txpr = 0;              // txpr - for connection
    }

//Object class//
public class Topology
    {
    //Data Values//
    //  - Router/Extender devices are connected to
    private bool isMaster;                           // Router?
    private string serial;                           // Router/Extender serial #
    private Tuple<double, double, double> location = new Tuple<double, double, double>(0, 0, 0);

    //  - eth_clients
    private List<EthClients> eth_clients = new List<EthClients>();

    //  - mesh_links
    private List<MeshLinks> mesh_links = new List<MeshLinks>();

    //  - sta_clients
    private List<StaClients> sta_clients = new List<StaClients>();

    // ------------------------------ Constructor -------------------------
    public Topology(List<EthClients> temp)
        {
        eth_clients = temp;
        }

    // ------------------------------ Setter ------------------------------
    public void set_isMaster(bool isMaster)
        {
        this.isMaster = isMaster;
        }

    public void set_serial(string serial)
        {
        this.serial = serial;
        }

    public void set_location(double x, double y, double z)
        {
        location = Tuple.Create(x, y, z);
        }

    // ------------------------------ Add ---------------------------------
    //  - Mesh Link
    public void add_mesh_links(MeshLinks temp) 
        {
        mesh_links.Add(temp);
        }
    
    //  - Sta Client
    public void add_sta_clients(StaClients temp)
        {
        sta_clients.Add(temp);
        }   

    // ------------------------------ Getters ------------------------------
    public List<EthClients> get_eth_clients() 
        {
        return eth_clients;
        }
    
    public List<MeshLinks> get_mesh_links()
        {
        return mesh_links;
        }

    public List<StaClients> get_sta_clients()
        {
        return sta_clients;
        }

    public string get_serial()
        {
        return serial;
        }

    public Tuple<double, double, double> get_location()
        {
        return location;
        }
}
