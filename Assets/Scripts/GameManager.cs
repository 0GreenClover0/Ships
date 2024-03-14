using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform lifeUIParent;
    [SerializeField] private GameObject lifeUIPrefab;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private TMP_Text pointsText;

    private List<ShipSpawner> shipSpawners = new List<ShipSpawner>();
    private List<Image> lifesUI = new List<Image>();
    private int lifes = 5;
    private int points = 0;

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < lifes; i++)
        {
            lifesUI.Add(Instantiate(lifeUIPrefab, lifeUIParent).GetComponent<Image>());
        }
    }

    public void RemoveLife()
    {
        lifes--;

        if (lifes >= 0)
            lifesUI[lifes].sprite = emptyHeart;

        if (lifes <= 0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddPoint()
    {
        points++;
        pointsText.text = points.ToString() + "/5";

        if (points == 5)
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
