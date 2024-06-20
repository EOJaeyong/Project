using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{

    public float maxScale = 3.0f; // �ִ� ���� ��ġ

    public float growthRate = 1.0f; // ���� �ӵ�

    public float currentScale = 0.0f; // ���� ũ��

    private int currentStageIndex = 0; // ���� ���� �ܰ� �ε���

    public bool isHarvestable = false; // ��Ȯ ���� ����

    public GameObject plant01Prefab; // �ʱ� �Ĺ� ������
    public GameObject plant02Prefab; // ���� ���°� 1�� ���� �Ĺ� ������
    public GameObject plant03Prefab; // ���� ���°� 2 �̻��� ���� �Ĺ� ������
    public GameObject currentPlant; // ���� �Ĺ� ������Ʈ

    [SerializeField]
    public CountPlant countPlant; // CountPlant ��ũ��Ʈ�� ���� ����

   
    private void Start()
    {      
        // CountPlant ��ũ��Ʈ�� ���� ã�� ���� GameObject.Find �Ǵ� GameObject.FindWithTag�� ����� �� �ֽ��ϴ�.
        GameObject countPlantObj = GameObject.Find("CountPlant"); // CountPlantObject�� CountPlant ��ũ��Ʈ�� ���Ե� GameObject�� �̸��Դϴ�.
        // CountPlant ��ũ��Ʈ�� �ν��Ͻ��� �����ͼ� �Ҵ�
        if (countPlantObj != null)
        {
            countPlant = countPlantObj.GetComponent<CountPlant>(); // CountPlant ��ũ��Ʈ�� �����ͼ� countPlant�� �Ҵ��մϴ�.
        }
        else
        {
            Debug.LogError("CountPlantObject�� ã�� �� �����ϴ�.");
        }
    }

    // �Ĺ� ���� �Լ�
    public void GrowPlant(float growthRate)
    {
        // ���� ũ�Ⱑ �ִ� ũ�⿡ �������� �ʾҴٸ� ��� ����
        if (currentScale < maxScale)
        {
            currentScale += 1f;
        }
        // �ִ� ũ�⿡ �����ϸ� ��Ȯ ���� ���·� ����
        if (currentScale >= maxScale)
        {
            isHarvestable = true;
            
            HarvestPlant();       // �Ĺ��� ��Ȯ�ϴ� �Լ� ȣ��
            countPlant.HarvestedPlant();

        }
        if (currentPlant != null)
        {
            Destroy(currentPlant);
        }
        // ���� ���¿� ���� ������ �������� ����
        if (currentScale == 1)
        {
            currentPlant = Instantiate(plant02Prefab, transform.position, transform.rotation);
            Destroy(plant01Prefab);
            
        }
        else if (currentScale >= 2)
        {
            currentPlant = Instantiate(plant03Prefab, transform.position, transform.rotation);
            Destroy(plant02Prefab);
        }
        // ���� ���� �߰�
        currentScale += 1;
        Debug.Log("�Ĺ��� �����߽��ϴ�. ���� ���� ����: " + currentScale);
    }
    private void HarvestPlant()
    {
        // ��Ȯ ������ ���¶��
        if (isHarvestable)
        {
             
                Debug.Log("TryHarvest01����");
                // �Ĺ��� ��Ȯ�� �� �ʱ�ȭ
                currentScale = 0.0f;
                transform.localScale = Vector3.zero;

                isHarvestable = false;

                Destroy(plant03Prefab);
        }
        else
        {
            Debug.Log("���� ��Ȯ�� �� ���� �����Դϴ�.");
        }
    }
}