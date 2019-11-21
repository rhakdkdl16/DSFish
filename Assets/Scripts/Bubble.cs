using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    int speed;
    [SerializeField] float startPosition;   //시작하게될 바닥위치
    [SerializeField] float endPosition;    // 끝나게되는위치  => startPosition
    private void Start()
    {
        speed = Random.Range(1, 10);
    }

    private void Update()
    {
        
        transform.Translate(0, speed * Time.deltaTime, 0);

        if (transform.position.y >= endPosition)  //endpsotion 을 넘었을경우
        {
            transform.Translate(0,  startPosition *2 , 0);   //< 12,0,0
            
            //SendMessage("ChangePosition", SendMessageOptions.DontRequireReceiver);
        }
    }
}
