// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GraphMaker : MonoBehaviour
// {
//     public int SAMPLE_RATE = 10;
//     // 점을 찍을 횟수이다. const로 할까하다 테스트를 위해 일단 public필드로 만듬
//     public Gameobject Dot;
//     // 위에서 만든 점 프리팹
//     public Transform DotGroup;
//     // 생성한 점을 자식으로 둘 부모 오브젝트이다.
//     // 점들을 모아놓는 것과 선과 점의 그려지는 순서를 바꾸기에 편하다.
//     public Color BlueTeamColor;   // 블루팀 색상
//     public Color PurpleTeamColor; // 퍼플팀 색상

//     public RectTransform GraphArea;
//     // 그래프 길이가 해상도에 따라 달라지므로.. 받아올 애 추가

//     private float graph_Width;
//     private float graph_Height;
//     // 길이, 높이 저장할 변수 추가

//     Dictionary<int, int[]> AllTeamTotalGold = new Dictionary<int, int[]> { };

//     void Start()
//     {
//         var rand = new System.Random();
//         int BlueTeamGold = 0, PurpleTeamGold = 0;
//         for(int time=0; time < 100; time++)
//         {
//             BlueTeamGold = BlueTeamGold + rand.Next(0, 1000);
//             PurpleTeamGold = PurpleTeamGold + rand.Next(0, 1000);
//             AllTeamTotalGold.Add(time, new[] { BlueTeamGold, PurpleTeamGold });
//         }
//         graph_Width = GraphArea.rect.width;
//         graph_Height = GraphArea.rect.height;
//         // GraphArea의 가로 세로값을 가져온다.
        
//         DrawGoldGraph();
//     }

//     private void DrawGoldGraph()
//     {
//     float startPosition = -graph_Width / 2;
//     // 그래프 영역의 길이 / 2에 -를 붙이면 시작위치가 된다.
    
//     float maxYPosition = graph_Height / 2;
//     // 그래프 영역의 높이 / 2 => 점을 찍을 최대 높이

//     // ---
//     // 격차 컬렉션 생성 및 저장, 최대값 저장...(위에꺼)
//     // ---

//     for (int i = 0; i < SAMPLE_RATE; i++)
//     {
//         // 점 오브젝트 생성 및 부모 설정, 각 컴포넌트 가져오기
//         GameObject dot = Instantiate(Dot, DotGroup, true);
//         dot.transform.localScale = Vector3.one;

//         RectTransform dotRT = dot.GetComponent<RectTransform>();
//         Image dotImage = dot.GetComponent<Image>();

//         int tick = SAMPLE_RATE - 1 == i ? AllTeamTotalGold.Count - 1 : AllTeamTotalGold.Count / (SAMPLE_RATE - 1) * i;
//         // 총 경기시간 / 샘플링할 수로 간격을 정해 값을 가져오지만
//         // 마지막 경기 끝났을 때의 격차를 알고싶어 경기 끝난 시간을 따로 가져옴

//         float yPosOffset = comparisonValue[tick] / MaxValue;
//         // tick시간대의 골드격차 / 골드격차최대값 = -1.0f ~ 1.0f

//         dotImage.color = yPos >= 0f ? BlueTeamColor : PurpleTeamColor;
//         // 위 결과에 따라 격차가 0이여도 점은 BlueTeam색상을 하게 된다.
//         // 이것도 엄격하게 0일때는 중립색으로 하겠다면 바꾸면 된다.
        
//         dotRT.anchoredPosition = new Vector2(startPosition + (graph_Width / (SAMPLE_RATE - 1) * i), maxYPosition * yPosOffset);
//         // 가로는 startPosition부터 각 격차마다 늘어나며 일정간격으로 찍히게 하였고
//         // 세로는 격차/격차최대값에 따라 점이 찍힐 최대 높이에서 비율에 맞게 찍히게 하였다.
//         }
//     }
// }