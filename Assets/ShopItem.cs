using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] GameObject itemOwnedFill;
    [SerializeField] TMP_Text quantityText;
    [SerializeField] TMP_Text priceText;
    GameObject player;
    Gems gemController;
    public int gemBalance;

    [Range(0, 10000)]
    [SerializeField] int itemPrice = 0;
    private int quantityOwned = 0;

    public int getItemPrice() { return itemPrice; }
    public int getQuantityOwned() { return quantityOwned; }
    public void setItemPrice(int price) { itemPrice = price; }
    public void incrementQuantityOwnedBy(int value) { quantityOwned += value; }
    public void setQuantity(int value) { quantityOwned = value; }

    public void buyItem()
    {
        if (gemBalance >= itemPrice)
        {
            gemController.incrementGemBalanceBy(-itemPrice);
            incrementQuantityOwnedBy(1);
            quantityText.text = $"x{quantityOwned}";

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gemController = player.GetComponent<Gems>();
        gemBalance = gemController.getGemBalance();
    }

    // Update is called once per frame
    void Update()
    {
        gemBalance = gemController.getGemBalance();
        priceText.text = itemPrice.ToString() ;
        if (quantityOwned > 0)
        {
            itemOwnedFill.gameObject.SetActive(true);
            quantityText.gameObject.SetActive(true);
        }
    }
}
