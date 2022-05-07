using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [DllImport("__Internal")] private static extern string WalletAddress();
    [DllImport("__Internal")] private static extern string EthBalance();

    bool wasDead = false;
    static string walletAddress;
    static string ethBalance;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        scoreSystem = player.GetComponent<ScoreSystem>();
        cc = player.GetComponent<Cainos.CharacterController>();
        walletAddress = WalletAddress();
        ethBalance = EthBalance();
    }

    public void GetWalletAddress()
    {
        walletAddressText.text = walletAddress;
        ethBalanceText.text = ethBalance;
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
