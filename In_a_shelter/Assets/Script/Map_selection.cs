using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Map_selection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject originalBtn;
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite colorChangeSprite;

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(clickedObject);
        SceneManager.LoadScene("Adventure1"); //나중에 맵 더 생기면 objcct 이름으로 구별해서 따로 해야지
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalBtn.GetComponent<Image>().sprite = colorChangeSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        originalBtn.GetComponent<Image>().sprite = originalSprite;
    }
}
