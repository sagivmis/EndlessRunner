using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInstantiate : MonoBehaviour
{
    [SerializeField] GameObject worldPrefab;
    [SerializeField] Transform groundEndTransform;
    [Range(-100, 100)] [SerializeField] float offsetX = 48.27f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InstantiateWorld(Transform groundEnd)
    {
        Vector2 spawnPos = new Vector2(groundEnd.position.x + offsetX, groundEnd.position.y);
        Instantiate(worldPrefab, spawnPos, groundEnd.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InstantiateWorld(groundEndTransform);
        }
    }
}
