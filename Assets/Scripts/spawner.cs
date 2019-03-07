using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject router;
    public GameObject device1;
    public GameObject device2;

    // Start is called once at the beginning of the program
    void Start()
    {
        // Initialize JsonMain script and start parsing Json
        JsonMain jsonMain = new JsonMain();
        jsonMain.Start();

        // Retrieve network_devices and serials from JsonMain
        List<Topology> network_devices = jsonMain.GetDevices();
        List<string> serials = jsonMain.GetSerials();

        // Template transform variable for GameObject positioning and rotation
        Transform objTrans = new GameObject().transform;

        // Render scene
        objTrans.position = new Vector3(0, -3, 0);
        Instantiate(platform, objTrans.position, objTrans.rotation);

        objTrans.position = new Vector3(0, 0, 0);
        Instantiate(router, objTrans.position, objTrans.rotation);

        List<int> nextCoordinate = new List<int> { 1, 1, 1, 1 };
        Vector3 objPos = new Vector3();
        
        for (int i = 0; i < network_devices.Count; i++)
        {
            objPos.x = 0;
            objPos.z = 0;

            var num = i < 3 ? i : i % 4;
            switch(num) {
                case 0:
                    objPos.x = i + (nextCoordinate[0] * 15);
                    nextCoordinate[0]++;
                    break;
                case 1:
                    objPos.x = i + (-nextCoordinate[1] * 15);
                    nextCoordinate[1]++;
                    break;
                case 2:
                    objPos.z = i + (nextCoordinate[2] * 15);
                    nextCoordinate[2]++;
                    break;
                case 3:
                    objPos.z = i + (-nextCoordinate[3] * 15);
                    nextCoordinate[3]++;
                    break;
                default:
                    Debug.Log("Rendering device error - Line 54");
                    break;
            }
            objTrans.position = objPos;

            Instantiate(device2, objTrans.position, objTrans.rotation);
        }
    }
}
