using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private GameManager gameManager;
    public GameObject MonsterPrefab;

    void Awake(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SummonMonster", 2.0f, 10.0f);
    }

    public void SummonMonster(){
        if (gameManager.isSpawning()) {
            Vector3 position = this.gameObject.transform.position;
            Quaternion rotation = this.gameObject.transform.rotation;
            position[2] += Random.Range(-20.0f, 20.0f);

            Instantiate(MonsterPrefab, position, rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
