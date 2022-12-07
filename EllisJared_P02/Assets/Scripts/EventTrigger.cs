using Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, ISelectHandler, ISubmitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonHover");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonSelect");
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonHover");
    }

    public void OnSubmit(BaseEventData eventData)
    {
        AudioManager.Instance.PlaySFX("ButtonSelect");
    }
}
