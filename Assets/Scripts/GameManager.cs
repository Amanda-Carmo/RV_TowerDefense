using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int playerScore = 0, playerMoney = 0;
    private List<GameObject> livingMonsters = new List<GameObject>();
    private bool doSpawns = true;
    private Spawner spawner;
    private float spawnTimer = 10.0f;
    private int amountSpawned = 0;

    public GameObject spawnerObject;
    public int gateHealth = 10000;
    public GameObject scoreText;


    // Start is called before the first frame update
    void Awake()
    {
        spawner = spawnerObject.GetComponent<Spawner>();
    }

    void Start(){
        Invoke("spawnMonsters", spawnTimer);
    }

    private void spawnMonsters(){
        if (doSpawns) {
            int amt = 2;
            amountSpawned += 1;
            spawner.SummonMonster(amt);
        }
        Invoke("spawnMonsters", spawnTimer);
    }

    public void gainScore(int gain){
        playerMoney += gain;
        playerScore += gain;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + playerScore.ToString();
    }

    public bool spendMoney(int amount){
        if (amount < playerMoney) {
            playerMoney -= amount;
            return true;
        }
        return false;
    }

    public void addMonster(GameObject monster){
        livingMonsters.Add(monster);
    }

    public void removeMonster(GameObject monster){
        livingMonsters.Remove(monster);
        Destroy(monster);
    }

    public void hitGate(int damage){
        // Debug.Log("Gate hit! " + gateHealth.ToString() + " -> " + (gateHealth-damage).ToString());
        gateHealth -= damage;
        gameOver();
    }

    public void gameOver(){
        if (gateHealth <= 0) {
            doSpawns = false;
            while (livingMonsters.Count > 0) {
                removeMonster(livingMonsters[0]);
            }
            Debug.Log("Game Over!");
            Debug.Log("Player Score: " + playerScore.ToString());
        }
    }

    public int getScore(){
        return playerScore;
    }
}
