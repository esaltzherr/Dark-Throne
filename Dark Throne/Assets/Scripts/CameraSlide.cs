using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer player;
    public Vector2 inbetweenSpace = new Vector2(19.9f, 6f);
    
    
    void Start()
    {
        // player = GetComponent<SpriteRenderer>();
        // Debug.Log(player.size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnCollisionExit2D(Collision2D other) {
    //     Debug.Log(other.transform.name);
    //     // if other == player 
    // }
    private void OnTriggerExit2D(Collider2D other) {
        
        Debug.Log(other.transform.tag);
        Bounds cameraBounds = this.GetComponent<Collider2D>().bounds;
        if (other.CompareTag("Player")){
            Debug.Log("Moving Camera");
           

            // Move off the right
            if (other.bounds.min.x >= cameraBounds.max.x){
                transform.Translate(Vector2.right * inbetweenSpace.x);
                // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
                other.transform.Translate(Vector2.right * .1f);
            }
            // Move off the left
            else if(other.bounds.max.x <= cameraBounds.min.x) {
                transform.Translate(Vector2.right * -inbetweenSpace.x);
                // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
                other.transform.Translate(Vector2.right * -.1f);
            }
            // Move off the up
            else if(other.bounds.max.y <= cameraBounds.min.y) {
                transform.Translate(Vector2.up * -inbetweenSpace.y);
                // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
                other.transform.Translate(Vector2.up * -.1f);
            }
            // Move off the down
            else if(other.bounds.min.y >= cameraBounds.max.y) {
                transform.Translate(Vector2.up * inbetweenSpace.y);
                // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
                other.transform.Translate(Vector2.up * .1f);
            }




            // Probably Pause the game so enemies arent moving and you dont keep moving, maybe not for more seemless.

            // moves to the right 2
            //transform.Translate(Vector2.right * 2f);
        }

    }
}
