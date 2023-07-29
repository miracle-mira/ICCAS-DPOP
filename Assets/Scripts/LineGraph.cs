using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineGraph : MonoBehaviour
{
    public RectTransform graphArea;
    public GameObject dotPrefab;
    public int maxValue = 5;

    private List<Vector2> dataPoints = new List<Vector2>
    {
        new Vector2(0, 1), // 2023-07-26
        new Vector2(1, 2), // 2023-07-27
        new Vector2(2, 2), // 2023-07-28
        new Vector2(3, 3)  // 2023-07-29
    };

    private float graphWidth;
    private float graphHeight;

    private LineRenderer lineRenderer;

    private void Start()
    {
        graphWidth = graphArea.rect.width;
        graphHeight = graphArea.rect.height;

        CreateLineGraph();
    }

    private void CreateLineGraph()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = dataPoints.Count;
        lineRenderer.startWidth = 5f;
        lineRenderer.endWidth = 5f;

        for (int i = 0; i < dataPoints.Count; i++)
        {
            Vector2 dataPoint = dataPoints[i];

            // Calculate position of the dot based on dataPoint and graph size
            float xPos = graphWidth / (dataPoints.Count - 1) * dataPoint.x;
            float yPos = graphHeight * dataPoint.y / maxValue;

            // Instantiate the dot and set its position
            GameObject dot = Instantiate(dotPrefab, graphArea);
            dot.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

            // Set position of the line renderer
            lineRenderer.SetPosition(i, new Vector3(xPos, yPos, 0f));
        }
    }
}
