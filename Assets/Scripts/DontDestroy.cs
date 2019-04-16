using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public void dontDestroyObj()
    {
        //var instance = FindObjectOfType<T>();
        DontDestroyOnLoad(transform.root.gameObject);
    }
   
}
