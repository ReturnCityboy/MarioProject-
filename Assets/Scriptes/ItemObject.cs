using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private GameObject itemBox;
    [SerializeField] private GameObject itemCoin;
    [SerializeField] private GameObject sensor;
    
    private GameObject player;
    private int hitCount = 0;
    public int hitMax = 10;
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        hitCount = 0;
    }

    void Update()
    {
        if (hitCount < hitMax)
        {
            Hit();
        }
        
    }

    private void Hit()
    {
        Vector2 p1 = sensor.transform.position;
        Vector2 p2 = player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.3f;
        float r2 = 0.3f;

        if (d < r1 + r2)
        {
            Debug.Log("hit");
            hitCount++;
        }

    }
}
