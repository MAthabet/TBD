using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public static ButtonHoverManager Instance;

    [SerializeField] private RectTransform hoverOverlay;
    [SerializeField] private AudioClip hoverSound;   // Sound played on hover
    //[SerializeField] private AudioClip selectSound;  // Sound played on selection

    private GameObject currentHoveredButton = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (hoverOverlay == null)
        {
            GameObject overlayObject = GameObject.Find("HoverOverlay");
            if (overlayObject != null)
            {
                hoverOverlay = overlayObject.GetComponent<RectTransform>();
            }
            else
            {
                Debug.LogError("HoverOverlay not found");
            }
        }

        if (hoverOverlay != null)
        {
            hoverOverlay.gameObject.SetActive(false);
            hoverOverlay.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter != currentHoveredButton)
        {
            currentHoveredButton = eventData.pointerEnter;
            EventSystem.current.SetSelectedGameObject(currentHoveredButton);
            ShowHover(currentHoveredButton);


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == currentHoveredButton)
        {
            currentHoveredButton = null;
            HideHover();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        currentHoveredButton = eventData.selectedObject;
        ShowHover(currentHoveredButton);

        //PlaySound(selectSound);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (eventData.selectedObject == currentHoveredButton)
        {
            currentHoveredButton = null;
            HideHover();
        }
    }

    private void ShowHover(GameObject target)
    {
        if (hoverOverlay == null) return;

        if (target != null && target.TryGetComponent<RectTransform>(out RectTransform targetRect))
        {
            hoverOverlay.gameObject.SetActive(true);
            hoverOverlay.position = targetRect.position;
            hoverOverlay.sizeDelta = targetRect.sizeDelta;
        }
    }

    private void HideHover()
    {
        if (hoverOverlay == null) return;

        hoverOverlay.gameObject.SetActive(false);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Play"));
            }
        }
    }
}
