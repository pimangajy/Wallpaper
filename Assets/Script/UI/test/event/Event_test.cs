using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Event_test : MonoBehaviour
{
    public event EventHandler LavelChanged;

    public int level
    {
        get { return level; }

        set { level = value; }
    }
    
}


