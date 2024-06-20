using UnityEngine;

public class SprayerController : MonoBehaviour
{
   
    public float growthRate = 0.1f; // �Ĺ��� ���� �ӵ� ������ ���� ����
    public float raycastDistance = 100f; // Raycast �Ÿ�
    public bool holdingSprayer = false; // �й��⸦ ��� �ִ��� ����
    public GameObject SprayerPrefab; // �й��� ������Ʈ
    private PlayerController playerController; // PlayerController ��ũ��Ʈ�� �ν��Ͻ� ����
    public float currentScale = 0.0f; // ���� ũ��
    public AudioSource audioSource;     // AudioSource ������Ʈ
    public AudioClip spraySound;      //  ����� Ŭ��


    void Start()
    {
        // PlayerController�� �̺�Ʈ�� �޼��带 ���
        PlayerController.OnSprayerToggled += UpdateSprayerState;
        // PlayerController ��ũ��Ʈ�� �ν��Ͻ� ��������
        playerController = PlayerController.Instance;

        playerController = FindObjectOfType<PlayerController>();

    }

    public void UseItem()
    {
        Update();
        
        Debug.Log("�й��⸦ ������ϴ�");
    }
    void UpdateSprayerState(bool holding)
    {
        holdingSprayer = holding;
    }

    void Update()
    {       
            // ���콺 ������ ��ư�� ������ �й��⸦ ��� ���� ����
            if (Input.GetMouseButtonDown(1))
            {
                
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Raycast�� ����Ͽ� ���콺�� Ŭ���� ��ġ���� �Ĺ��� ����
                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    // ������ ������Ʈ�� Plant �±׸� ������ �ִ��� Ȯ��
                    if (hit.collider.gameObject.CompareTag("Plant"))
                    {
                       

                        Plant plant = hit.collider.GetComponent<Plant>();
                        if (plant != null)
                        {
                            // ���� �Ĺ��� ���� ���¿� ���� �� ����
                            //UpdatePlantModel(plant);
                        }
                        //Ŭ���� ������Ʈ���� GrowPlant �޼��� ȣ���Ͽ� �Ĺ� ���� ����
                        hit.collider.GetComponent<Plant>().GrowPlant(currentScale);
                        audioSource.PlayOneShot(spraySound);
                    }                    
                }
            }             
    }
}
