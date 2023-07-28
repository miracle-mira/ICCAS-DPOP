using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Sprite[] cardSprites;

    private List<int> cardIDList = new List<int>();

    private List<Card> cardList = new List<Card>();

    

    // Start is called before the first frame update
    void Start()//초기화 작업
    {
        GenerateCardID();
        ShuffleCardID();
        InitBoard();
    }

    void GenerateCardID(){
        for (int i =0; i< cardSprites.Length; i++){
            cardIDList.Add(i);
            cardIDList.Add(i); 
        }
    }

    void ShuffleCardID(){
        int cardCount = cardIDList.Count;
        for (int i = 0; i<cardCount; i++){
            int randomIndex = Random.Range(i, cardCount);
            int temp = cardIDList[randomIndex];
            cardIDList[randomIndex] = cardIDList[i];
            cardIDList[i] = temp;

        }
    }

    void InitBoard(){ //판의 초기화 의 개수 가로 4장 세로 5장 배치
        float spaceY = 1.8f;
        float spaceX = 1.3f;

        int cardIndext = 0;

        //col(-1.5, -0.5, 0.5, 1.5)
        // 0 - 2 = -2 + 0.5
        // 1 - 2 = -1 + 0.5
        // 2 - 2 = -0 + 0.5
        // 3 - 2 = 1 + 0.5
        //(col - (colCount/2)) * spaceX - (spaceX/2);
        //-2, -0.7, 0.7, 2
        //row
        // 0-2=-2 *spaceY
        // 1-2=-1*spaceY
        // 2-2=0*spaceY
        // 3-2=1*spaceY
        // 4-2=2*spaceY
        //(int)(rowCount /2) = 5/2의 정수값 = 2
        //(row -(int)(rowCount /2))*spaceY
        int rowCount = 5;
        int colCount = 4;

        for (int row = 0; row < rowCount; row++){
            for (int col = 0; col < colCount; col++){
                float posX = (col - (colCount/2)) * spaceX + (spaceX/2);
                float posY = (row - (int)(rowCount / 2)) * spaceY ;
                Vector3 pos = new Vector3(posX,posY,0f);
                GameObject cardObject = Instantiate(cardPrefab,pos, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();
                int cardID = cardIDList[cardIndext++];
                card.SetCardID(cardID);
                card.SetAnimalsprite(cardSprites[cardID]);
                cardList.Add(card);

            }
        }
    }

    public List<Card> GetCards(){
        return cardList;
    }
}
