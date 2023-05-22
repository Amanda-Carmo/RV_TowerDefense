using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar; // This is the health bar that will be updated
    [SerializeField] private GameManager _gameManager; // This is the game manager that will be updated

    public int gateHealth = 10000;
    public int maxGateHealth = 10000;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the health bar
        _healthBar.UpdateHealthBar(maxGateHealth, gateHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitGate(int damage){
        // Debug.Log("Gate hit! " + gateHealth.ToString() + " -> " + (gateHealth-damage).ToString());
        gateHealth -= damage;
        _healthBar.UpdateHealthBar(maxGateHealth, gateHealth); // Update the health bar
        _gameManager.gameOver(gateHealth <= 0);
    }


    void OnMouseDown(){
        Debug.Log("Clicked!");
        this.hitGate(1000);
    }
}
