using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int gemBalance = 0;
    private float gemPrice = 0.001f; // ether for 1000 pack

    public void setGemPrice(float newPrice)
    {
        gemPrice = newPrice;
    }

    public float getGemPrice() { return gemPrice; }

    public int getGemBalance() { return gemBalance; }

    public int incrementGemBalanceBy(int value)
    {
        gemBalance += value;
        return gemBalance;
    }

    public void setGemBalance(int value)
    {
        gemBalance = value;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
