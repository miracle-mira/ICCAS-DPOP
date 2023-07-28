// ButtonType 스크립트 (코드2)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Game1:
                SceneLoad.LoadSceneHandle("ShootingLevel1");
                break;
            case BTNType.Game2:
                SceneLoad.LoadSceneHandle("Miro_1");
                break;
            // case BTNType.Game3:
            //     SceneLoad.LoadSceneHandle("Game3");
            //     break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
