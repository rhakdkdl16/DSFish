using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour
{
    
    private void Start()
    {
        gameObject.SetActive(false);   
    }
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            float alpha = GetComponent<Image>().color.a;
            alpha -= 0.03f;
            GetComponent<Image>().color = new Color(1, 1, 1, alpha);

            if(alpha < 0.01f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void StartFlash()
    {
        gameObject.SetActive(true);

    }
}
