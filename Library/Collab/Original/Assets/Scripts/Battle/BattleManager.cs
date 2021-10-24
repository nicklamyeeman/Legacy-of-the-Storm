using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Scheme :
 * - Initiate player
 * - Initiate Ennemies
 * - Card system
 * - Start game mecanics (phases, turns ...)
*/

public class BattleManager : MonoBehaviour
{
    public List<Enemy> enemies;
    private Player player;

    private EntitiesPrefab prefabs;

    private int Phase = 0;

    public Canvas ending;
    private bool hasEnded;

    public Text cardText;
    private Card _card;
    private int pos;

    private bool preTurn;
    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start() {
        prefabs = GetComponent<EntitiesPrefab>();
        // TODO : initialisation of the player and generating ennemies here
        GeneratePlayer();
        GenerateEnemies(2);
        preTurn = true;
        hasEnded = false;
        ending.gameObject.SetActive(false);

        GameObject backScene = GameObject.FindWithTag("BackScene");
        if (backScene != null)
            sceneLoader = backScene.GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update() {
        // TODO : Handling the battle phases : Player turn; passive ending turn effects; ennemies turn; shuffle cards, draw ...
        if (preTurn == true)
            PredictEnemiesTurn();            
        if (Phase == 0) {
            PlayerTurn();
        }
        else if (Phase == 1)
            EnemiesTurn();
        else if (Phase == 2)
            PassiveEffects();
        if (hasEnded == true && Input.GetKeyDown(KeyCode.Space))
            sceneLoader.back();
        CheckEndBattle();
    }

    private bool AreEnemiesDead()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.CurrentHealth > 0)
                return false;
        }
        return true;
    }

    private bool IsPlayerAlive()
    {
        if (player.CurrentHealth > 0)
            return true;
        return false;
    }

    private void TriggerEnding(Entity win)
    {
        ending.gameObject.SetActive(true);
        if (win == null)
        {
            ending.GetComponentInChildren<Text>().text = "You Loose !";

        } else
        {
            ending.GetComponentInChildren<Text>().text = "You Win !";
        }
        hasEnded = true;
    }

    void CheckEndBattle()
    {
        if (AreEnemiesDead() == true)
            TriggerEnding(player);
        if (IsPlayerAlive() == false)
            TriggerEnding(null);
    }

    // Handle the player's turn
    void PlayerTurn()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //PlayerDeck.SendHandToDiscard();
            print("End player turn");
            Phase = 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
            return;
        if (Input.GetKeyDown(KeyCode.F)) {
            enemies[0].GetComponent<Enemy>().SetAnimatorTrigger("Hurt");
            enemies[0].GetComponent<HealthManager>().TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            enemies[1].GetComponent<Enemy>().SetAnimatorTrigger("Hurt");
            enemies[1].GetComponent<HealthManager>().TakeDamage(10);
        }
    }

    void PredictEnemiesTurn()
    {
        if (preTurn == true) {
            foreach (Enemy enemy in enemies) {
                enemy.PrepareTurn();
            }
        }
        preTurn = false;
    }

    // Handle the ennemie's turn
    void EnemiesTurn()
    {
        print("Ennemies animations");
        foreach (Enemy enemy in enemies) {
            // Enemy manager = enemy.GetComponent<Enemy>();
            // Enemy yep = enemy.GetComponent<Enemy>();
            enemy.MakeAction(player);
        }
        Phase = 2;
    }

    // Handle the passive effects of the character
    void PassiveEffects()
    {
        print("Passive effects animations");
        Phase = 0;
        player.action = 3;
        preTurn = true;
        player.GetComponentInChildren<ActionPoints>().RefreshCount();
    }

    void GenerateEnemies(int number)
    {
        // TODO Randomize type of fight
        for (int i = 0; i < number; i++) {
            int rand = Random.Range(0, 3);
            print(rand);
            Transform transformEnemies = GameObject.FindGameObjectWithTag("Enemies").transform;
            Transform child = transformEnemies.transform.GetChild(i).transform;
            Enemy enemyObj = null;
            if (rand == 0) {
                enemyObj = prefabs.GenerateEnemy(prefabs.heavyBandit, child);
            } else if (rand == 1) {
                enemyObj = prefabs.GenerateEnemy(prefabs.lightBandit, child);
            } else if (rand == 2) {
                enemyObj = prefabs.GenerateEnemy(prefabs.skeleton, child);
            }
            // enemies.Add(enemyObj);
            // GameObject enemyObj = Instantiate(prefabs.heavyBandit, child.position, child.rotation);
            // enemyObj.transform.SetParent(child);
            // enemies[i] = enemyObj.GetComponent<Enemy>();
            enemies.Add(enemyObj);
        }
    }

    void GeneratePlayer()
    {
        Transform transform = GameObject.FindGameObjectWithTag("Player").transform;
        Player playerObj = prefabs.GeneratePlayer(prefabs.player, transform);
        // GameObject playerObj = Instantiate(prefabs.player, transform.position, transform.rotation);
        // playerObj.transform.SetParent(transform);
        // player = playerObj.GetComponent<Player>();
        // player = playerObj;
        player = playerObj;
        print("Player Generated");
    }



    // Handle the targetting with the cards currently held by the player
    public void OnDragCard(Card card)
    {
        if (card.target == Card.Target.ALLY)
        {
            TargetAble target = player.GetComponentInChildren<TargetAble>();
            if (target)
                target.PlaceTarget();
        }
        else
        {
            foreach (Enemy enemy in enemies)
            {
                TargetAble target = enemy.GetComponentInChildren<TargetAble>();
                if (target)
                {
                    target.PlaceTarget();
                }
            }
        }
    }

    // Reset all targets
    public void OnDragEnd()
    {
        TargetAble target = player.GetComponentInChildren<TargetAble>();
        if (target)
            target.RemoveTarget();
        foreach (Enemy enemy in enemies)
        {
            target = enemy.GetComponentInChildren<TargetAble>();
            if (target)
                target.RemoveTarget();
        }
    }

    public void OnUseCard(Card card, GameObject target)
    {
        //print("use card with " + _card.name + " on " + target.name);
        //if (player.action == 0)
        //{
        //    print("Can't do specified action");
        //    return;
        //}
        //if (_card.type == Card.Type.ATTACK)
        //{
        //    if (_card.zone)
        //    {
        //        print("Area dmg");
        //        enemies[0].GetComponent<HealthManager>().TakeDamage(_card.stat);
        //        enemies[1].GetComponent<HealthManager>().TakeDamage(_card.stat);
        //    }
        //    else
        //    {
        //        print("ATTACK with " + _card.stat);
        //        target.GetComponent<HealthManager>().TakeDamage(_card.stat);
        //    }
        //}
        //if (_card.type == Card.Type.ARMOR)
        //{
        //    player.GetComponent<HealthManager>().ArmorUp(_card.stat);
        //    print("ARMOR UP " + _card.stat + " On ");
        //}
        //player.action -= 1;
        //player.GetComponentInChildren<ActionPoints>().RefreshCount();
    }

    public void SelectCard(Card card, int nb)
    {
        cardText.text = "Selected card : " + card.name;
        int pos = nb;
        _card = card;
    }

}
