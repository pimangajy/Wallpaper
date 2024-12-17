using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeChildren : MonoBehaviour
{
    public RectTransform parent;
    public RectTransform child;

    void Update()
    {
        Vector2 parentSize = parent.sizeDelta;
        child.sizeDelta = new Vector2(parentSize.x * 0.5f, parentSize.y * 0.5f); // �θ� ũ���� 50%�� ����
    }
}
