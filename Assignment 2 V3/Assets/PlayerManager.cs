using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;

    #endregion
    public int points = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<TestEnemyController>().GetIsDead())
        {
            Debug.Log("DED");
        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "SCORE : " + points);
    }
}
