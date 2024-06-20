using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountPlant : MonoBehaviour
{
    public int CurrentPlant { get; private set; }  // ���� ��Ȯ�� �Ĺ� ����
    public int MaxHarvestablePlants = 100; // ��Ȯ ������ �ִ� �Ĺ� ����
    public int TargetPlantCount = 100; // ��ǥ �Ĺ� ����
    public GameObject NextSecenePanel;  // UI�� ��� �ִ� �г�
    public Button nextSceneButton;  // ���� ������ �Ѿ�� ��ư
    private Alien alienScript; // Alien ��ũ��Ʈ�� �����ϱ� ���� ����
    private string currentSceneName; // ���� ���� �̸��� �����ϱ� ���� ����
    private string nextSceneName; // ���� ���� �̸��� �����ϱ� ���� ����

    public SceneLoader sceneLoader; // SceneLoader ��ũ��Ʈ�� �����ϱ� ���� ����
    public Text CurrentPlantText; // ���� ��Ȯ�� �Ĺ� ������ ǥ���� UI(Text) ���
    private PlayerHealth playerHealth; // PlayerHealth ��ũ��Ʈ�� �����ϱ� ���� ����

    public void Start()
    {
        // ���� ���� �̸� ����
        currentSceneName = SceneManager.GetActiveScene().name;

        sceneLoader = FindObjectOfType<SceneLoader>(); // SceneLoader ��ũ��Ʈ ������Ʈ�� ã�� �Ҵ�
        nextSceneButton.onClick.AddListener(LoadNextSceneOnClick);  // ��ư Ŭ�� �� LoadNextScene �Լ� ȣ��
        // Alien ��ũ��Ʈ ������Ʈ�� �����ͼ� ���� ������ �Ҵ�
        alienScript = GameObject.FindGameObjectWithTag("Alien").GetComponent<Alien>();

        // ���� ���� �̸� ����
        currentSceneName = SceneManager.GetActiveScene().name;

        // PlayerHealth ��ũ��Ʈ ������Ʈ�� �����ͼ� ���� ������ �Ҵ�
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();


        // �ʱ�ȭ �� UI ������Ʈ
        UpdateCurrentPlantUI();

    }
    void UpdateCurrentPlantUI()
    {
        if (CurrentPlantText != null)
        {
            CurrentPlantText.text = "���� ��Ȯ�� �Ĺ� ����: " + CurrentPlant.ToString();
        }
    }

    public void HarvestedPlant()
    {
        CurrentPlant++;
        Debug.Log("�� ��Ȯ�� �Ĺ� ����: " + CurrentPlant);


        UpdateCurrentPlantUI();


        if (CurrentPlant > MaxHarvestablePlants)
        {
            Debug.Log("��Ȯ ������ �ִ�ġ�� �ʰ��Ͽ����ϴ�!");
        }

        if (CurrentPlant >= TargetPlantCount)
        {
            Debug.Log("��ǥ �Ĺ� ������ �����߽��ϴ�! ���� ������ �̵��մϴ�.");
            // �ܰ����� ���� �߻����� �ʵ��� ����
            // �÷��̾ �Ͻ������� ���� ���·� ����
            playerHealth.SetInvincible(true);
            alienScript.SetCanShoot(false);
            NextSecenePanel.SetActive(true);
            alienScript.isShooting = false;
        }
    }
    public void LoadNextSceneOnClick()
    {
        // Debug.Log�� currentSceneName ������ ���� Ȯ��
        Debug.Log("Current Scene: " + currentSceneName);
        if (currentSceneName == "SampleScene")
        {
            sceneLoader.LoadRD2Scene();
        }
        else if (currentSceneName == "RD2")
        {
            sceneLoader.LoadRD3Scene();
        }
        else if (currentSceneName == "RD3")
        {
            sceneLoader.HappyEnding();
        }
        Debug.Log("�ε�ؽ�Ʈ��");


    }
}