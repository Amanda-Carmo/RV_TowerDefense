using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerScore = 0, playerMoney = 0;
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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerScore);
    }
}
