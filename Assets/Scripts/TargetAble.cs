using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetAble : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public Sprite sprite;
    private BattleManager _bm;

    private bool isTargetAble = false;
    public bool IsTargetAble { get { return isTargetAble; } set { isTargetAble = value; } }
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = null;
        //_bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        //if (!_bm)
        //    print("There is not GameObject with the tag BattleManager or the BattleManager component");
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        //    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        //    if (hit.collider != null && hit.collider.gameObject == transform.parent.gameObject)
        //    {
        //        print("CLICKED : " + hit.collider.name);
        //        _bm.OnUseCard(null, hit.collider.transform.parent.gameObject);
        //    }
        //}
    }

    public void PlaceTarget()
    {
        IsTargetAble = true;
        _renderer.sprite = sprite;
    }

    public void RemoveTarget()
    {
        IsTargetAble = false;
        _renderer.sprite = null;
    }

}
