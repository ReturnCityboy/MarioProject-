using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofle;

public class EnemyObject : MonoBehaviour
{
    public enum eSprite { Run, Die }

    [SerializeField] private SpriteRenderer spriteRun = null;
    [SerializeField] private SpriteRenderer spriteDie = null;

    private StateMachine<EnemyObject> stateMachine = null;

    private GameObject player; //충돌판정
    [SerializeField] private GameObject sensor;
    [SerializeField] private float sensorRange = 3f;
    [SerializeField] private float speed = 0.01f;
    private float step = 3f;
    private int maxStepCounts= 60;
    private int stepCounts = 0;
    private bool isLeft = false;

    void Start()
    {
        stateMachine = new StateMachine<EnemyObject>(this);
        this.player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Move();
        Die();
    }

    private void HideAllSprite()
    {
        spriteRun.enabled = false;
        spriteDie.enabled = false;
    }
    
    private void Move()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);

        //Todo: 적이동
        if (dis < sensorRange)
        {
            //player가 감지범위
            //만약 적 기준 x축이 플레이어보다 x숫자가 크면 플레이어는 왼쪽에 있는거고, 아니면 오른쪽에 있음
            if (transform.position.x >= player.transform.position.x)
            {
                transform.position += Vector3.left * speed * step;
            }
            else
            {
                transform.position += Vector3.right * speed * step;
            }
            
        }
        else
        {
            if (!isLeft)
            {
                if (stepCounts < maxStepCounts)
                {
                    transform.position += Vector3.left * speed * step;
                    stepCounts++;
                }
                else
                {
                    isLeft = true;
                    stepCounts = 0;
                }
               // Debug.Log("왼쪽:" + stepCounts);
            }
            else
            {
                if (stepCounts < maxStepCounts)
                {
                    transform.position += Vector3.right * speed * step;
                    stepCounts++;
                }
                else
                {
                    isLeft = false;
                    stepCounts = 0;
                }
               // Debug.Log("오른쪽:" + stepCounts);
            }
           
        }
    }

    private void Die()
    {
        Vector2 p1 = sensor.transform.position;
        Vector2 p2 = player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.3f;
        float r2 = 0.3f;

        if (d < r1 + r2)
        {
            GameManager.Instance.CountKillingEnemy();
            BgManager.Instance.DieEnemy(this.gameObject);       
        }
    }

    private void ShowSprite(eSprite type)
    {
        HideAllSprite();

        switch (type)
        {
            case eSprite.Run:
                spriteRun.enabled = true;
                break;
            case eSprite.Die:
                spriteDie.enabled = true;
                break;
            default:
                break;
        }
    }

    private class RunState : State<EnemyObject>
    {
        protected override void Begin()
        {
            Owner.ShowSprite(eSprite.Run);
        }
        protected override void Update()
        {
            //Todo: player가 위에서 내려오면
            if (true)
            {
                Invoke<DieState>();
            }
        }
        protected override void End()
        {
        }
    }
    private class DieState : State<EnemyObject>
    {
        protected override void Begin()
        {
            Owner.ShowSprite(eSprite.Die);
        }
        //Todo: delayTime 지나고 이미지 끄기
        protected override void Update()
        {
        }
        protected override void End()
        {
        }
    }
   
    

}
