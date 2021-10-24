using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnClickChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
