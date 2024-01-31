using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apricity{

public class GameManager : MonoBehaviour
{
    public int playerHealth = 3;

    public List<GameObject> healthBarIcon = new List<GameObject>();

    public static GameManager instance{get; private set;}

    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this);
        }else{
            instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PlayerReceiveDamage(int damage){
        playerHealth -= damage;
        Debug.Log("ooche, player remain health: " + playerHealth);
    }
}
}