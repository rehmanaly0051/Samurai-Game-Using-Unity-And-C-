using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float raydist;
    private bool MoveR;
    public Transform groundDetect;
    Animator ami;
    playerMove pla;
   // public Transform player;
   // public bool chaseing;
   // public float chaseD;

    // Start is called before the first frame update
    private void Start()
    {
        ami = GetComponent<Animator>();
        pla = FindAnyObjectByType<playerMove>();
    }

    // Update is called once per frame
    void Update()
    {
      transform.Translate(Vector2.right * speed *Time.deltaTime);
      RaycastHit2D groundcheck = Physics2D.Raycast(groundDetect.position,Vector2.down,raydist);
        if(groundcheck.collider == false)
        {
            flip();
        }
    }
    void flip()
    {
        if (MoveR)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            MoveR = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            MoveR = true;
        }
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            flip();
        }
       else if (collision.gameObject.CompareTag("enemy"))
        {
            flip();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            pla.decH(0.01f);
        }
    }
}
