using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChildCount : MonoBehaviour
{
    void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
