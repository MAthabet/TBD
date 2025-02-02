using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public RectTransform panel1;
    public RectTransform panel2;
    public float moveSpeed = 500f;

    private bool isSwitching = false;
    private Vector2 panel1TargetPosition;
    private Vector2 panel2TargetPosition;
    private bool panel1IsActive = true;

    void Start()
    {
        float screenWidth = Screen.width;
        panel1.anchoredPosition = Vector2.zero;
        panel2.anchoredPosition = new Vector2(screenWidth, 0);
        panel1TargetPosition = panel1.anchoredPosition;
        panel2TargetPosition = panel2.anchoredPosition;
    }

    void Update()
    {
        if (isSwitching)
        {
            panel1.anchoredPosition = Vector2.MoveTowards(panel1.anchoredPosition, panel1TargetPosition, moveSpeed * Time.deltaTime);
            panel2.anchoredPosition = Vector2.MoveTowards(panel2.anchoredPosition, panel2TargetPosition, moveSpeed * Time.deltaTime);

            if (panel1.anchoredPosition == panel1TargetPosition && panel2.anchoredPosition == panel2TargetPosition)
            {
                isSwitching = false;
            }
        }
    }

    public void SwitchPanels()
    {
        float screenWidth = Screen.width;
        
        if (panel1IsActive)
        {
            panel1TargetPosition = new Vector2(-screenWidth, 0);
            panel2TargetPosition = Vector2.zero;
        }
        else
        {
            panel1TargetPosition = Vector2.zero;
            panel2TargetPosition = new Vector2(screenWidth, 0);
        }

        panel1IsActive = !panel1IsActive;
        isSwitching = true;
    }
}