using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cointime : MonoBehaviour
{
    [SerializeField] private GameObject itemBox;
    [SerializeField] private GameObject itemCoin;
    [SerializeField] private GameObject sensor;

    private GameObject player;
    private int hitCount = 0;
    public int hitMax = 10;
    bool isHitOn = true;

    public float boxSpeed = 0.5f;
    public float boxFallDistance = 0.5f;
    public float boxPosition;


    bool isCoinShow = true;
    public float coinMoveSpeed = 8f;
    public float coinMoveHight = 3f;
    public float coinFallDistance = 2f;
    public Vector2 _position;
    public float coinPosition;


    void Start()
    {
        isHitOn = true;
        boxPosition = itemBox.transform.localPosition.y;
        coinPosition = itemCoin.transform.localPosition.y;
        _position = transform.localPosition;
        this.player = GameObject.FindWithTag("Player");
        hitCount = 0;
    }

    void Update()
    {
        if (hitCount < hitMax)
        {
            if (isHitOn == true)
            {
                Hit();
            }
           
            
        }

    }
    IEnumerator BounceBox(GameObject box)
    {
        while (box != null)
        {
            box.transform.localPosition = new Vector2(box.transform.localPosition.x, box.transform.localPosition.y + boxSpeed * Time.deltaTime);
            if (box.transform.localPosition.y >= _position.y + boxSpeed + .05)
                break;

            yield return null;
        }

        while (box != null)
        {
            box.transform.localPosition = new Vector2(box.transform.localPosition.x, box.transform.localPosition.y - boxSpeed * Time.deltaTime);
            if (box.transform.localPosition.y <= boxPosition)
            {

                Debug.Log("bounce");
                
                isHitOn = true;
                isCoinShow = true;
                itemCoin.transform.localPosition = new Vector2(itemCoin.transform.localPosition.x, coinPosition);
                itemCoin.gameObject.SetActive(true);

                break;
            }

            yield return new WaitForSeconds(.1f);

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
            isHitOn = false;
            Debug.Log("hit");
            hitCount++;
            
            StartCoroutine(MoveCoin(itemCoin));
            StartCoroutine(BounceBox(itemBox));
            

        }

    }

    IEnumerator MoveCoin (GameObject coin)
    {
        while (isCoinShow == true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + coinMoveSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y >= _position.y + coinMoveHight + 1)
                break;

            yield return null;
        }

        while (isCoinShow == true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - coinMoveSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y <= _position.y + coinFallDistance + 1)
            {
                
                Debug.Log("파괴한다");
                //Destroy(coin.gameObject);
                coin.gameObject.SetActive(false);
                isCoinShow = false;
                
                break;
            }

            yield return new WaitForSeconds(.1f);

        }

    }
}
