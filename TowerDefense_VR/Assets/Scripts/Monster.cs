using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    float HP = 100.0f;

    public GameObject Gate;
    public UnityEngine.AI.NavMeshAgent ai;

    public void Start(){
        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
