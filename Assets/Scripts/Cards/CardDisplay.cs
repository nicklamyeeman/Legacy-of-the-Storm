using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour
{
    public new Text name;
    public Text cost;
    public Text description;
    public Image artwork;

    public int pos;

    public Vector3 CardScale
    {
        get { return cardScale; }
        set { cardScale = value; }
    }
    private Vector3 cardScale;
    private Transform tmpParent;
    private CardManager cardManager;

    public bool FollowMouse = true;
    public Card CardInfo;

    void Awake()
    {
        cardManager = GetComponent<CardManager>();
    }

    public void SetCardInfo(Card card)
    {
        CardInfo = card;
        name.text = card.name;
        cost.text = card.cost.ToString();
        description.text = card.description;
        artwork.sprite = card.artwork;
    }

    public void FocusCard()
    {
        if (cardManager.CardState == CardManager.E_State.IN_HAND)
        {
            transform.localScale = new Vector3(cardScale.x * 1.2f, cardScale.y * 1.2f, cardScale.z * 1.2f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1);
        }
    }

    public void StopFocus()
    {
        if (cardManager.CardState == CardManager.E_State.IN_HAND)
        {
            transform.localScale = new Vector3(cardScale.x, cardScale.y, cardScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }

    public void OnPointerDown()
    {
        Debug.Log("Clicking on card: " + gameObject.name);
    }

    public void OnDrag()
    {
        //TODO: Add a target on possible entities that can be affected by the current card
        if (cardManager.CardState == CardManager.E_State.IN_HAND || cardManager.CardState == CardManager.E_State.DRAGGED)
        {
            cardManager.CardState = CardManager.E_State.DRAGGED;
            Vector3 mousePos = Input.mousePosition;
            this.gameObject.transform.SetParent(GameObject.Find("PlayerArea").transform);
            if (FollowMouse)
                transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    public void OnDragEnd()
    {
        // TODO: disable all targets
        print("Stop drag");
        FollowMouse = true;
        if (cardManager.CardState != CardManager.E_State.IN_HAND)
        {
            transform.localScale = new Vector3(cardScale.x, cardScale.y, cardScale.z);
            cardManager.CardState = CardManager.E_State.IN_HAND;
        }
    }
}
