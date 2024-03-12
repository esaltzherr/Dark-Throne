using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;
    public bool playerInDoor = false;
    public string nextScene;

    // Update is called once per frame
    // private void OnTriggerEnter2D(Collider2D other){
    //     print("Trigger Entered");
        
    //     if(other.tag == "Player"){
    //         print("Switching Scene to " + sceneBuildIndex);
    //         SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    //    } 
    // }

    // public void update(){

      

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerInDoor)
        {
            Debug.Log("NEXT Level");
            SceneManager.LoadScene(nextScene);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerInDoor = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        playerInDoor = false;
    }

}

