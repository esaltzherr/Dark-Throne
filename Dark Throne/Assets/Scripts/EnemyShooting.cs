using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public Animator animator;

    private float timer = 1;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position,player.transform.position);
        //Debug.Log(distance);

        if(distance < 10)
        {
            timer += Time.deltaTime;
            if(timer > 2)
            {
                timer = 0;
                animator.SetTrigger("Attack");
                shoot();
            }
        }
        Flip();
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }



    void shoot(){
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
