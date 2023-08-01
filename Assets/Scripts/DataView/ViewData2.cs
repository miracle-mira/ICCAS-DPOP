using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ViewData2 : MonoBehaviour
{
    private List<int> gameIDs = new List<int>();
    private List<int> gameLevels = new List<int>();
    private List<string> playDates = new List<string>();

    private string serverURL = "http://localhost:3000/dateLoad/load";

    public GameObject sliderPrefab; // 슬라이더 프리팹을 연결할 변수
    public float maxSliderValue = 5f;
    public float speed = 2f;
    public float[] targetValues = { 1, 1, 1, 1, 1 }; // We will initialize this array later

    private Slider[] sliders; // 생성된 슬라이더들을 저장할 배열
    private Text[] texts;     // 생성된 텍스트들을 저장할 배열

    private void Start()
    {
        StartCoroutine(GetGameData());
        // 슬라이더와 텍스트를 각각 5개씩 생성합니다.
        sliders = new Slider[5];
        texts = new Text[5];
    }


    private void Update()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            // Lerping the slider value to the target value smoothly
            sliders[i].value = Mathf.Lerp(sliders[i].value, targetValues[i], Time.deltaTime * speed);

            // 만약 targetValues 값이 슬라이더의 최대값인 maxSliderValue를 초과하면 maxSliderValue로 설정
            sliders[i].value = Mathf.Min(sliders[i].value, maxSliderValue);

            // 만약 targetValues 값이 슬라이더의 최소값인 0보다 작으면 0으로 설정
            sliders[i].value = Mathf.Max(sliders[i].value, 0f);
        }
    }

    private IEnumerator GetGameData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(serverURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("데이터를 가져오는 데 실패하였습니다: " + www.error);
            }
            else
            {
                string jsonData = www.downloadHandler.text;
                ProcessGameData(jsonData);
            }
        }
    }

    private void ProcessGameData(string jsonData)
    {
        List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

        foreach (var data in dataList)
        {
            int gameID = int.Parse(data["gameID"].ToString());
            int gameLevel = int.Parse(data["gameLevel"].ToString());
            string playDate = data["playDate"].ToString();

            gameIDs.Add(gameID);
            gameLevels.Add(gameLevel);
            playDates.Add(playDate);
        }

        int sliderCount = 0; // 슬라이더 생성 개수를 카운트하기 위한 변수
        targetValues = new float[5]; // targetValues 배열 초기화

        for (int i = 0; i < gameIDs.Count; i++)
        {
            if (gameIDs[i] == 12 && sliderCount < 5)
            {
                // gameID가 11인 데이터만 수집하여 슬라이더와 텍스트를 생성합니다.
                float xPos = -300f + sliderCount * 150f;
                CreateSliderAndText(sliderCount, xPos, gameLevels[i]);
                targetValues[sliderCount] = gameLevels[i]; // targetValues 배열에 gameLevel 값을 넣어줍니다.
                sliderCount++;
            }
        }
    }

    private void CreateSliderAndText(int index, float xPos, int gameLevel)
    {
        // 슬라이더 생성
        GameObject sliderGO = Instantiate(sliderPrefab, transform);
        Slider slider = sliderGO.GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = maxSliderValue;
        slider.value = gameLevel; // gameLevel을 슬라이더의 초기 값으로 설정
        slider.transform.localPosition = new Vector3(xPos, 0f, 0f); // x 위치 설정
        sliders[index] = slider;

        // 텍스트 생성
        Text text = sliderGO.GetComponentInChildren<Text>();
        text.text = playDates[index]; // playDates의 값을 텍스트로 설정
        texts[index] = text;
    }
}
