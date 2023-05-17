using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private GameManager gameManager;
    public GameObject MonsterPrefab;
    public List<GameObject> spawnLocations; 

    void Awake(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SummonMonster(int NumMonsters){
        NumMonsters = Mathf.Min(NumMonsters, spawnLocations.Count);
        for (int i = 0; i < NumMonsters; i++){
            int index = Random.Range(0, spawnLocations.Count);
            GameObject obj = spawnLocations[index];
            Vector3 position = obj.transform.position;
            Quaternion rotation = obj.transform.rotation;
            gameManager.addMonster(Instantiate(MonsterPrefab, position, rotation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
