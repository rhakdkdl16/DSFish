using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fish : MonoBehaviour
{    
    [SerializeField] float jumpVelocity;
    [SerializeField] float maxHeight;
    [SerializeField] GameObject sprite;
    [SerializeField] FlashImage flashImage;
    Rigidbody2D rb;
    bool isDead;
    bool gravity;
    

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
    public void Idle()
    {
        gravity = false;
        isDead = false;
        Animator a = sprite.GetComponent<Animator>();
         a.SetTrigger("idle");        
        StartCoroutine(SetDie());
    }
    IEnumerator SetDie()
    {
         //Color color = sprite.GetComponent<SpriteRenderer>().color;
         CircleCollider2D collider = sprite.GetComponent<CircleCollider2D>();
         float a = 0;
         float buttonTime = Time.time;   

         while(a < 3 )
         {
            float pingPong = Mathf.PingPong(Time.time * 5,1);
            Color newColor = new Color(1,1,1,pingPong);

            a =  Time.time - buttonTime ;

            sprite.GetComponent<SpriteRenderer>().color = newColor;
            collider.enabled = false;

            if (gravity == false)
            {
                rb.isKinematic = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                rb.isKinematic = false;
                gravity = true;
            }

            yield return null;
         }
        collider.enabled = true;
        sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        rb.isKinematic = false;
    }

}
