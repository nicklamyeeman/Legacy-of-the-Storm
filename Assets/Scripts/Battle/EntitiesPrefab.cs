using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesPrefab : MonoBehaviour
{
    public GameObject player;
    public GameObject heavyBandit;
    public GameObject lightBandit;
    public GameObject skeleton;

    public Enemy GenerateEnemy(GameObject prefab, Transform transform)
    {
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
        obj.transform.SetParent(transform);
        return obj.GetComponent<Enemy>();
    }

    public Player GeneratePlayer(GameObject prefab, Transform transform)
    {
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
        obj.transform.SetParent(transform);
        return obj.GetComponent<Player>();
    }

}
