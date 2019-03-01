using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    //public Transform SpawnPos;
    public GameObject platform;
    public GameObject box;

    // Update is called once per frame
    void Start()
    {
        //Instantiate(spawnee, SpawnPos.position, SpawnPos.rotation);
        Instantiate(platform);
        Instantiate(box);
    }
}
