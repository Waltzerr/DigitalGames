using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int dna;
    public TextMeshProUGUI dnaDisplay;
    public static ShopManager Instance;
    public List<Button> purchaseButtons;
    public List<Vector2Int> purchasedUpgrades = new List<Vector2Int>();

    void Awake()
    {
        Instance = this;
        DNA = 30;
    }
    private void Update()
    {
        checkPrices();
    }
    public bool canPurchase(int cost)
    {
        return DNA >= cost;
    }

    public int DNA
    {
        get { return dna; }
        set
        {
            dna = value;
            dnaDisplay.text = $"DNA: {dna}";
        }
    }

    public void checkPrices()
    {
        foreach (Button button in purchaseButtons)
        {
            button.interactable = true;
        }
        int tower = GameManager.Instance.TowerUpgrade;

        if (!canPurchase(5))
        {
            purchaseButtons[0].interactable = false;
        }
        if (!canPurchase(7))
        {
            purchaseButtons[1].interactable = false;
        }
        if (!canPurchase(10))
        {
            purchaseButtons[2].interactable = false;
        }
        foreach(Vector2Int purchased in purchasedUpgrades)
        {
            if(purchased.x == GameManager.Instance.TowerUpgrade)
            {
                purchaseButtons[purchased.y - 1].interactable = false;
            }
        }

    }
}
