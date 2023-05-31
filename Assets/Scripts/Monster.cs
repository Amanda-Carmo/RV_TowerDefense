using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;
// Animation
using UnityEngine.AI;


public class Monster : MonoBehaviour
{
    float HP = 100.0f;
    float maxHP = 100.0f;

    public GameObject Target;
    public GameObject Avatar;
    public Gate gate;
    public float TargetTolerance = 5.0f;
    private UnityEngine.AI.NavMeshAgent ai;
    private GameManager gameManager;
    
    private bool attack = true;

    private Animator animator;

    // [SerializeField] private Gate gate; // This is the gate that will be updated
    [SerializeField] private HealthBar _healthBar; // This is the health bar that will be updated

    public void Awake(){
        animator = Avatar.GetComponent<Animator>();

        Target = GameObject.FindGameObjectWithTag("ObjectiveTargetTag");
        gate = GameObject.FindGameObjectWithTag("Gate").GetComponent<Gate>();

        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Start(){
        animator.SetTrigger("Walk");

        ai.SetDestination(Target.transform.position);
        _healthBar.UpdateHealthBar(maxHP, HP); // Update the health bar
    }

    public void restartAttack(){
        attack = true;
        hitGate();
    }

    public void hitGate(){
        if (attack) {
            animator.SetTrigger("Attack");
            gate.hitGate(1000);
            attack = false;
            Invoke("restartAttack", 1.5f);
        }
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
            hitGate();
        }
    }

    public void TakeDamage(float dmg){
        this.HP -= dmg;
        _healthBar.UpdateHealthBar(maxHP, HP); // Update the health bar
        animator.SetTrigger("GetHit");
        this.die();
    }

    private void die(){
        if (this.HP <= 0.0f) {
            gameManager.gainScore(1000);
            gameManager.removeMonster(this.transform.parent.gameObject);
            animator.SetTrigger("Death");
        }
    }

    // Test script with mouse click
    void OnMouseDown(){
        this.TakeDamage(10.0f);
    }

}
