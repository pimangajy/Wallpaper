using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Move_UI : MonoBehaviour
{
    public Vector2 start_point;
    public Vector2 end_point;

    private RectTransform rectTransform;

    public float duration = 1.0f;

    void Awake()
    {
        // RectTransform 컴포넌트 가져오기
        rectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        StartCoroutine(Open_Panel());
    }
    public void Close()
    {
        StartCoroutine(Close_Panel());
    }
    public IEnumerator Open_Panel()
    {
        float elapsedTime = 0;
        rectTransform.anchoredPosition = start_point;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 선형 보간으로 이동
            rectTransform.anchoredPosition = Vector3.Lerp(start_point, end_point, t);

            yield return null;
        }

        // 정확한 위치로 설정
        rectTransform.anchoredPosition = end_point;
    }

    public IEnumerator Close_Panel()
    {
        float elapsedTime = 0;
        rectTransform.anchoredPosition = end_point;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 선형 보간으로 이동
            rectTransform.anchoredPosition = Vector3.Lerp(end_point, start_point, t);

            yield return null;
        }

        // 정확한 위치로 설정
        rectTransform.anchoredPosition = start_point;
    }
}
