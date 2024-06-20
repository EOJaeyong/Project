using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject inventoryUI;

    void Start()
    {
        // 인벤토리 UI를 처음에는 비활성화 상태로 설정
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // "i" 키를 눌렀을 때 인벤토리를 열고 닫음
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 인벤토리 UI의 활성화 상태를 변경
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        // "esc" 키를 눌렀을 때 창을 닫음
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 인벤토리 UI가 활성화되어 있는 경우에만 닫음
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
            }
        }
    }
}
