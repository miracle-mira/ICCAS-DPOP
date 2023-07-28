using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public GameObject panelObject;

    // 패널을 토글하는 메서드
    public void TogglePanel()
    {
        panelObject.SetActive(!panelObject.activeSelf);
    }
}
