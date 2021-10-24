using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpeditionManager : MonoBehaviour
{

    public GameObject playerShip;

    DrawManager drawManager;
    GameObject[] islandsArray;

    GameObject tmpShip = null;
    GameObject lastIsland = null;
    GameObject firstIsland = null;
    IslandManager currentIslandManager = null;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("On scene loaded");
        if (scene.name.CompareTo("CardsSystem") == 0)
        {
            Debug.Log("Loading card system scene.");
            if (drawManager && tmpShip)
            {
                drawManager.SetIslandActive(false);
                tmpShip.gameObject.SetActive(false);
            }
        }
        else if (scene.name.CompareTo("ExpedetionMap") == 0)
        {
            Debug.Log("Loading expedition map scene.");
            if (drawManager && tmpShip)
            {
                drawManager.SetIslandActive(true);
                tmpShip.gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        drawManager = GetComponentInChildren<DrawManager>();
        drawManager.Draw();
        islandsArray = drawManager.GetDrawnIslands();
        FindBoundariesIslands();
        DrawPlayerShip(firstIsland.transform.position);
    }

    private void FindBoundariesIslands()
    {

        foreach (GameObject island in islandsArray)
        {
            firstIsland = firstIsland == null ? island : (island.transform.position.x <= firstIsland.transform.position.x ? island : firstIsland);
            lastIsland = lastIsland == null ? island : (island.transform.position.x >= lastIsland.transform.position.x ? island : lastIsland);
        }
        if (currentIslandManager == null)
        {
            currentIslandManager = firstIsland.GetComponent<IslandManager>();
            currentIslandManager.IsCurrentIsland = true;
            currentIslandManager.FindReachableIslands();
        }
    }

    private void DrawPlayerShip(Vector2 shipSpawnPoint)
    {
        tmpShip = Instantiate(playerShip, shipSpawnPoint, Quaternion.identity) as GameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Island"))
            {
                IslandManager targetIslandManager = hit.collider.transform.gameObject.GetComponent<IslandManager>();
                if (currentIslandManager.IsReachable(targetIslandManager.ID) == true && targetIslandManager.IsCurrentIsland == false)
                {
                    Destroy(tmpShip);
                    currentIslandManager.IsCurrentIsland = false;
                    currentIslandManager = targetIslandManager;
                    currentIslandManager.IsCurrentIsland = true;
                    DrawPlayerShip(hit.collider.gameObject.transform.position);
//                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}
