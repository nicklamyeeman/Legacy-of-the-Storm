using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCard(GameObject newCard)
    {
        card = newCard;
//        card.transform.position = transform.position;
//        card.transform.SetParent(transform);
    }
}
