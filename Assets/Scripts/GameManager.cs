using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerScore = 0, playerMoney = 0;
    private List<GameObject> livingMonsters = new List<GameObject>();
    public int gateHealth = 10000;
    private bool doSpawns = true;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void gainScore(int gain){
        playerMoney += gain;
        playerScore += gain;
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

    public bool isSpawning(){
        return doSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerScore);
    }

    public int getScore(){
        return playerScore;
    }
}
