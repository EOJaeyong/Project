using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    public Slider healthSlider; // ü���� ǥ���� Slider UI ���
    public PlayerHealth playerHealth; // �÷��̾��� ü���� �����ϴ� ��ũ��Ʈ

    void Start()
    {
        // PlayerHealth ��ũ��Ʈ�� ã�� �Ҵ��մϴ�.
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        // Slider�� �ִ밪�� �÷��̾��� ���� ü������ �����մϴ�.
        healthSlider.maxValue = playerHealth.startingHealth;

        // ���� ü���� ���� ü������ �����մϴ�.
        healthSlider.value = playerHealth.startingHealth;
    }

    void Update()
    {
        // ���� ü���� Slider�� �ݿ��մϴ�.
        healthSlider.value = playerHealth.currentHealth;
    }
}
