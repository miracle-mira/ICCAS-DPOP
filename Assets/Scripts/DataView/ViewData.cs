using UnityEngine;
using UnityEngine.UI;

public class ViewData : MonoBehaviour
{
    public GameObject sliderPrefab; // 슬라이더 프리팹을 연결할 변수
    public float maxSliderValue = 5f;
    public float speed = 2f;
    public int[] targetValues1 = { 1, 2, 3, 4, 5 };
    public float[] targetValues = { 1f, 2f, 2f, 3f, 4f };

    private Slider[] sliders; // 생성된 슬라이더들을 저장할 배열
    private Text[] texts;     // 생성된 텍스트들을 저장할 배열

    private void Start()
    {
        // 슬라이더와 텍스트를 5개 생성합니다.
        sliders = new Slider[5];
        texts = new Text[5];
        for (int i = 0; i < 5; i++)
        {
            float xPos = -300f + i * 150f; // x축 시작점과 간격 설정
            CreateSliderAndText(i, xPos, targetValues[i]);
        }
    }

    private void Update()
{
    for (int i = 0; i < sliders.Length; i++)
    {
        sliders[i].value = Mathf.Lerp(sliders[i].value, targetValues[i], Time.deltaTime * speed);
        texts[i].text = "2023-08-" + (01 + targetValues1[i]).ToString("00");
    }
}


    // 슬라이더와 텍스트를 생성하는 함수
    private void CreateSliderAndText(int index, float xPos, float targetValue) 
    {
        // 슬라이더 생성
        GameObject sliderGO = Instantiate(sliderPrefab, transform);
        Slider slider = sliderGO.GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = maxSliderValue;
        slider.value = 0f;
        slider.transform.localPosition = new Vector3(xPos, 0f, 0f); // x 위치 설정
        sliders[index] = slider;

        // 텍스트 생성
        Text text = sliderGO.GetComponentInChildren<Text>();
        text.text = "2023-08-" + (1 + (int)(targetValue)).ToString("00"); // 2023-07-26부터 2023-07-30까지
        texts[index] = text;
    }
}
