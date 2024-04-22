using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    public flying_enemy[] enemyArray;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            foreach (flying_enemy enemy in enemyArray){
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            foreach (flying_enemy enemy in enemyArray){
                enemy.chase = false;
            }
        }
    }
    
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
