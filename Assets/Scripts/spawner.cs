using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
    {
    private bool _mouseState;
    public GameObject platform;
    public GameObject router;
    public GameObject phone;
    public GameObject laptop;
    public GameObject assistant;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;

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
        objTrans.position = new Vector3(0, -0.5f, 0);
        Instantiate(platform, objTrans.position, objTrans.rotation);

        objTrans.position = new Vector3(0, 1.5f, 0);
        Instantiate(router, objTrans.position, objTrans.rotation);


        List<int> nextCoordinate = new List<int> { 1, 1, 1, 1 };
        Vector3 objPos = new Vector3();

        // Render random device types initialization
        System.Random rnd = new System.Random();
        int rndNum = 0;

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

            rndNum = rnd.Next(1, 4);

            switch(rndNum) {
                case 1:
                    Instantiate(phone, phone.transform.position + objPos, phone.transform.rotation);
                    break;
                case 2:
                    objTrans.rotation = Quaternion.Euler(objTrans.rotation.x, objTrans.rotation.y + 180, objTrans.rotation.z);
                    Instantiate(laptop, objTrans.position, objTrans.rotation);
                    break;
                case 3:
                    Instantiate(assistant, assistant.transform.position + objPos, assistant.transform.rotation);
                    break;
                default:
                    Debug.Log("error rendering object");
                    break;
                }
            }
        }
    //https://forum.unity.com/threads/drag-drop-game-objects-without-rigidbody-with-the-mouse.64169/

    void Update()
        {
        if (Input.GetMouseButtonDown(0))
            {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo);
            
            if (target != null)
                {
                _mouseState = true;
                //converting world position to screen position
                screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                }
            }
        if (Input.GetMouseButtonUp(0))
            {
            _mouseState = false;
            }
        if (_mouseState)
            {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

            //update the position of the object in the world
            target.transform.position = curPosition;
            }
        }


    GameObject GetClickedObject(out RaycastHit hit)
        {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
            target = hit.collider.gameObject;
            }

        return target;
        }
}

