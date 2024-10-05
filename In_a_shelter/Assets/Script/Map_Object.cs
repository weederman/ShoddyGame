using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Object : MonoBehaviour
{
    // 오브젝트 트리거
    public Transform targetObject;
    public GameObject f_Img;  // F키를 눌러 상호작용 가능한 이미지
    public GameObject f_map_out;
    public float radius;
    public UIManager UImanager;
    // 지도 이미지 UI (큰 지도)
    public GameObject mapImage;  // 지도를 표시할 UI

    // 외곽선
    public Material objectMaterial;
    public string outlineProperty = "_OutlineEnabled";

    private bool isPlayerNearby = false;  // 플레이어가 반경 안에 있는지 여부

    void Start()
    {
        // F키 이미지 및 지도 비활성화
        f_Img.gameObject.SetActive(false);
        f_map_out.gameObject.SetActive(false);
        mapImage.gameObject.SetActive(false);
        objectMaterial.SetFloat(outlineProperty, 0f);
    }

    void Update()
    {
        insideBox();

        // 플레이어가 범위 안에 있고 F 키를 눌렀을 때 지도를 표시/비활성화
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleMap();
        }
    }

    // 플레이어가 반경 안에 있는지 확인하는 함수
    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.position);

        // 오브젝트가 반경 안에 들어왔는지 확인
        if (distance <= radius)
        {
            // f_Img를 활성화
            f_Img.gameObject.SetActive(true);
            objectMaterial.SetFloat(outlineProperty, 1f);
            isPlayerNearby = true;
        }
        else
        {
            // f_Img를 비활성화
            f_Img.gameObject.SetActive(false);
            objectMaterial.SetFloat(outlineProperty, 0f);
            isPlayerNearby = false;

            // 반경을 벗어나면 지도도 닫기
            if (mapImage.gameObject.activeSelf)
            {
                mapImage.gameObject.SetActive(false);
                f_map_out.gameObject.SetActive(false);
            }
        }
    }

    // 지도를 토글하는 함수 (켜기/끄기)
    private void ToggleMap()
    {
        bool isActive = mapImage.gameObject.activeSelf;
        mapImage.gameObject.SetActive(!isActive);
        f_map_out.gameObject.SetActive(!isActive);
    }
}
