using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveObjects : MonoBehaviour
{

    public void save()
        {
        //number of objects
        //type of object
        //x
        //y
        //z
        }

    //load will be on spawner and i will access the positions with the serials from the json file. then for this script i will make it where the dragging and dropping
    //from spawner and actually move. Then raymond will save to json. Also move the info panel until you drop probably? 
    /*
       void SaveLocation(string file_path, string serial, double x, double y, double z)
       {
           int index = serials.BinarySearch(serial);
           location_data.serials[index].x = x;
           location_data.serials[index].y = y;
           location_data.serials[index].z = z;

           string json = JsonUtility.ToJson(location_data);
           File.WriteAllText(file_path, json);
       }
       */

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
