using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastTarget_ : MonoBehaviour
{
    public Button button;

    public void Racast_Target_On()
    {
        button.GetComponent<Image>().raycastTarget = true;
    }
    public void Racast_Target_Off()
    {
        button.GetComponent<Image>().raycastTarget = false;
    }

}
