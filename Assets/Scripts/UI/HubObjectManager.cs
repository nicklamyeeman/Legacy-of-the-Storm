using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HubObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp(){
        if (this.gameObject.name == "map") {
            StartMapExpedition();
        }
        else if (this.gameObject.name == "shop")
            OpenShop();
    }

    void StartMapExpedition()
    {
        MySceneManager.LoadScene("ExpedetionMap");
    }

    void OpenShop()
    {
        Debug.Log("Shop doesn't exist yet");
    }
}
