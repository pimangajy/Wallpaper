using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Plus : MonoBehaviour
{
    Event_test event_Test2 = new Event_test();


    private void Start()
    {
        //event_Test2.LavelChanged += DebugLog();
    }

    private void Eventlog()
    {
       // event_Test2.LavelChanged += DebugLog();
    }

    public void DebugLog()
    {
        Debug.Log("이벤트 추가");
    }
}
