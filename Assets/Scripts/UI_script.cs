using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_script : MonoBehaviour
{
    public void PlayLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ShopLevel()
    {
        SceneManager.LoadScene(1);
    }
}
