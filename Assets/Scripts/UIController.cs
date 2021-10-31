using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
    {
    GameObject player;
    ScoreSystem scoreSystem;
    Cainos.CharacterController cc;

    [SerializeField] GameObject mainMenu;
    [SerializeField] Text menuScoreText;
    [SerializeField] Button pauseButton;

    bool wasDead = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        scoreSystem = player.GetComponent<ScoreSystem>();
        cc = player.GetComponent<Cainos.CharacterController>();
    }
    public void OpenMenu()
    {

        pauseButton.gameObject.SetActive(false);
        menuScoreText.text = scoreSystem.GetScore().ToString();
        if (cc.IsDead) wasDead = true;
        cc.IsDead = true;
        mainMenu.SetActive(true);

    }
    
    public void CloseMenu()
    {
        pauseButton.gameObject.SetActive(true);
        if(!wasDead) cc.IsDead = false;
        mainMenu.SetActive(false);
    }
        
    }
