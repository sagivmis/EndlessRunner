using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiate : MonoBehaviour
{
    [Tooltip("Add several for randomness")]
    [SerializeField] GameObject[] prefabs; // add several for randomness
    [SerializeField] GameObject warningPrefab; // add several for randomness
    [Space(30)]
    [SerializeField] Transform topBorder;
    [SerializeField] Transform bottomBorder;
    [Space]
    [SerializeField] Transform parent;
    [Range(-100, 100)]
    [SerializeField] float xAxisOffset = 0f;    
    [Range(-100, 100)]
    [SerializeField] float warningXAxisOffset = 0f;
    [Range(-100, 100)]
    [SerializeField] float yBotOffset = 0f;
    [Range(-100, 100)]
    [SerializeField] float yTopOffset = 0f;
    [SerializeField] bool debugLogs = false;




    [Header("Config")]
    public float topYborder;
    public float bottomYborder;
    public float timeToInstantiate = 2.5f;
    public float warningTimer = -1f;
    public float timer = Mathf.Infinity;
    private bool updateTimers = true;

    private void Start()
    {
        topYborder = topBorder.position.y;
        bottomYborder = bottomBorder.position.y;
        if (!parent) Debug.LogError("No parent assigned.");
        if (!topBorder || !bottomBorder) Debug.LogError("No borders assigned.");
        if (warningTimer < 0) Debug.LogError($"{gameObject.name}'s Warning time not assinged.");
    }
    void UpdateTimers()
    {
        if (updateTimers) { timer += Time.deltaTime; }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        if (timer >= timeToInstantiate)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            if (prefab.name == "Laser") warningTimer = 3f;
            if(debugLogs) print($"Starting {prefab.name} Instancing");
            timer = 0;
            updateTimers = false;
            if (warningPrefab != null) StartCoroutine(InstancePrefab(prefab, warningTimer));
            else if(warningPrefab == null) Instance(prefab);
        }
    }

    IEnumerator InstancePrefab(GameObject prefab, float waitBeforeInstantiate)
    {
        // TO DO BEFORE INSTANTIATING
        
        float yAxis = Random.Range(topYborder + yTopOffset, bottomYborder + yBotOffset);
        Vector2 spawnLocation = new Vector2(transform.position.x - warningXAxisOffset, yAxis);

        GameObject warningInstance = Instantiate(warningPrefab, spawnLocation, transform.rotation);
        if (warningPrefab) warningInstance.transform.parent = parent;
        
        // WAIT BEFORE INSTANTIATING

        yield return new WaitForSeconds(waitBeforeInstantiate);


        // DESTROY PREVIOUS

        if(debugLogs) print($"{prefab.name} Done waiting.\n Waited {warningTimer} seconds");
        Destroy(warningInstance);

        spawnLocation = new Vector2(transform.position.x - xAxisOffset, yAxis);
        
        // INSTANCE PREFAB
         
        GameObject instance = Instantiate(prefab, spawnLocation, transform.rotation);
        instance.transform.parent = parent;
        updateTimers = true;
    }

    public void Instance(GameObject prefab)
    {

        // TO DO BEFORE INSTANTIATING

        float yAxis = Random.Range(topYborder + yTopOffset, bottomYborder + yBotOffset);
        Vector2 spawnLocation = new Vector2(transform.position.x - warningXAxisOffset, yAxis);

        spawnLocation = new Vector2(transform.position.x - xAxisOffset, yAxis);

        // INSTANCE PREFAB

        GameObject instance = Instantiate(prefab, spawnLocation, transform.rotation);
        instance.transform.parent = parent;
        updateTimers = true;
    }
}
