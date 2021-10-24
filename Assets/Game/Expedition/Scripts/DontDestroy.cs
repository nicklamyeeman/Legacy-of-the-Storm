using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
     public string objectTagToFind = "ExpeditionManager";
     public bool preventMultipleInstance = true;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(objectTagToFind);

        if (objs.Length > 1 && preventMultipleInstance == true)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
