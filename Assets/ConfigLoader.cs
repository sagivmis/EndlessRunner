using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    private string gameOwnerAddress;
    private string baliTokenAddress;
    private string customTokenAddress;
    private float baliTokenPrice;
    private float custonTokenPrice;

    public TextAsset configJSON;
    public Config config = new Config();
    public string GetOwnerAddress() { return gameOwnerAddress; }
    public string GetBaliTokenAddress() { return baliTokenAddress; }
    public string GetCustonToeknAddress() { return customTokenAddress; }
    public float GetCustonToeknPrice() { return custonTokenPrice; }
    public float GetBaliTokenPrice() { return baliTokenPrice; }
    // Start is called before the first frame update
    void Start()
    {
        configJSON = Resources.Load<TextAsset>("cfg");
        config = JsonUtility.FromJson<Config>(configJSON.text);
        gameOwnerAddress = config.gameOwnerAddress;
        baliTokenAddress = config.baliTokenAddress;
        customTokenAddress = config.customTokenAddress;
        custonTokenPrice = config.custonTokenPrice;
        baliTokenPrice = config.baliTokenPrice;
    }

}

[System.Serializable]
public class Config
{
    public string gameOwnerAddress;
    public string baliTokenAddress;
    public string customTokenAddress;
    public float baliTokenPrice;
    public float custonTokenPrice;
}