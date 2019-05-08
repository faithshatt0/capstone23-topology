using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// DInfo.cs
/// This script will push all of the informational panels to a far off location in the beginning 
/// 

public class DInfo : MonoBehaviour
    {
    // Start is called before the first frame update
    void Start()
        {
        foreach (var component in GetComponents<Canvas>())
            {
            component.transform.position = new Vector3(component.transform.position.x, component.transform.position.y, - 10000f);
            }
        }
    }
