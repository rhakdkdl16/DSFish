using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] float minPosY;
    [SerializeField] float maxPositionY;
    private void Start()
    {
        ChangePosition();
    }

    public void ChangePosition()
    {
        float positionY = Random.Range(minPosY, maxPositionY);
        transform.localPosition = new Vector3(transform.localPosition.x, positionY, transform.localPosition.z);
    }
}
