using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int gemBalance = 0;
    private float gemPrice = 0.001f; // ether for 1000 pack

    public void SetGemPrice(float newPrice)
    {
        gemPrice = newPrice;
    }

    public float GetGemPrice() { return gemPrice; }

    public int GetGemBalance() { return gemBalance; }

    public int IncrementGemBalanceBy(int value)
    {
        gemBalance += value;
        return gemBalance;
    }

    public void SetGemBalance(int value)
    {
        gemBalance = value;
    }

}
