using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        StartCoroutine(Shake_H());  // �ڷ�ƾ ����
        StartCoroutine(Shake_R());
    }

    IEnumerator Shake_H()
    {
        while (true)  // ���� �ݺ�
        {
            float waitTime = Random.Range(4.0f, 6.0f);  // ���� �ð� ����
            animator.SetLayerWeight(animator.GetLayerIndex("ArmLayer"), 1.0f);  // ���̾��� Weight�� 1�� �ٲ� �ִϸ��̼��� �� ���̰� �Ѵ�
            animator.SetTrigger("idle_1");  // Ʈ���� ����
            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
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
