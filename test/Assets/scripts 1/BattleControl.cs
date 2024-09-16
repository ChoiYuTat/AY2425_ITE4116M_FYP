using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BattleControl : BattleObj
{
    private Vector3 primitivPos;
    private Vector3 primitivRot;
    private Vector3 tempPos;
    private bool CanMove;
    public bool isAttack;

    public Transform t;
    private float distance;
    private Vector3 targetPos;
    public SpriteRenderer monsterSr;


    public Animator LunaAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        distance = 3;
        primitivPos = this.transform.localPosition;
        tempPos = Vector3.zero;
        CanMove = true;
        targetPos = t.localPosition;


    }
    private void Update()
    {
        if (isAttack)
        {
            StartCoroutine(PerformAttackLogic());
        }
    }

    protected override void MoveTo(Vector3 go , float dis)
    {

        if(Vector3.Distance(transform.position, go ) > dis)
        {
            tempPos = go;
            tempPos.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, go, 5 * Time.deltaTime);
        }
        else
        {
            CanMove = false;
            if (dis > 0.1f)
            {
                //Attect
                CanMove = true;
                targetPos = primitivPos;
                distance = 0.1f;
            }
            else 
            {
                //Go black
                GoBlack();
            }
        }
    }

    private void GoBlack()
    {
        transform.position = primitivPos;
        CanMove = false;
    }

    IEnumerator PerformAttackLogic()
    {
        //close UI
        LunaAnimator.SetBool("MoveState",true);
        LunaAnimator.SetFloat("Move", -1);
        this.transform.DOLocalMove(targetPos + new Vector3(1, 0, 0), 0.5f).OnComplete
            (
                () =>
                {
                    LunaAnimator.SetBool("MoveState", false);
                    LunaAnimator.SetFloat("Move", 0);
                    LunaAnimator.CrossFade("Attack", 0);
                    monsterSr.DOFade(0.3f, 0.2f).OnComplete(() => { JudgeMonsterHP(20); });
                }
            );
        yield return null;

    }


    private void JudgeMonsterHP(int value)
    {
        monsterSr.DOFade(1,0.2f);
    }
}
