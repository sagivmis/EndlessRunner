using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 4f;
    public float timer = 0;
    // Start is called before the first frame update
    void UpdateTimers()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();

        if (timer >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
