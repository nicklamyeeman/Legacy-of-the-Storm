using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PhysicDeck : MonoBehaviour
{
    public List<GameObject> physicDeck;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Generate(GameObject pattern, Transform parent, float offset)
    {
        physicDeck.Add(Instantiate(pattern, new Vector2(parent.position.x, parent.position.y + offset), pattern.transform.rotation, parent));
        physicDeck[physicDeck.Count - 1].name = "Card" + physicDeck.Count;
    }

    public List<GameObject> LinkCardsInfo(List<Card> cardsInfo)
    {
        List<GameObject> drawnCards = new List<GameObject>();

        for (int i = 0; i < cardsInfo.Count; i += 1)
        {
            drawnCards.Add(physicDeck.PopBack());
            CardDisplay display = drawnCards[i].GetComponent<CardDisplay>();
            display.SetCardInfo(cardsInfo[i]);
        }
        return drawnCards;
    }
}
