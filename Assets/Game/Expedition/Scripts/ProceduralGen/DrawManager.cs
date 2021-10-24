using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
	public GameObject islandPattern;

	public float radius = 1;
	public int rejectionSamples = 10;
	public float displayRadius = 0.3f;

	private GameObject[] islandsArray = null;

	List<Vector2> points;

    public void Draw()
    {
		points = ProceduralMapGen.GeneratePoints(radius, new Vector2(transform.localScale.x, transform.localScale.y), rejectionSamples);
		if (points != null)
		{
			int i = 0;
			islandsArray = new GameObject[points.Count];
			foreach (Vector2 point in points)
			{
				islandsArray[i] = GameObject.Instantiate(islandPattern, new Vector2(point.x + transform.position.x, point.y + transform.position.y), Quaternion.identity) as GameObject;
				islandsArray[i].name = "Island" + i;
				IslandManager manager = islandsArray[i].GetComponent<IslandManager>();
				manager.ID = i;
				i += 1;
			}

		}
	}

	public GameObject[] GetDrawnIslands()
    {
		return islandsArray;
    }

	public void SetIslandActive(bool state)
    {
		foreach (GameObject island in islandsArray)
        {
			island.gameObject.GetComponent<IslandManager>().ResetIslandState();
			island.gameObject.SetActive(state);
        }
    }

	public void DestroyAllIslands()
    {
		foreach (GameObject island in islandsArray)
        {
			if (island != null)
				Destroy(island);
        }
    }
}