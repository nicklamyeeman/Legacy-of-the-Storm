using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public int numberOfSlots = 5;
    public int numberOfCards = 20;
    public float marketMaxWidth = 38f;

    [SerializeField] private GameObject cardPattern;
    [SerializeField] private GameObject slotPattern;

    private GameObject[] slots;
    public List<GameObject> marketCards = new List<GameObject>();

    void Start()
    {
        CreateMarketSlots();
    }

    public void GenerateDeck()
    {
        for (double i = 0, y = -0.3f; i < numberOfCards; i += 1, y += 0.05)
        {
            marketCards.Add(Instantiate(cardPattern, new Vector3(transform.position.x, transform.position.y + (float)y, 0), cardPattern.transform.rotation, transform));
            marketCards[(int)i].name = "MarketCard" + i;
        }
    }

    private void CreateMarketSlots()
    {
        slots = new GameObject[numberOfSlots];
        for (int i = 0; i < numberOfSlots; i += 1)
        {
            slots[i] = Instantiate(slotPattern, new Vector3(transform.position.x + (marketMaxWidth) - i * (marketMaxWidth / numberOfSlots), transform.position.y, 0), Quaternion.identity, transform);
        }
    }

    void Update()
    {
        foreach(GameObject slot in slots)
        {
            SlotManager slotManager = slot.GetComponent<SlotManager>();
            if (slotManager.card == null)
            {
                if (marketCards.Count >= 1)
                {
                    CardAnimationManager cardAnimationManager = marketCards[marketCards.Count - 1].GetComponent<CardAnimationManager>();
//                    cardAnimationManager.CardToSlotAnim( 22.94f, slotManager.gameObject.transform.position);
                    slotManager.SetCard(marketCards[marketCards.Count - 1]);
                    marketCards.RemoveAt(marketCards.Count - 1);
                }
                //GameObject card = marketCards[marketCards.Count - 1];
            }
        }
    }
}
