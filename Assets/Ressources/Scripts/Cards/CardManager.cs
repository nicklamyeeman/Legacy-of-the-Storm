using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private CardAnimationManager animManager;

    public float animationTime = 0.25f;

    private GameObject targetObject;
    private GameObject cardHolder;

    public enum E_State
    {
        DEFAULT,
        DRAWING,
        DRAGGED,
        IN_HAND,

        IN_DECK,
        ON_BOARD,
        IN_DISCARD
    }

    public E_State CardState
    {
        get { return cardState; }
        set
        {
            cardState = value;
            if (cardState == E_State.IN_HAND)
            {
                gameObject.GetComponent<CardDisplay>().CardScale = gameObject.transform.localScale;
                Debug.Log("Card scale: " + gameObject.GetComponent<CardDisplay>().CardScale);
                if (gameObject.transform.parent != cardHolder.transform)
                {
                    gameObject.transform.SetParent(cardHolder.transform);
                    gameObject.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
    private E_State cardState;

    private void Awake()
    {
        CardState = E_State.DEFAULT;
        animManager = GetComponentInChildren<CardAnimationManager>();
    }

    public void PlaceCardToHand(GameObject playerHand) {
        this.targetObject = playerHand;
        cardHolder = CreateCardHolder();
        animManager.LaunchAnimation(cardHolder, "Drawing", animationTime);
    }

    public void PlaceCardToDiscard(GameObject discard)
    {
        this.targetObject = discard;
        animManager.CardToDiscardAnim(discard, animationTime);
        if (cardHolder != null)
            Destroy(cardHolder);
    }

    private GameObject CreateCardHolder()
    {
        GameObject cardHolder = new GameObject();

        cardHolder.AddComponent<RectTransform>();
        cardHolder.name = gameObject.name + "Holder";
        Rect rectHolder = cardHolder.GetComponent<RectTransform>().rect;
        rectHolder.width = 80;
        rectHolder.height = 80;
        cardHolder.transform.localScale = new Vector3(1f, 1f, 1);
        cardHolder.transform.SetParent(targetObject.transform);
        return cardHolder;
    }

}
