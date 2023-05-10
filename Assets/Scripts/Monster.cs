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

    public void Start(){
        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ai.SetDestination(Gate.transform.position);
        Debug.Log("Oi, consegui fazer o Start!");
    }

    public void Update(){
        float distance = ai.remainingDistance;
        if (distance < this.TargetTolerance) {
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
            Destroy(this.gameObject);
        }
    }

}
