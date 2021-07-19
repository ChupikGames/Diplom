using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Text scoreText;
    [SerializeField] public int score;

    private void Update()
    {
        score = (int)(player.position.z / 2);
        scoreText.text = score.ToString();
    }
}
