﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {
    public static int Random_Item = 0; // 랜덤 아이템의 상태 체크 
    public static bool Invincibility_Check = false; // 무적상태 체크
    public float Speed = 0f;
    public Slider HpBar;
    Vector2 MoveVelocity; 
    Rigidbody2D rigid;
    Transform t;
    Vector3 a;
    Vector3 b;
    // Use this for initialization
    void Start () {
        t = gameObject.GetComponent<Transform>();
        a = t.localScale / 2;
        b = t.localScale;
        rigid = GetComponent<Rigidbody2D>();
        if (Cchar_Mgr.ch[0].ch_use == true)
        {
            HpBar.maxValue = Cchar_Mgr.ch[0].ch_hp;
            HpBar.value = Cchar_Mgr.ch[0].ch_hp;
        }
        else if (Cchar_Mgr.ch[1].ch_use == true)
        {
            HpBar.maxValue = Cchar_Mgr.ch[1].ch_hp;
            HpBar.value = Cchar_Mgr.ch[1].ch_hp;
        }
        else if (Cchar_Mgr.ch[2].ch_use == true)
        {
            HpBar.maxValue = Cchar_Mgr.ch[2].ch_hp;
            HpBar.value = Cchar_Mgr.ch[2].ch_hp;
        }
    }	
	// Update is called once per frame
	void Update () {
        Ch_Move();
        Limit();
        if(Random_Item != 0) // 랜덤 아이템 체크
        {
            switch (Random_Item)
            {
                case 1:
                    StartCoroutine(Invincibility());
                    break;
                case 2:
                    StartCoroutine(Hp_up());
                    break;
                case 3:
                    StartCoroutine(Speed_up());
                    break;
                case 4:
                    StartCoroutine(Speed_down());
                    break;
                case 5:
                    StartCoroutine(Size_up());
                    break;
                default:
                    break;
            }
        }
    }
    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + MoveVelocity * Time.fixedDeltaTime);
    }
    void Ch_Move()
    {
        
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        MoveVelocity = moveInput.normalized * Speed;

    }
     void Limit()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Mathf.Clamp(transform.position.x, -5.5f, 5.5f);
        pos.y = Mathf.Clamp(transform.position.y, -4, 2);

        transform.position = pos;

    }

    void OnCollisionEnter2D(Collision2D collision) // 부딪혔을때 이벤트
    {
        if(collision.gameObject.tag == "Random") // 랜덤박스에 부딪힘
        {
            int ran = Random.Range(1, 6);
            Random_Item = ran;
            Destroy(collision.gameObject);
        }

        if(Invincibility_Check == false)
        {

        }
    }

    IEnumerator Invincibility() // 무적 이벤트
    {
        Random_Item = 0;
        Invincibility_Check = true;
        yield return new WaitForSeconds(5.0f);
        Invincibility_Check = false;
        yield return null;
    }

    IEnumerator Hp_up() // 체력 회복
    {
        Random_Item = 0;
        HpBar.value += 20;
        yield return null;
    }

    IEnumerator Speed_up() // 스피드 업
    {
        Random_Item = 0;
        Speed = 4;
        Back_Ground.scrollSpeed = 1.5f;
        yield return new WaitForSeconds(5.0f);
        Speed = 3;
        Back_Ground.scrollSpeed = 0.5f;
        yield return null;
    }

    IEnumerator Speed_down() // 스피드 다운
    {
        Random_Item = 0;
        Speed = 2;
        Back_Ground.scrollSpeed = 0.5f;
        yield return new WaitForSeconds(5.0f);
        Speed = 3;
        Back_Ground.scrollSpeed = 1.5f;
        yield return null;
    }

    IEnumerator Size_up() // 사이즈 증가
    {
        Random_Item = 0;
        t.localScale = a;
        yield return new WaitForSeconds(5.0f);
        t.localScale = b;
        yield return null;
    }
}
