using UnityEngine;
using UnityEngine.UI;

public class ViewData : MonoBehaviour
{
    public Slider slider;
    public float maxSliderValue = 5f;
    public float speed = 2f;
    public float targetValue;

    

    void Start()
    {
        // 슬라이더를 초기화합니다.
        slider.minValue = 0f;
        slider.maxValue = maxSliderValue;
        slider.value = 0f;
    }

    void Update()
    {
        // 슬라이더의 value를 타겟 값으로 보간하여 부드럽게 올려줍니다.
        slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * speed);
    }

    // 슬라이더를 올리는 함수를 호출합니다.
    public void RaiseSlider(float inputValue)
    {
        targetValue = inputValue;
        targetValue = Mathf.Clamp(targetValue, 0f, maxSliderValue); // 입력 값을 최대 값 범위 내로 조정
    }
}
