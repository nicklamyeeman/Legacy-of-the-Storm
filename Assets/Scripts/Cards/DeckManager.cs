using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public List<Card> originalDeck = new List<Card>();

    private List<Card> hand = new List<Card>();
    public List<Card> Hand { get { return hand; } }

    private List<Card> discard = new List<Card>();
    public List<Card> Discard { get { return discard; } }

    public List<Card> deck = new List<Card>();
    public List<Card> Deck { get { return deck; } }

    public int DeckCount { get { return deck.Count; } }
    public int DiscardCount { get { return discard.Count; } }

    [SerializeField] private GameObject cardPattern;

    private PhysicDeck physicDeck;

    private void Awake()
    {
        physicDeck = gameObject.GetComponent<PhysicDeck>();
    }

    void Start()
    {
    }

    public void GenerateDeck()
    {
        for (double i = 0, offset = -0.3f; i < originalDeck.Count; i += 1, offset += 0.5)
        {
            deck.Add(originalDeck[(int)i]);
            physicDeck.Generate(cardPattern, this.transform, (float)offset);
        }
    }

    public void SendToDiscardnb(int pos)
    {
        SendToDiscard(pos, hand);
    }

    public void SendToDiscard(int pos, List<Card> list)
    {
        pos -= 1;
        Card toDiscard = list[pos];
        //GameObject toDelete = GameObject.FindGameObjectsWithTag("Card")[pos];
        //Destroy(toDelete);
        discard.Add(toDiscard);
        list.RemoveAt(pos);
    }

    public void SendHandToDiscard()
    {
        for (int i = 0; i < hand.Count; i += 1)
            discard.Add(hand[i]);
        hand.Clear();
    }

    public void SendDiscardToDeck()
    {
        for (int i = 0; i < discard.Count; i += 1)
            deck.Add(discard[i]);
        discard.Clear();
        //ShuffleDeck();
    }


    public List<GameObject> GetCards(int nb)
    {
        int draw;

        for (draw = 0; deck.Count > 0 && draw < nb; draw += 1)
        {
            Card card = deck.PopBack();
            hand.Add(card);
        }
        if (draw < nb)
        {
            SendDiscardToDeck();
            GetCards(nb - draw);
        }
        List<GameObject> cards = physicDeck.LinkCardsInfo(hand);
        hand = new List<Card>();
        return (cards);
    }

    private void ShuffleDeck()
    {
        //ListCards(discard);
        for (int i = 0; i < deck.Count; i++)
        {
            // Random for remaining positions.
            int rand;
            while ((rand = Random.Range(0, deck.Count - 1)) == i) ;

            //swapping the elements
            Card temp = deck[rand];
            deck[rand] = deck[i];
            deck[i] = temp;
        }
        //ListCards(deck);
    }

    private void ListCards(List<Card> cards)
    {
        // Debug purpose
        print("Listing cards");
        foreach (Card card in cards)
            print(card.name);
    }
}