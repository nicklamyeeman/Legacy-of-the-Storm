using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public GameObject line;
    public Sprite[] islandsSprite;

    int id;
    public int ID { get { return id; } set { id = value; } }

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;

    [HideInInspector]
    GameObject[] lineArray = null;
    Collider2D[] reachableIslands = null;
    SpriteRenderer spRenderer;

    bool hasFound = false;
    bool hasDrawn = false;
    bool isCurrentIsland = false;
    public bool IsCurrentIsland {
        get { return isCurrentIsland; }
        set
        {
            isCurrentIsland = value;
            if (isCurrentIsland == true)
            {
                ChangeIslandColor();
            }
        }
    }

    private void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = islandsSprite[Random.Range(0, 14)];
    }

    void OnMouseOver()
    {
        this.gameObject.layer = 7;
        transform.localScale = new Vector2(0.75f, 0.75f);

        if (hasFound == false)
            FindReachableIslands();
        if (hasDrawn == false && (isCurrentIsland == true || IsCurrentNeighbor() == true))
            DrawLines();
    }

    void OnMouseExit()
    {
        ResetIslandState();
    }

    public void FindReachableIslands()
    {
        reachableIslands = null;
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        reachableIslands = targetsInViewRadius;
        hasFound = true;
    }

    void DrawLines()
    {
        lineArray = new GameObject[reachableIslands.Length];
        for (int i = 0; i < reachableIslands.Length; i += 1)
        {
            {
                lineArray[i] = Instantiate(line, transform.position, Quaternion.identity);
                LineRenderer tmpLine = lineArray[i].GetComponent<LineRenderer>();
                tmpLine.SetPosition(0, transform.position);
                tmpLine.SetPosition(1, reachableIslands[i].transform.position);
            }
        }
        hasDrawn = true;
    }

    bool IsCurrentNeighbor()
    {
        foreach (Collider2D col in reachableIslands)
        {
            IslandManager targetManager = col.transform.gameObject.GetComponent<IslandManager>();
            if (targetManager.IsCurrentIsland == true)
                return true;
        }
        return false;
    }

    public bool IsReachable(int targetIslandId)
    {
        foreach (Collider2D col in reachableIslands)
        {
            IslandManager manager = col.transform.gameObject.GetComponent<IslandManager>();
            if (manager.ID == targetIslandId)
                return true;
        }
        return false;
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public Collider2D[] getVisibleTarget()
    {
        return reachableIslands;
    }

    public void ResetIslandState()
    {
        this.gameObject.layer = 6;
        transform.localScale = new Vector2(0.5f, 0.5f);
        if (lineArray != null)
        {
            for (int i = 0; i < lineArray.Length; i += 1)
                Destroy(lineArray[i]);
        }
        hasFound = false;
        hasDrawn = false;
    }

    private void ChangeIslandColor()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Color newColor = spr.color;
        newColor.a = 0.5f;
        spr.color = newColor;
    }
}
