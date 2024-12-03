using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework.Internal;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Event_Anime event_Anime;
    public Rect clickableRect = new Rect(0.25f, 0.25f, 0.5f, 0.5f);
    public UnityEngine.Color gizmoColor = UnityEngine.Color.red;
    public Camera mainCamera;

    private bool isDragging = false;
    private Vector3 lastDragPosition;
    private float dragThreshold = 0.01f; // �巡�׷� �ν��� �ּ� �̵� �Ÿ�
    private float touchStartTime;
    private Vector3 touchStartPosition;
    private const float TOUCH_TIME_THRESHOLD = 0.2f; // ��ġ�� �ν��� �ִ� �ð� (��)

    void Awake()
    {
        if (mainCamera == null)  // ī�޶��Ҵ������� Ȯ��
        {
            mainCamera = Camera.main;
            Debug.LogWarning("ī�޶� �Ҵ���� �ʾ� ���� ī�޶� ����մϴ�. Inspector���� ī�޶� ���� �Ҵ��ϴ� ���� �����մϴ�.");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

#if UNITY_EDITOR   // ����Ƽ ������ ���콺 �Է� ó��
        HandleMouseInput();
#elif UNITY_ANDROID  // �ȵ���̵� ��ġ �Է� ó��
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (clickableRect.Contains(viewportPoint))
            {
                touchStartTime = Time.time;
                touchStartPosition = viewportPoint;
                lastDragPosition = viewportPoint;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!isDragging && clickableRect.Contains(viewportPoint))
            {
                float distance = Vector3.Distance(viewportPoint, touchStartPosition);
                if (distance > dragThreshold)
                {
                    isDragging = true;
                    event_Anime.animator.SetBool("Drag", true);
                    Debug.Log("�巡�� ����!");
                }
            }

            if (isDragging)
            {
                Vector3 delta = viewportPoint - lastDragPosition;
                //idle_Anime.DragEvent(viewportPoint, delta);
                lastDragPosition = viewportPoint;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                //idle_Anime.DragEndEvent(viewportPoint);
                event_Anime.animator.SetBool("Drag", false);
                Debug.Log("�巡�� ����!");
            }
            else if (Time.time - touchStartTime < TOUCH_TIME_THRESHOLD)
            {
                event_Anime.TouchEvent(viewportPoint);
                Debug.Log("��ġ �̺�Ʈ ����!");
            }
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (clickableRect.Contains(viewportPoint))
                    {
                        touchStartTime = Time.time;
                        touchStartPosition = viewportPoint;
                        lastDragPosition = viewportPoint;
                    }
                    break;

                case TouchPhase.Moved:
                    if (clickableRect.Contains(viewportPoint))
                    {
                        if (!isDragging)
                        {
                            float distance = Vector3.Distance(viewportPoint, touchStartPosition);
                            if (distance > dragThreshold)
                            {
                                isDragging = true;
                                Debug.Log("�巡�� ����!");
                            }
                        }

                        if (isDragging)
                        {
                            Vector3 delta = viewportPoint - lastDragPosition;
                            //idle_Anime.DragEvent(viewportPoint, delta);
                            lastDragPosition = viewportPoint;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isDragging && clickableRect.Contains(viewportPoint))
                    {
                        isDragging = false;
                        //idle_Anime.DragEndEvent(viewportPoint);
                        Debug.Log("�巡�� ����!");
                    }
                    else if (clickableRect.Contains(viewportPoint) &&
                           Time.time - touchStartTime < TOUCH_TIME_THRESHOLD)
                    {
                        event_Anime.TouchEvent(viewportPoint);
                        Debug.Log("��ġ �̺�Ʈ ����!");
                    }
                    break;
            }
        }
    }

    private void StartDrag(Vector3 viewportPoint)
    {
        isDragging = true;
        lastDragPosition = viewportPoint;
        event_Anime.TouchEvent(viewportPoint); // ���� �̺�Ʈ ȣ��
        Debug.Log("�巡�� ����!");
    }

    private void UpdateDrag(Vector3 viewportPoint)
    {
        if (isDragging)
        {
            Vector3 delta = viewportPoint - lastDragPosition;
            //idle_Anime.DragEvent(viewportPoint, delta); // Test Ŭ������ ���ο� �޼��� �ʿ�
            lastDragPosition = viewportPoint;
            Debug.Log($"�巡�� ��: Delta = {delta}");
        }
    }

    private void EndDrag()
    {
        isDragging = false;
        //idle_Anime.DragEndEvent(lastDragPosition); // Test Ŭ������ ���ο� �޼��� �ʿ�
        Debug.Log("�巡�� ����!");
    }

    // ������ OnDrawGizmos �޼���� �״�� ����
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Rect�� ȭ�� ������ �°� ��ȯ�Ͽ� �׸��ϴ�
        // Recr���� �׸� �簢���� �� �������� Vector��ġ�� ����
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMin, clickableRect.yMin, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMax, clickableRect.yMax, mainCamera.nearClipPlane));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMin, clickableRect.yMax, mainCamera.nearClipPlane));
        Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMax, clickableRect.yMin, mainCamera.nearClipPlane));

        // Gizmos�� �簢�� �׸���
        // Vector�� ������ ��ġ���� ���� �̾������� �簢���� �׸�
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}