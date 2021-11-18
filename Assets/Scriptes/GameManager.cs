using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    public UI ui;
    public int kilCount = 0;

    public UiManager uiManager;
    public float timeLeft = 100;
    // private int coinCount = 0;

    void Start()
    {
        
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        int i = (int)Math.Ceiling(timeLeft);
        uiManager.ShowTimeLeftCount(i);
    }

    public void CountKillingEnemy()
    {
        kilCount++;
        ui.RenewKillText(kilCount);
    }
}
