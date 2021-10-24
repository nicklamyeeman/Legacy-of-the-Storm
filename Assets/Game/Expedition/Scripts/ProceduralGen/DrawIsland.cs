using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIsland : MonoBehaviour
{
	public GameObject island;

	public float radius = 1;
	public int rejectionSamples = 10;
	public float displayRadius = 0.3f;

	List<Vector2> points;

	void Start()
    {
		points = ProceduralMapGen.GeneratePoints(radius, new Vector2(transform.localScale.x, transform.localScale.y), rejectionSamples);
		if (points != null)
		{
			foreach (Vector2 point in points)
			{
				GameObject tmpObj = GameObject.Instantiate(island, new Vector2(point.x + transform.position.x, point.y + transform.position.y), Quaternion.identity) as GameObject;
			}
		}
	}
}