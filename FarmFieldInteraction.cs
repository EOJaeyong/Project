using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmFieldInteraction : MonoBehaviour
{
    public Image plant;

    private PlayerController playerController;
    public float shovelRange = 100f;

    public Texture2D newTexture; // ���� �κ��� ��Ÿ���� ���ο� �ؽ�ó

    private Texture2D originalTexture; // ���� �ؽ�ó�� ������ ����
    private Renderer renderer; // ������ ������Ʈ�� ������ ����

    public Texture hoeTexture; // ���� ���� ��Ÿ���� �ؽ�ó

    public GameObject seedPrefab; // ���� ������

    public bool digground = false;

    private int growthLevel = 0; // �Ĺ� ���� ����

    public bool holdingSprayer = false; // �й��⸦ ��� �ִ��� ���θ� ��Ÿ���� ����

    public GameObject Sprayer; // �й��� ������Ʈ
    public Transform hand; // �÷��̾��� �� ��ġ

    [SerializeField] private bool plantOnField = false; // �翡 �Ĺ��� �ִ��� ���θ� ��Ÿ���� ����

    public AudioSource audioSource;     // AudioSource ������Ʈ
    public AudioClip digSound;      //  ����� Ŭ��
    private void Start()
    {
        // ������ ������Ʈ ��������
        renderer = GetComponent<Renderer>();

        // ���� �ؽ�ó�� ����
        originalTexture = (Texture2D)renderer.material.mainTexture;

        // ���� �������� �Ҵ�
        seedPrefab = Resources.Load<GameObject>("Seed");

        playerController = FindObjectOfType<PlayerController>();

    }

    private void Update()
    {
        DigGround();
    }

    void PlantSeed(Vector3 position)
    {
        // ���� �������� �ν��Ͻ�ȭ�ϰ� ���� ��ġ�� ��ġ
        //Instantiate(seedPrefab, position, Quaternion.identity);
    }

    void PlantSeedposition(Vector3 position)
    {
        // ���� �������� �ν��Ͻ�ȭ�ϰ� ���� ��ġ�� ��ġ
        Instantiate(seedPrefab, position, Quaternion.identity);
    }

    [SerializeField] bool isDigged = false;
    public bool IsDigged { get { return isDigged; } }
    void DigGround()
    {
        // ���콺 ���� ��ư Ŭ�� ��
        if (playerController.holdingShoveler && Input.GetMouseButtonDown(0))
        {
            // ���콺�� Ŭ���� �������� ray�� �߻��Ͽ� �浹�� ��ü ������ ������
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, shovelRange))
            {
                // �浹�� ������Ʈ�� �� �ڱ� �ڽ����� Ȯ���Ͽ� �ٲ� // �±׸� this.gameObject�� ����
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("���� �Ϸ�");

                    if (!IsPlantOnField())
                    {
                        plant.gameObject.SetActive(true);

                        // 2�� �Ŀ� ������Ʈ�� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
                        StartCoroutine(DisableObjectAfterDelay(1f));

                        // �浹�� ������Ʈ�� ������ ������Ʈ ��������
                        Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
                        // �浹�� ������Ʈ�� �ؽ�ó ����
                        hitRenderer.material.mainTexture = newTexture;
                        isDigged = true;
                        audioSource.PlayOneShot(digSound);

                        plantOnField = true; // �Ĺ��� �ɾ��� ���·� ����
                    }
                    else
                    {
                        Debug.Log("�̹� �Ĺ��� �ɾ��� �ֽ��ϴ�.");
                    }
                }
            }
        }
    }

    private bool IsPlantOnField()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");

        foreach (GameObject plant in plants)
        {
            if (plant.transform.parent == transform)
            {
                return true;
            }
        }

        return false;
    }
    private void Dig()
    {
        // ���� �κ��� ��Ÿ���� ���ο� �ؽ�ó�� ����
        renderer.material.mainTexture = newTexture;
    }

    private void RestoreOriginalTexture()
    {
        // ���� �ؽ�ó�� ����
        renderer.material.mainTexture = originalTexture;
    }

    void IncreaseGrowth()
    {
        // ���� ���� ������ ������Ŵ
        growthLevel++;
        Debug.Log("�Ĺ��� ������ �����߽��ϴ�. ���� ���� ����: " + growthLevel);


    }



    public void ToggleSprayer()
    {
        holdingSprayer = !holdingSprayer;

        // �й��⸦ ��ų� ���������� �� �й��� ��ġ�� ����
        if (holdingSprayer)
        {
            // �й��⸦ �÷��̾��� �� ��ġ�� �̵�
            Sprayer.transform.position = hand.position;
            Sprayer.transform.parent = hand; // �й��⸦ �տ� �θ�� ����
            Debug.Log("�й��⸦ ������ϴ�.");
        }
        else
        {
            // �й��⸦ �÷��̾� �տ� �� �ֵ��� ��ġ ����
            Sprayer.transform.position = transform.position + transform.forward * 2f;
            Sprayer.transform.parent = null; // �й����� �θ� �����Ͽ� ���� ��Ʈ�� ��ġ
            Debug.Log("�й��⸦ �������ҽ��ϴ�.");
        }
    }

    //������ ������� �ϴ� �ڷ�ƾ
    IEnumerator DisableObjectAfterDelay(float delay)
    {
        // ������ �� ���� ��ٸ�
        yield return new WaitForSeconds(delay);

        // ������Ʈ�� ��Ȱ��ȭ
        plant.gameObject.SetActive(false);
    }

    bool IsPlantOnField(GameObject field)
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach (GameObject plant in plants)
        {
            // ��� �Ĺ��� ��ġ�� ������ Ȯ��
            if (plant.transform.position == field.transform.position)
            {
                return true;
            }
        }

        return false;
    }
}

