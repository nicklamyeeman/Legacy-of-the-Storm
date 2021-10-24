using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int golds = 0;
/*

    public Deck deck;       // TO ADD

*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool useGold(int toUse)
    {
        if (toUse <= golds) {
            golds -= toUse;
            return true;
        } else
            return false;
    }

    void addGold(int toAdd)
    {
        golds += toAdd;
    }
}
