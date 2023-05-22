using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DummyController : MonoBehaviour
{
    private GameManager gameManager;

    void Awake() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Called when dummy is hit
    private void ApplyDamage()
    {
        Debug.Log("Dummy Hit");
        gameManager.makeNight();
        Destroy(this.gameObject);
    }

    public void OnMouseDown(){
        Debug.Log("Clicado dummy!");
        this.ApplyDamage();
    }

}




