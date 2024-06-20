using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public float shovelRange = 10f; // 괭이의 사정 거리
    public GameObject digEffectPrefab; // 땅을 파는 효과의 프리팹
    private PlayerController playerController; // PlayerController 스크립트의 인스턴스 변수
                                               // public Material AfterMaterial;
    bool digground = false;

    //private LineRenderer circleRenderer;

    //public Texture2D hoeTexture; // 갈린 밭을 나타내는 텍스처

    // 다른 스크립트에서 FarmFieldInteraction 스크립트의 인스턴스를 가져옴
     private FarmFieldInteraction farmFieldInteraction;

    void Start()
    {
        // PlayerController 스크립트의 인스턴스를 가져옴
        playerController = FindObjectOfType<PlayerController>();

        // FarmFieldInteraction 스크립트의 인스턴스 가져오기
        farmFieldInteraction = GetComponent<FarmFieldInteraction>();

        // 밭의 기본 텍스처를 적용
        //GetComponent<Renderer>().material.mainTexture = hoeTexture;
       // Renderer renderer = GetComponent<Renderer>();

    }

    public void UseItem()
    {
        Update();
        // 괭이 사용 로직 
        Debug.Log("괭이를 사용했습니다!");
    }

    void Update()
    {
        // 괭이를 들고 && 마우스 좌 클릭 시 DigGround() 실행
        if (playerController.holdingShoveler && Input.GetMouseButtonDown(0))
        {
            DigGround();
        }
    }

    void DigGround()
    {
        Debug.Log("밭을 갈았습니다");       
    }
}


