using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fish : MonoBehaviour
{
    [SerializeField] Sprite deadFish;
    [SerializeField] float jumpVelocity;
    [SerializeField] float maxHeight;
    [SerializeField] GameObject sprite;
    [SerializeField] FlashImage flashImage;
    Rigidbody2D rb;
    bool isDead;

    

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
     
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight) 
        {
            if(!isDead && rb.isKinematic == false)
            rb.velocity = new Vector2(0, jumpVelocity);
        }
        float angle;
        //물고기 회전
        if (isDead)
        {
            angle = -90;
            
        }
        else
        {
            angle = Mathf.Atan2(rb.velocity.y, 10) * Mathf.Rad2Deg;   //물고기가회전되야할 방향
        }
        
        sprite.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
 
        isDead = true;
        Camera.main.SendMessage("Shake");
        Animator a = sprite.GetComponent<Animator>();
        a.SetTrigger("Dead");
        flashImage.StartFlash();
    }
    public void SetKinematic(bool value)
    {
        rb.isKinematic = value;
    }

}
