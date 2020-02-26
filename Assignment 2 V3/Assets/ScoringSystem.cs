using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int score = 0;

    public void UpdateScore(int i)
    {
        score += i;
        scoreText.GetComponent<Text>().text = " : " + score;
    }
}