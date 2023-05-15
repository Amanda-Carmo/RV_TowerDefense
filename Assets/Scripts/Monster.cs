using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    float HP = 100.0f;

    public GameObject Gate;
    public float TargetTolerance = 5.0f;
    private UnityEngine.AI.NavMeshAgent ai;
    private bool StartedWalking = false;
    private GameObject gameManager;

    public void Awake(){
        Gate = GameObject.FindGameObjectWithTag("ObjectiveTargetTag");
        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameManager = FindGameObjectWithTag("GameManager");
    }

    public void Start(){
        ai.SetDestination(Gate.transform.position);
        StartedWalking = true;
        Debug.Log(Gate.transform.position);
    }

    public void LateUpdate(){
        float distance = ai.remainingDistance;
        // Debug.Log("Gate position: " + Gate.transform.position.ToString());
        // Debug.Log("Monster position: " + gameObject.transform.position.ToString());
        // Debug.Log("Remaining distance: " + ai.remainingDistance.ToString());
        if ((distance < this.TargetTolerance) && distance != 0) {
            // Debug.Log("Cheguei no alvo!");
            ai.isStopped = true;
            ai.SetDestination(this.gameObject.transform.position);
        }
    }

    public void TakeDamage(float dmg){
        this.HP -= dmg;
        this.die();
    }

    private void die(){
        if (this.HP <= 0.0f) {
            gameManager.gainScore(1000);
            Destroy(this.gameObject);
        }
    }

}
