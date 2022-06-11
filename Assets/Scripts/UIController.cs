﻿using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.JsonRpc.UnityClient;

public class UIController : MonoBehaviour
{
    GameObject player;
    ScoreSystem scoreSystem;
    AccountController accountController;
    Cainos.CharacterController cc;

    // Menus
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject buyGemsModal;
    [SerializeField] GameObject ethAPIModal;

    // Text fields
    [SerializeField] GameObject balancePanel;
    [SerializeField] Text menuScoreText;
    [SerializeField] TMP_Text playerPrivateKeyText;
    [SerializeField] TMP_Text walletAddressText;
    [SerializeField] TMP_Text ethBalanceText;
    [SerializeField] TMP_Text ethBalanceTextAPIModal;
    [SerializeField] TMP_Text walletAddressTextAPIModal;
    [SerializeField] TMP_Text latestBlockTextAPIModal;

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
    private string playerPrivateKey;

    public void SetUIEthBalance(string balance) { ethBalance = balance; }
    public void SetUIWalletAddress(string address) { walletAddress = address; }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        scoreSystem = player.GetComponent<ScoreSystem>();
        cc = player.GetComponent<Cainos.CharacterController>();

        accountController = player.GetComponent<AccountController>();
        gemsController = player.GetComponent<Gems>();
        gemBalance = gemsController.GetGemBalance();

    }

    public void SetLatestBlockAPIModal(string value) { latestBlockTextAPIModal.text = value; }
    public void SetWalletAddressAPIModal(string value) { walletAddressTextAPIModal.text = value; }
    public void SetEthBalanceAPIModal(string value) { ethBalanceTextAPIModal.text = value; }
    public void SetAPIModalAllRequests(string latestBlock, string walletAddress, string ethBalance) {
        latestBlockTextAPIModal.text = latestBlock;
        walletAddressTextAPIModal.text = walletAddress;
        ethBalanceTextAPIModal.text = ethBalance;
    }
    //public void 
    private void Update()
    {
        SetBalanceFieldsEntireGame(gemBalance);
    }

    public void SetBalanceFieldsEntireGame(int value)
    {
        gemBalance = gemsController.GetGemBalance();
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
        balancePanel.SetActive(false);
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
        balancePanel.SetActive(true);
        if (!wasDead) cc.IsDead = false;
        mainMenu.SetActive(false);
        if (debugLogs)
        {
            print($"MenuClose");
        }
    }

    public void OpenShop() { shop.SetActive(true); }

    public void CloseShop() { shop.SetActive(false); }
    public void OpenEthAPI() { ethAPIModal.SetActive(true); }
    public void CloseEthAPI() { ethAPIModal.SetActive(false); }

    public void OpenBuyGemModal() { buyGemsModal.SetActive(true); }
    public void CloseBuyGemModal() { buyGemsModal.SetActive(false); }

    public void CompleteGemPurchase()
    {
        //int amountWanted = baliGemSlider.getWantedAmountOfGems();
        //print($"WantedAmount: {amountWanted}");
        //float packPrice = 0.001f;
        //// buy with metamask

        //gemsController.incrementGemBalanceBy((int)(amountWanted / gemsController.getGemPrice()));
        //print($"Balance: {gemsController.getGemBalance()}");
        //CloseBuyGemModal();

        StartCoroutine(CompleteGemBuy());
    }

    public IEnumerator CompleteGemBuy()
    {
        int amountWanted = baliGemSlider.getWantedAmountOfGems();
        print($"WantedAmount: {amountWanted}");
        accountController.SendEthTx(walletAddress, (amountWanted * gemsController.GetGemPrice()).ToString(), playerPrivateKey);
        
        yield return new WaitForSeconds(5);

        print($"Balance: {gemsController.GetGemBalance()}");
        CloseBuyGemModal();
    }
    public void IncrementGemsBy(int value)
    {
        gemsController.IncrementGemBalanceBy(value);
    }
    public void SetPlayerPrivateKey()
    {
        playerPrivateKey = playerPrivateKeyText.text;
        print(playerPrivateKey);
    }
    public string GetPlayerPrivateKey() { return playerPrivateKey; }
}
