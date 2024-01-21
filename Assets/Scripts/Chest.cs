using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;

   

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            
            
                
                anim.SetTrigger("Touched");
            



        }
    }
}
