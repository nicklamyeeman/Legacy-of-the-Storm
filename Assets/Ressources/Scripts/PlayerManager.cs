using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int life;
    private int golds;

    private CardManager cardManager;
    private DeckManager deckManager;

    private GameObject deck;
    private GameObject hand;
    private GameObject discard;

    private int nbCardsToDraw = 5;

    private List<GameObject> handCards = new List<GameObject>();
    public List<GameObject> HandCards { get { return handCards; } set { handCards = value; } }

    private List<GameObject> drawnCards = new List<GameObject>();
    public List<GameObject> DrawnCards
    {
        get { return drawnCards; }
        set
        {
            drawnCards = value;
            FromDeckToHands();
        }
    }

    void Awake()
    {
        deck = GameObject.FindGameObjectWithTag("Deck");
        hand = GameObject.FindGameObjectWithTag("Hand");
        discard = GameObject.FindGameObjectWithTag("Discard");
        deckManager = deck.GetComponent<DeckManager>();
    }

    public void GeneratePlayerDeck()
    {
        deckManager.GenerateDeck();
    }

    public void DrawCards()
    {
        DrawnCards = deckManager.GetCards(nbCardsToDraw);
        Debug.Log("Nombre de cartes à draw : " + DrawnCards.Count);
    }

    void FromDeckToHands()
    {
        if (drawnCards.Count != 0)
        {
            cardManager = drawnCards[0].GetComponent<CardManager>();
            cardManager.CardState = CardManager.E_State.DRAWING;
            cardManager.PlaceCardToHand(hand);
        }
    }

    public void DiscardLeftCards()
    {
        foreach (GameObject card in handCards)
        {
            Debug.Log("name to discard: " + card.name);
            CardManager cm = card.GetComponent<CardManager>();
            cm.PlaceCardToDiscard(discard);
        }
        handCards.Clear();
    }

    public GameObject GetHand()
    {
        return hand;
    }
}
