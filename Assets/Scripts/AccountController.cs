using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;
using UnityEngine.Networking;
using System.Globalization;

public class AccountController : MonoBehaviour
{
    //const string API_URL = "https://mainnet.infura.io/v3/4679ded2f85c4d84b00e6cbd933df2cd";
    const string API_URL = "https://eth-rinkeby.alchemyapi.io/v2/FyurlQP2uslRFw_nYtq6Mkz0oLmpJAN5";
    const string SERVER_URL = "http://localhost:8080";
    UIController uiController;
    ConfigLoader config;
    Gems gemsController;
    GameObject player;
    
    [DllImport("__Internal")] private static extern string WalletAddress();
    
    
    public static string walletAddress = "0xac";
    static string ethBalance = "00.00";
    float amountWanted;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        config = GetComponent<ConfigLoader>();
        walletAddress = WalletAddress();
        StartCoroutine(GetAccountBalance(walletAddress, SetEthBalance));
        uiController = GameObject.FindWithTag("Player").GetComponent<UIController>();
        uiController.SetUIWalletAddress(walletAddress);
        gemsController = player.GetComponent<Gems>();

        GetBaliBalance(walletAddress);
    }

    public string GetEthBalance() { return ethBalance; }

    public string GetWalletAddress() { return walletAddress; }
    public static IEnumerator GetAccountBalance(string address, System.Action<decimal> callback)
    {

        var getBalanceRequest = new EthGetBalanceUnityRequest(API_URL);
        yield return getBalanceRequest.SendRequest(address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());

        if (getBalanceRequest.Exception == null)
        {
            var balance = getBalanceRequest.Result.Value;
            callback(Nethereum.Util.UnitConversion.Convert.FromWei(balance, 18));
        }
        else
        {
            throw new System.InvalidOperationException("Get balance request failed");
        }

    }

    public void SetEthBalance(decimal balance)
    {
        ethBalance = (balance).ToString();
        uiController.SetUIEthBalance(ethBalance);
        uiController.SetEthBalance();
        uiController.SetEthBalanceAPIModal($"{ethBalance} E");
    }

    public IEnumerator GetBlockNumber()
    {
        var blockNumberRequest = new EthBlockNumberUnityRequest(API_URL);

        yield return blockNumberRequest.SendRequest();
        uiController.SetLatestBlockAPIModal(blockNumberRequest.Result.Value.ToString());
    }

    public void LatestBlockNumber()
    {
        StartCoroutine(GetBlockNumber());
    }

    public void GetWalletAddressAPIModal()
    {
        uiController.SetWalletAddressAPIModal(walletAddress);
    }

    public void GetEthBalanceAPIModal()
    {
        StartCoroutine(GetAccountBalance(walletAddress, SetEthBalance));
    }

    public void AllAPIRequests()
    {
        LatestBlockNumber();
        GetWalletAddressAPIModal();
        GetEthBalanceAPIModal();
    }

    public IEnumerator HttpGetRequest(string uri, string method)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (method)
            {
                case "mint":
                    switch (webRequest.result)
                    {
                        case UnityWebRequest.Result.ConnectionError:
                        case UnityWebRequest.Result.DataProcessingError:
                            Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.Success:
                            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                            uiController.IncrementGemsBy((int)(amountWanted / gemsController.GetGemPrice()));
                            break;
                    }
                    break;
                case "getBaliBalance":
                    switch (webRequest.result)
                    {
                        case UnityWebRequest.Result.ConnectionError:
                        case UnityWebRequest.Result.DataProcessingError:
                            Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.Success:
                            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                            BaliBalanceResponse response = JsonUtility.FromJson<BaliBalanceResponse>(webRequest.downloadHandler.text);
                            gemsController.SetGemBalance(response.balance);
                            break;
                    }
                    break;
            }
            
        }
    }

    public void SendEthTx(string playerAddress, string amountInEth, string playerPrivateKey)
    {
        StartCoroutine(HttpGetRequest($"{SERVER_URL}/api/mintBaliTokens/{playerAddress}/{amountInEth}/{playerPrivateKey}", "mint"));
        amountWanted = float.Parse(amountInEth) / gemsController.GetGemPrice() ;
    }

    public void GetBaliBalance(string playerAddress)
    {
        StartCoroutine(HttpGetRequest($"{SERVER_URL}/api/getBaliBalance/{playerAddress}", "getBaliBalance"));
    }
}

[System.Serializable]
public class BaliBalanceResponse
{
    public int balance;
}