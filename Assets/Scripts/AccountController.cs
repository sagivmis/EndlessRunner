using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class AccountController : MonoBehaviour
{

    UIController uiController;
    ConfigLoader config;
	[DllImport("__Internal")] private static extern string WalletAddress();
    public static string walletAddress = "0xac";
    static string ethBalance = "00.00";

    void Start()
    {
        config = GetComponent<ConfigLoader>();
        walletAddress = WalletAddress();
        StartCoroutine(getAccountBalance(walletAddress, SetEthBalance));
        uiController = GameObject.FindWithTag("Player").GetComponent<UIController>();
        uiController.setUIWalletAddress(walletAddress);
    }

    public string GetEthBalance() { return ethBalance; }

    public string GetWalletAddress() { return walletAddress; }
    public static IEnumerator getAccountBalance(string address, System.Action<decimal> callback)
    {

        var getBalanceRequest = new EthGetBalanceUnityRequest("https://mainnet.infura.io/v3/4679ded2f85c4d84b00e6cbd933df2cd");
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
        uiController.setUIEthBalance(ethBalance);
        uiController.SetEthBalance();
    }





}