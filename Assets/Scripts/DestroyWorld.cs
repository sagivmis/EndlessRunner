using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWorld : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 15f;
    public float timer = 0f;

    public bool startDestroyProccess = false;

    // Start is called before the first frame update
    void UpdateTimers()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDestroyProccess)
        {
            UpdateTimers();

            if (timer >= timeToDestroy)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            startDestroyProccess = true;
        }
    }
}
