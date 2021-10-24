using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionPoints : MonoBehaviour
{
    public Text actionText;

    // Use this for initialization
    void Start()
    {
        actionText.text = GetComponentInParent<Player>().action.ToString();
    }

    public void RefreshCount()
    {
        actionText.text = GetComponentInParent<Player>().action.ToString();
    }

}
