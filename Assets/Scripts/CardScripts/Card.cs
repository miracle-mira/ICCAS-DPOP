using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //dat사용

public class Card : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer cardRenderer;

    [SerializeField]
    private Sprite animalSprite;

    [SerializeField]
    private Sprite backSprite;

    private bool isFlipped = false;

    private bool isFlipping = false;
    private bool isMatched = false;

    public int cardID;

    public void SetCardID(int id){
        cardID = id;
    }

    public void SetMatched(){
        isMatched = true;
    }

    public void SetAnimalsprite(Sprite sprite){
        this. animalSprite = sprite;
    }

    public void FlipCard() {                       //이미지를 동물모양으로 교체

        isFlipping = true;

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(0f,originalScale.y,originalScale.z);

        transform.DOScale(targetScale, 0.2f).OnComplete(() =>           //DOScale작업을 끝낸후 다음 작업
        {
            isFlipped = !isFlipped;                     //실행될때마다 값을 설정 시작값을 바꾸어줌

            if (isFlipped){
                cardRenderer.sprite = animalSprite;
            } else{
                cardRenderer.sprite = backSprite;
            }
            transform.DOScale(originalScale,0.2f).OnComplete(()=>
            {
                isFlipping = false;
            });
        });
    }

    void OnMouseDown(){
        if(isFlipping == false && !isMatched && !isFlipped){
            CardGameManager.instance.CardClicked(this);
        }
    }

}
