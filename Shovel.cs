using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public float shovelRange = 10f; // ������ ���� �Ÿ�
    public GameObject digEffectPrefab; // ���� �Ĵ� ȿ���� ������
    private PlayerController playerController; // PlayerController ��ũ��Ʈ�� �ν��Ͻ� ����
                                               // public Material AfterMaterial;
    bool digground = false;

    //private LineRenderer circleRenderer;

    //public Texture2D hoeTexture; // ���� ���� ��Ÿ���� �ؽ�ó

    // �ٸ� ��ũ��Ʈ���� FarmFieldInteraction ��ũ��Ʈ�� �ν��Ͻ��� ������
     private FarmFieldInteraction farmFieldInteraction;

    void Start()
    {
        // PlayerController ��ũ��Ʈ�� �ν��Ͻ��� ������
        playerController = FindObjectOfType<PlayerController>();

        // FarmFieldInteraction ��ũ��Ʈ�� �ν��Ͻ� ��������
        farmFieldInteraction = GetComponent<FarmFieldInteraction>();

        // ���� �⺻ �ؽ�ó�� ����
        //GetComponent<Renderer>().material.mainTexture = hoeTexture;
       // Renderer renderer = GetComponent<Renderer>();

    }

    public void UseItem()
    {
        Update();
        // ���� ��� ���� 
        Debug.Log("���̸� ����߽��ϴ�!");
    }

    void Update()
    {
        // ���̸� ��� && ���콺 �� Ŭ�� �� DigGround() ����
        if (playerController.holdingShoveler && Input.GetMouseButtonDown(0))
        {
            DigGround();
        }
    }

    void DigGround()
    {
        Debug.Log("���� ���ҽ��ϴ�");       
    }
}


