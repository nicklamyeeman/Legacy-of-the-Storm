using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOscilliator : MonoBehaviour
{
    float timeCounter = 0;

    public float speed = 1;
    public float width = 2;
    public float height = 2;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * -width;
        float y = Mathf.Sin(timeCounter) * height;

        transform.position = new Vector2(initialPos.x + x, initialPos.y + y);
    }
}
