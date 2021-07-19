using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int currentSkinIndex;
    public GameObject[] playerModels;

    public SkinClass[] skins;
    public Button buyButton;

    void Start()
    {
        foreach(SkinClass skin in skins)
        {
            if (skin.price == 0)
                skin.isUnlocked = true;
            else
                skin.isUnlocked = PlayerPrefs.GetInt(skin.name, 0) == 0 ? false: true;
        }

        currentSkinIndex = PlayerPrefs.GetInt("selectedSkin",0);
        foreach (GameObject skin in playerModels)
        {
            skin.SetActive(false);

        }

        playerModels[currentSkinIndex].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void ChangeNext()
    {
        playerModels[currentSkinIndex].SetActive(false);
        currentSkinIndex++;
        if (currentSkinIndex == playerModels.Length)
            currentSkinIndex = 0;
        playerModels[currentSkinIndex].SetActive(true);
        SkinClass s = skins[currentSkinIndex];
        if (!s.isUnlocked)
            return;

        PlayerPrefs.SetInt("selectedSkin", currentSkinIndex);
    }

    public void ChangePrev()
    {
        playerModels[currentSkinIndex].SetActive(false);
        currentSkinIndex--;
        if (currentSkinIndex == -1)
            currentSkinIndex = playerModels.Length - 1;
        playerModels[currentSkinIndex].SetActive(true);
        SkinClass s = skins[currentSkinIndex];
        if (!s.isUnlocked)
            return;
        PlayerPrefs.SetInt("selectedSkin", currentSkinIndex);
    }

    public void UnlockSkin()
    {
        SkinClass s = skins[currentSkinIndex];

        PlayerPrefs.SetInt(s.name, 1);
        PlayerPrefs.SetInt("selectedSkin", currentSkinIndex);
        s.isUnlocked = true;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - s.price);

    }

    private void UpdateUI()
    {
        SkinClass s = skins[currentSkinIndex];
        if (s.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.interactable = false;
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<Text>().text = s.price.ToString();
            if(s.price > PlayerPrefs.GetInt("Coins", 0))
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }
    }
}
