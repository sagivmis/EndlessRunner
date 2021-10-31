using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score = 0;
    [SerializeField] Text scoreText;


    public int AddScore(int amount)
    {
        score += amount;
        return score;
    }
    public int RemoveScore(int amount)
    {
        score -= amount;
        return score;
    }

    public int GetScore() { return score; }

    void Update()
    {
        scoreText.text = score.ToString();
    }
}
