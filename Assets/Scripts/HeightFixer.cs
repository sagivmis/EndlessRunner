using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightFixer : MonoBehaviour
{
    public float startingYAxis;
    void Start()
    {
        startingYAxis = transform.position.y;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, startingYAxis);
    }


}
