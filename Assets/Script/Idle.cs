using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        StartCoroutine(Shake_H());  // 코루틴 시작
        StartCoroutine(Shake_R());
    }

    IEnumerator Shake_H()
    {
        while (true)  // 무한 반복
        {
            float waitTime = Random.Range(4.0f, 6.0f);  // 랜덤 시간 생성
            animator.SetLayerWeight(animator.GetLayerIndex("ArmLayer"), 1.0f);  // 레이어의 Weight를 1로 바꿔 애니메이션을 잘 보이게 한다
            animator.SetTrigger("idle_1");  // 트리거 실행
            yield return new WaitForSeconds(waitTime);  // 자신을 다시 실행
        }
    }

    IEnumerator Shake_R()
    {
        while (true)
        {
            float waitTime = Random.Range(2.0f, 5.0f);
            animator.SetLayerWeight(animator.GetLayerIndex("LegLayer"), 1.0f);
            animator.SetTrigger("idle_2");
            yield return new WaitForSeconds(waitTime);
        }
    }
}
