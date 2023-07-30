// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NewBehaviourScript : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;
// using Newtonsoft.Json;
// using System.Collections.Generic;

// public class DataManager : MonoBehaviour
// {
//     private List<int> gameIDs = new List<int>();
//     private List<int> gameLevels = new List<int>();
//     private List<string> playDates = new List<string>();

//     private string serverURL = "http://localhost:3000/gameData/gamedata";

//     void Start()
//     {
//         StartCoroutine(GetGameData());
//     }

//     IEnumerator GetGameData()
//     {
//         using (UnityWebRequest www = UnityWebRequest.Get(serverURL))
//         {
//             yield return www.SendWebRequest();

//             if (www.result != UnityWebRequest.Result.Success)
//             {
//                 Debug.Log("데이터를 가져오는 데 실패하였습니다: " + www.error);
//             }
//             else
//             {
//                 string jsonData = www.downloadHandler.text;
//                 ProcessGameData(jsonData);
//             }
//         }
//     }

//     void ProcessGameData(string jsonData)
//     {
//         // JSON 데이터를 역직렬화하여 List<Dictionary<string, object>> 형태로 변환
//         List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

//         // 데이터 변수에 저장
//         foreach (var data in dataList)
//         {
//             int gameID = int.Parse(data["gameID"].ToString());
//             int gameLevel = int.Parse(data["gameLevel"].ToString());
//             string playDate = data["playDate"].ToString();

//             gameIDs.Add(gameID);
//             gameLevels.Add(gameLevel);
//             playDates.Add(playDate);
//         }

//         // 데이터 확인용 로그 출력
//         for (int i = 0; i < gameIDs.Count; i++)
//         {
//             Debug.Log($"Game {i + 1}: ID={gameIDs[i]}, Level={gameLevels[i]}, Date={playDates[i]}");
//         }
//     }
// }

//     }
// }
