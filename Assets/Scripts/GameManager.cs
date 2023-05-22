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

    public Material daySkybox, nightSkybox;

    public GameObject spawnerObject;
    public GameObject scoreText;
    public GameObject dummyPrefab;
    public GameObject dummySpawnPosition;


    [SerializeField] private Gate gate;

    // private bool gateHealthZero = false;


    //=============================//
    //=======Initialization========//
    //=============================//

    void Awake()
    {
        spawner = spawnerObject.GetComponent<Spawner>();
        makeDay();
    }

    void Start(){
        Invoke("spawnMonsters", spawnTimer);
    }

    //=============================//
    //=======MonsterSpawning=======//
    //=============================//

    private void spawnMonsters(){
        if (doSpawns) {
            int amt = 2;
            amountSpawned += 1;
            spawner.SummonMonster(amt);
        }
        Invoke("spawnMonsters", spawnTimer);
    }

    public void addMonster(GameObject monster){
        livingMonsters.Add(monster);
    }

    public void removeMonster(GameObject monster){
        livingMonsters.Remove(monster);
        Destroy(monster);
    }

    public void destroyAllMonsters(){
        while (livingMonsters.Count > 0) {
            removeMonster(livingMonsters[0]);
        }
    }

    //=============================//
    //========ScoreTracking========//
    //=============================//

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

    public void gameOver(bool gateHealthZero){
        Debug.Log(gate.gateHealth.ToString() + " / " + gate.maxGateHealth.ToString());

        if (gateHealthZero) {
            doSpawns = false;
            destroyAllMonsters();
            Debug.Log("Game Over!");
            Debug.Log("Player Score: " + playerScore.ToString());
        }
    }

    public int getScore(){
        return playerScore;
    }

    //=============================//
    //========DayNightCycle========//
    //=============================//

    public void makeDay() {
        doSpawns = false;
        destroyAllMonsters();
        RenderSettings.skybox = this.daySkybox;
        spawnDummy();
    }

    public void makeNight() {
        doSpawns = true;
        RenderSettings.skybox = this.nightSkybox;
        // Invoke("SpawnMonsters", spawnTimer);
    }

    private void spawnDummy() {
        Vector3 position = dummySpawnPosition.transform.position;
        Quaternion rotation = dummySpawnPosition.transform.rotation;
        Instantiate(dummyPrefab, position, rotation);
    }

}
