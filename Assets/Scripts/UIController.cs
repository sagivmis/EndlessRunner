using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.JsonRpc.UnityClient;

public class UIController : MonoBehaviour
    {
    GameObject player;
    ScoreSystem scoreSystem;
    Cainos.CharacterController cc;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject balancePanel;
    [SerializeField] Text menuScoreText;
    [SerializeField] TMP_Text walletAddressText;
    [SerializeField] TMP_Text ethBalanceText;
    [SerializeField] Button pauseButton;
    [SerializeField] bool debugLogs = true;
    [DllImport("__Internal")] private static extern string WalletAddress();
    [DllImport("__Internal")] private static extern string EthBalance();

    bool wasDead = false;
    static string walletAddress;
    static string ethBalance;
    //check
    private void  Start()
    {
        player = GameObject.FindWithTag("Player");
        scoreSystem = player.GetComponent<ScoreSystem>();
        cc = player.GetComponent<Cainos.CharacterController>();
        walletAddress = WalletAddress();

        StartCoroutine(getAccountBalance("0xAcD1Cdd365f7d42c846F21893fcD918e4713748b", GetEthBalance));
    }



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

    public void GetWalletAddress()
    {
        int endIndex = walletAddress.Length;
        string start = walletAddress.Substring(0, 6);
        string end = walletAddress.Substring(endIndex-6, 6);

        walletAddressText.text = $"{start}............{end}";
    }

    public void GetEthBalance(decimal balance)
    {
        ethBalance = (balance).ToString();
        if (debugLogs)
        {
            print("balance from function imported:");
            print(ethBalance);
        }
        ethBalanceText.text = ethBalance.Substring(0,7) + "E";
    }

    public void OpenMenu()
    {
        GetWalletAddress();
        pauseButton.gameObject.SetActive(false);
        balancePanel.gameObject.SetActive(false);
        menuScoreText.text = scoreSystem.GetScore().ToString();
        if (cc.IsDead) wasDead = true;
        cc.IsDead = true;
        mainMenu.SetActive(true);

    }
    
    public void CloseMenu()
    {
        pauseButton.gameObject.SetActive(true);
        balancePanel.gameObject.SetActive(true);
        if(!wasDead) cc.IsDead = false;
        mainMenu.SetActive(false);
    }

    public void OpenShop()
    {
        shop.SetActive(true);

    }
       
    public void CloseShop()
    {
        shop.SetActive(false);
    }



}
