using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject inventoryUI;

    void Start()
    {
        // �κ��丮 UI�� ó������ ��Ȱ��ȭ ���·� ����
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // "i" Ű�� ������ �� �κ��丮�� ���� ����
        if (Input.GetKeyDown(KeyCode.I))
        {
            // �κ��丮 UI�� Ȱ��ȭ ���¸� ����
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        // "esc" Ű�� ������ �� â�� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �κ��丮 UI�� Ȱ��ȭ�Ǿ� �ִ� ��쿡�� ����
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
            }
        }
    }
}
