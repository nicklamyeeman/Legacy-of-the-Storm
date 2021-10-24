using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Scheme :
 * - Initiate player
 * - Initiate Ennemies
 * - Card system
 * - Start game mecanics (phases, turns ...)
*/

public class GameManager : MonoBehaviour
{
    public float animationTime = 3f;

    [SerializeField] private PlayerManager player;

    void Start() {
        InitGame();
    }

    void InitGame()
    {
        player.GeneratePlayerDeck();
        player.DrawCards();
    }
}