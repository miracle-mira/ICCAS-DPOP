using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class selectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType2 currentType;
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
            case BTNType2.Start:
                SceneLoad.LoadSceneHandle("Select");
                break;
            case BTNType2.LookData:
                SceneLoad.LoadSceneHandle("ViewData");
                break;
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
