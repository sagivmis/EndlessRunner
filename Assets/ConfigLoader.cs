using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    public string gameOwnerAddress;
    public string baliTokenAddress;
    public string customTokenAddress;
    public float tokenPrice;

    public TextAsset configJSON;
    public Config config = new Config();

    // Start is called before the first frame update
    void Start()
    {
        configJSON = Resources.Load<TextAsset>("cfg");
        config = JsonUtility.FromJson<Config>(configJSON.text);
        gameOwnerAddress = config.gameOwnerAddress;
        baliTokenAddress = config.baliTokenAddress;
        customTokenAddress = config.customTokenAddress;
        tokenPrice = config.tokenPrice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Config
{
    public string gameOwnerAddress;
    public string baliTokenAddress;
    public string customTokenAddress;
    public float tokenPrice;
}