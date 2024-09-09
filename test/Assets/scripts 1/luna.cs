    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luna : MonoBehaviour
{
    private bool isclimb;
    private Rigidbody RD;
    public float movespeed;
    private Vector2 move;
    private int maxHealth = 5;
    public int MaxHealth { get { return maxHealth; } }

    private int currentHealth;
    public int Health { get { return currentHealth; } }


    private Animator animator;
    private Vector2 looDirection = new Vector2(1, 0);
    private float moveScacle;
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");

    void Start()
    {
        RD = GetComponent<Rigidbody>();
        currentHealth = 5;
        int LunaHp = GetChurrentHp();
        animator = GetComponentInChildren<Animator>();

        //chang frame
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame

    public void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        move = new Vector2(h, v);
        animator.SetFloat("MoveValue", 0);
        if (!Mathf.Approximately(move.x, 0)|| !Mathf.Approximately(move.y, 0))
        {
            looDirection.Set(move.x, move.y);
            looDirection.Normalize();
        }

        animator.SetFloat("LookX", looDirection.x);
        animator.SetFloat("LookY", looDirection.y);
        moveScacle = move.magnitude;
        movespeed = 2;
        if (move.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveScacle = 2;
                movespeed = 4;
            }
            else
            {
                moveScacle = 1;
            }
        }
        animator.SetFloat("MoveValue", moveScacle);
    }
    private void FixedUpdate()
    {
            Move();
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //give transoform
        Vector3 position = transform.position;
        //move
        position.Set(h, 0f, v);
        position = position.normalized * movespeed * Time.deltaTime;
        //position= position+ movespeed * move * Time.fixedDeltaTime;
        //transform.position = position;
        RD.MovePosition(transform.position + position);
    }
    public void ChangHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth+"/"+maxHealth);
    }
    private  int GetChurrentHp()
    {
        return currentHealth;
    }

    public void cisclimb(bool off)
    {
        isclimb = off;
        animator.SetBool("Climb", off);
        Debug.Log("111");
    }
    public void climb(bool start)
    {
        Debug.Log("000");
        isclimb = true;
        animator.SetBool("Climb", start);
        RD.useGravity = false;
    }
}

