using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public int currentSkinIndex;
    public Animator currentanim;
    public GameObject[] playerSkin;
    private PlayerController playerController;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentSkinIndex = PlayerPrefs.GetInt("selectedSkin", 0);
        
        foreach (GameObject skin in playerSkin)
        {
            skin.SetActive(false);
        }
        playerController.anim = playerSkin[currentSkinIndex].GetComponent<Animator>();
        playerSkin[currentSkinIndex].SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
