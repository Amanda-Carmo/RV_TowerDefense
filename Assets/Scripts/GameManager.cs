using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int playerScore = 0, playerMoney = 0;
    private List<GameObject> livingMonsters = new List<GameObject>();
    private bool isNight = true, currStageFinished = true;
    private Spawner spawner;

    public Material daySkybox, nightSkybox;

    public GameObject spawnerObject;
    public GameObject scoreText;
    public GameObject dummyPrefab;
    public GameObject dummySpawnPosition;


    [SerializeField] private Gate gate;

    class StageStep {
        public int spawnAmount;
        public float delay;
        public List<int> locations = null;
        public StageStep(int amt, float delay){
            spawnAmount = amt;
            this.delay = delay;
        }
        public StageStep(int amt, float delay, List<int> locations){
            spawnAmount = amt;
            this.delay = delay;
            this.locations = locations;
        }
    }

    class Stage {
        private List<StageStep> stepList;
        public Stage(List<StageStep> stepList){
            this.stepList = stepList;
        }

        public StageStep getStageStep(int i){
            if (i > stepList.Count) {
                Debug.Log("Freeplay enabled!");
            }
            return (stepList[i % stepList.Count]);
        }

        public int getStageLength(){
            return this.stepList.Count;
        }
    }

    private int currStage = 0, currStageStep = 0;
    private List<Stage> stages = new List<Stage> {
        new Stage(new List<StageStep> {new StageStep(1, 5.0f), new StageStep(1, 3.0f), new StageStep(1, 3.0f), new StageStep(1, 3.0f)}),
        new Stage(new List<StageStep> {new StageStep(2, 5.0f), new StageStep(2, 6.0f), new StageStep(2, 6.0f), new StageStep(1, 3.0f)}),
        new Stage(new List<StageStep> {new StageStep(2, 5.0f), new StageStep(2, 4.5f), new StageStep(3, 10.0f)}),
        new Stage(new List<StageStep> {new StageStep(3, 5.0f), new StageStep(1, 2.0f), new StageStep(1, 2.0f), new StageStep(1, 2.0f), new StageStep(1, 2.0f)}),
        new Stage(new List<StageStep> {new StageStep(3, 5.0f), new StageStep(3, 5.0f), new StageStep(3, 5.0f)}),
    };

    //=============================//
    //=======Initialization========//
    //=============================//

    void Awake()
    {
        spawner = spawnerObject.GetComponent<Spawner>();
        makeDay();
    }

    // void Start(){
    //     Invoke("spawnMonsters", 2.0f); // Always wait at least 2 seconds before spawning the first wave, even though it'll probably still be Day Time
    // }

    //=============================//
    //=======MonsterSpawning=======//
    //=============================//

    private float Timer = 0.0f;
    void Update() {
        Timer += Time.deltaTime;       
        
        // If we're spawning monsters (i.e. Night Time)
        if (isNight) {

            // Identify game state
            Stage stage = this.stages[this.currStage];
            StageStep step = stage.getStageStep(this.currStageStep);
            
            int amt = step.spawnAmount;
            float spawnDelay = step.delay;
            
            // If time elapsed to meet Stage specs, spawn enemies
            if (Timer >= spawnDelay && !this.currStageFinished){
                // Log day and step
                Debug.Log("Spawning step " + (this.currStageStep + 1).ToString() + " on stage " + (this.currStage + 1).ToString());
                
                Timer = 0.0f;
                spawner.SummonMonster(amt);
                // Update stage and step counters
                if (isFinalStep()) {
                    this.currStageFinished = true;
                    this.currStageStep = 0;
                    if (isFinalStage()){
                        this.currStage = 0;
                        isNight = false;
                    } else {
                        this.currStage ++;
                    }
                } else {
                    this.currStageFinished = false;
                    this.currStageStep ++;
                }
            }
            // Debug.Log("Finished: " + this.currStageFinished.ToString());
            // Debug.Log("Living Monsters: " + this.livingMonsters.Count.ToString());
            // If we're at the final step of the stage, and all monsters are dead, make day
            if (this.currStageFinished && (this.livingMonsters.Count == 0 /*|| Timer > 20.0f*/)) {
                makeDay();
            }
        }
    }

    public bool isFinalStep(){
        return (this.currStageStep == this.stages[currStageStep].getStageLength()-1);
    }

    public bool isFinalStage(){
        return (this.currStage == this.stages.Count-1);
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
            isNight = false;
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
        isNight = false;
        destroyAllMonsters();
        this.currStageFinished = true;
        RenderSettings.skybox = this.daySkybox;
        spawnDummy();
    }

    public void makeNight() {
        this.isNight = true;
        this.Timer = 0.0f;
        this.currStageFinished = false;
        RenderSettings.skybox = this.nightSkybox;
        // Invoke("SpawnMonsters", spawnTimer);
    }

    private void spawnDummy() {
        Vector3 position = dummySpawnPosition.transform.position;
        Quaternion rotation = dummySpawnPosition.transform.rotation;
        Instantiate(dummyPrefab, position, rotation);
    }

}
