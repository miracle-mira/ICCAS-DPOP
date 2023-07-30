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
    public int gameLevel; // 기본값으로 1을 설정합니다.

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Game1:
                SceneLoad.LoadSceneHandle(GetShootingSceneName("ShootingLevel"));
                break;
            case BTNType.Game2:
                SceneLoad.LoadSceneHandle(GetMiroSceneName("Miro_"));
                break;
        }
    }

    private string GetShootingSceneName(string prefix)
    {
        // gameLevel 값에 따라 적절한 ShootingLevel Scene 이름을 반환합니다.
        return $"{prefix}{gameLevel}";
    }

    private string GetMiroSceneName(string prefix)
    {
        // gameLevel 값에 따라 적절한 Miro Scene 이름을 반환합니다.
        return $"{prefix}{gameLevel}";
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
