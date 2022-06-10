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

    // Menus
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject buyGemsModal;

    // Text fields
    [SerializeField] GameObject balancePanel;
    [SerializeField] Text menuScoreText;
    [SerializeField] TMP_Text walletAddressText;
    [SerializeField] TMP_Text ethBalanceText;

    // others
    [SerializeField] Button pauseButton;
    [SerializeField] bool debugLogs = true;
    [SerializeField] InputValue baliGemSlider;

    public Gems gemsController;

    // balance fields (shop and in-game)
    [SerializeField] TMP_Text mainGameWindowBalanceText;
    [SerializeField] TMP_Text mainMenuBalanceText;
    [SerializeField] TMP_Text shopBalanceText;
    [SerializeField] TMP_Text buyGemModalBalanceText;

    [DllImport("__Internal")] private static extern string WalletAddress();
    [DllImport("__Internal")] private static extern string EthBalance();

    public string walletAddress;
    public string ethBalance;
    bool wasDead = false;
    public int gemBalance = 0;
    public void setUIEthBalance(string balance) { ethBalance = balance; }
    public void setUIWalletAddress(string address) { walletAddress = address; }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        scoreSystem = player.GetComponent<ScoreSystem>();
        cc = player.GetComponent<Cainos.CharacterController>();
    
        gemsController = player.GetComponent<Gems>();
        gemBalance = gemsController.getGemBalance();

    }

    private void Update()
    {
        setBalanceFieldsEntireGame(gemBalance);
    }

    public void setBalanceFieldsEntireGame(int value)
    {
        gemBalance = gemsController.getGemBalance();
        mainGameWindowBalanceText.text = value.ToString();
        mainMenuBalanceText.text = value.ToString();
        shopBalanceText.text = value.ToString();
        buyGemModalBalanceText.text = value.ToString();
    }


    public void SetWalletAddress()
    {
        if (walletAddress.Length > 10)
        {
            int endIndex = walletAddress.Length;
            string start = walletAddress.Substring(0, 6);
            string end = walletAddress.Substring(endIndex - 6, 6);
            if (debugLogs)
            {
                print($"WalletAddress: {walletAddress}");
                print($"WalletAddress_First6Characters: {start}");
                print($"WalletAddress_Last6Characters: {end}");
            }

            walletAddressText.text = $"{start}............{end}";
        }
    }

    public void SetEthBalance()
    {
        if (debugLogs)
        {
            print($"BalanceImportedFunction: {ethBalance}");
        }
        ethBalanceText.text = ethBalance.Substring(0, 7) + "E";
    }

    public void OpenMenu()
    {
        SetWalletAddress();
        pauseButton.gameObject.SetActive(false);
        balancePanel.gameObject.SetActive(false);
        menuScoreText.text = scoreSystem.GetScore().ToString();
        if (cc.IsDead) wasDead = true;
        cc.IsDead = true;
        mainMenu.SetActive(true);
        if (debugLogs)
        {
            print($"MenuOpen");
        }

    }

    public void CloseMenu()
    {
        pauseButton.gameObject.SetActive(true);
        balancePanel.gameObject.SetActive(true);
        if (!wasDead) cc.IsDead = false;
        mainMenu.SetActive(false);
        if (debugLogs)
        {
            print($"MenuClose");
        }
    }

    public void OpenShop() { shop.SetActive(true); }

    public void CloseShop() { shop.SetActive(false); }

    public void OpenBuyGemModal() { buyGemsModal.SetActive(true); }
    public void CloseBuyGemModal() { buyGemsModal.SetActive(false); }

    public void CompleteGemPurchase()
    {
        int amountWanted = baliGemSlider.getWantedAmountOfGems();
        print($"WantedAmount: {amountWanted}");
        // buy with metamask

        gemsController.incrementGemBalanceBy((int)(amountWanted / gemsController.getGemPrice()));
        print($"Balance: {gemsController.getGemBalance()}");
        CloseBuyGemModal();
    }
}
