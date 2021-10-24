using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseCardZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject discard;
    public GameObject line;

    private GameObject lineRender;
    private bool drag = false;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragiing in ");
    }

    private void UseCard(Card cardInfo, BattleManager battleManager)
    {
        // TODO: Get the target and then if targetable use card
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // not hitting UI things
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {

            //TargetAble hitTarget = hit.collider.GetComponent<TargetAble>();
            print("Use card: " + cardInfo.name + " on : " + hit.collider.name);
            battleManager.OnUseCard(cardInfo, hit.collider.transform.parent.gameObject);
        } else
        {
            battleManager.OnUseCard(cardInfo, null);
        }

        //gameManager.OnUseCard(cardInfo, Target);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.dragging)
            return;

        print("Stop drag on use card zone");

        CardManager cardManager = eventData.pointerDrag.GetComponent<CardManager>();
        if (cardManager)
        {
            cardManager.GetComponent<CardDisplay>().OnDragEnd();
            BattleManager gameManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
            if (!gameManager)
            {
                Debug.Log("GameManager was not found on the scene");
                return;
            }
            Debug.Log("drop card for use");
            // Check if the target is targetable and then use card
            Card cardInfo = cardManager.GetComponent<CardDisplay>().CardInfo;
            print("Calling use card");
            UseCard(cardInfo, gameManager);
            cardManager.PlaceCardToDiscard(discard);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.dragging)
            return;
        Debug.Log("pointer entered zone");
        GameObject gameObject = eventData.pointerDrag;
        Card card = eventData.pointerDrag.GetComponent<CardDisplay>().CardInfo;
        drag = true;
        if (card)
        {
            if (card.type == Card.Type.ATTACK)
            {
                // Place the card at the center of the screen and draw an arrow from the card to the mouse
                CardDisplay cd = gameObject.GetComponent<CardDisplay>();
                cd.FollowMouse = false;
                //lineRender = Instantiate(line, transform.position, Quaternion.identity);

                //LineRenderer tmpLine = lineRender.GetComponent<LineRenderer>();
                //tmpLine.SetPosition(0, gameObject.transform.position);
                //tmpLine.SetPosition(1, eventData.position);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.dragging)
            return;
        Debug.Log("Exited and returning to drag");
        GameObject gameObject = eventData.pointerDrag;
        Card card = eventData.pointerDrag.GetComponent<CardDisplay>().CardInfo;
        if (card)
        {
            if (card.type == Card.Type.ATTACK)
            {
                CardDisplay cd = gameObject.GetComponent<CardDisplay>();
                cd.FollowMouse = true;
            }
        }
        drag = false;
    }

    void Awake()
    {
        //EventTrigger trigger = GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        //trigger.triggers.Add(entry);
    }

    void Update()
    {
        //if (drag)
        //    lineRender.GetComponent<LineRenderer>().SetPosition(1, Input.mousePosition);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        Debug.Log("OnPointerDownDelegate called.");
    }

    public void pointerClick()
    {
        Debug.Log("clicked standard way");
    }

}
