using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    GameManager _instance = null;

    // Singleton
    public GameManager GetInstance(){
        if (this._instance == null) {
            this._instance = new GameManager();
        }
        return this._instance;
    }

}
