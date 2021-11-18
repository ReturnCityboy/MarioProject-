using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofle;

public class PlayerObject : MonoBehaviour
{
    public enum eSprite { Idle, Run, Jump}
    [SerializeField] private Rigidbody2D rigidbody = null;

    [SerializeField] private SpriteRenderer spriteIdle = null;
    [SerializeField] private SpriteRenderer spriteRun = null;
    [SerializeField] private SpriteRenderer spriteJump = null;

    private StateMachine<PlayerObject> stateMachine = null;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 180f;

    private bool isJump
    {
        get
        {
            return 0 != rigidbody.velocity.y;
        }
    }

    void Start()
    {
        stateMachine = new StateMachine<PlayerObject>(this);
        StartCoroutine(stateMachine.Coroutine<IdleState>());
    }
    
    private void Jump()
    {
        if (!isJump)
        {
            rigidbody.AddForce(new Vector2(0, jump));
        }
    }

    private void Move(bool left)
    {
        rigidbody.AddForce(new Vector2(left ? -speed : speed, 0));
    }

    private void LookAt(bool left)
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, left ? 180 : 0, transform.rotation.z));
    }

    private void HideAllSprite()
    {
        spriteIdle.enabled = false;
        spriteRun.enabled = false;
        spriteJump.enabled = false;
    }

    private void ShowSprite(eSprite type)
    {
        HideAllSprite();

        switch (type)
        {
            case eSprite.Idle:
                spriteIdle.enabled = true;
                break;
            case eSprite.Run:
                spriteRun.enabled = true;
                break;
            case eSprite.Jump:
                spriteJump.enabled = true;
                break;
            default:
                break;
        }
    }

    private class IdleState : State<PlayerObject>
    {
        protected override void Begin()
        {
            Owner.ShowSprite(eSprite.Idle);
        }
        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Invoke<RunState>();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Invoke<JumpState>();
            }
        }
        protected override void End()
        {
        }
    }

    private class RunState : State<PlayerObject>
    {
        protected override void Begin()
        {
            Owner.ShowSprite(eSprite.Run);
        }
        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Owner.Move(true);
                Owner.LookAt(true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Owner.Move(false);
                Owner.LookAt(false);
            }
            else
            {
                Invoke<IdleState>();
            }

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Invoke<IdleState>();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Invoke<JumpState>();
            }
        }
        protected override void End()
        {
        }
    }

    private class JumpState : State<PlayerObject>
    {
        protected override void Begin()
        {
            Owner.Jump();
            Owner.ShowSprite(eSprite.Jump);
        }
        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Owner.Move(true);
                Owner.LookAt(true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Owner.Move(false);
                Owner.LookAt(false);
            }

            if (!Owner.isJump)
            {
                Invoke<IdleState>();
            }
        }
        protected override void End()
        {
        }
    }
}
