using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool canJump = true;
    public Animator animator;
    public float basemovementSpeed = 7f; // Adjustable movement speed

    public float movementSpeed = 7f; // Adjustable movement speed
    public float jumpForce = 14f;    // Adjustable jump force
    public bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove){
            return;
        }
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space)) // Probably set it so Space is the dash or another machanic. 
        //                                      and then Use W for jumping. can use "Input.GetAxisRaw("Horizontal"); > 0" 
        //                                       so that it also uses the arrow keys
        {
            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){ 
            movementSpeed = basemovementSpeed * 5;
            // add a countdown or Cooruitine to reset back to base
        }
         if(Input.GetKeyUp(KeyCode.S)){
            movementSpeed = basemovementSpeed;
         } 
        // animator.SetInteger("Speed", Mathf.Abs((int)rb.velocity.x));
    }

    public void SetMovement(bool canMove) {
        this.canMove = canMove;
    }
}
