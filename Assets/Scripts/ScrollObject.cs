using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float startPosition;   //시작하게될 바닥위치
    [SerializeField] float endPosition;    // 끝나게되는위치  => startPosition
    
    private void Update()
    {
  
        transform.Translate(-1* speed * Time.deltaTime, 0, 0);

        if(transform.position.x <= endPosition)  //endpsotion 을 넘었을경우
        {
            transform.Translate(-1 * (endPosition - startPosition), 0, 0);   //< 12,0,0

            SendMessage("ChangePosition", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void speedUp()
    {
        speed += 0.2f;
    }

}
