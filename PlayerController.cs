using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Transform characterBody;
    [SerializeField]
    public Transform cameraArm;

    Animator animator;

    //���Ŀ� ������ ������ ��ü
    public Image water; //���ִ� �� 
    public Image seed; //���ѻѸ��� �� 
    public Image plant; //����, ��Ȯ ��

    public GameObject SprayerPrefab; // �й��� ������Ʈ
    public bool holdingSprayer = false; // �й��⸦ ��� �ִ��� ����
    private GameObject heldSprayer;   // ��� �ִ� �й��� ������Ʈ


    public GameObject ShovelPrefab;  // ���� ������Ʈ
    public bool holdingShoveler = false; // ���̸� ��� �ִ��� ����
    private GameObject heldShovel;   // ��� �ִ� ���� ������Ʈ


    public GameObject SeedPrefab; // seed ������
    private GameObject heldSeed; // ��� �ִ� ���� ������Ʈ

    public GameObject SeedPrefab2; // �� ��° �Ĺ��� ���� ������
    private GameObject heldSeed2; // ��� �ִ� �� ��° �Ĺ��� ���� ������Ʈ
    public bool holdingSeed2 = false; // �� ��° �Ĺ��� ������ ��� �ִ��� ����

    public GameObject SeedPrefab3; // �� ��° �Ĺ��� ���� ������
    private GameObject heldSeed3; // ��� �ִ� �� ��° �Ĺ��� ���� ������Ʈ
    public bool holdingSeed3 = false; // �� ��° �Ĺ��� ������ ��� �ִ��� ����

    private GameObject heldItem; // ���� ��� �ִ� ������
    public Transform hand; // �÷��̾��� �� ��ġ

    public bool holdingSeed = false; // ������ ��� �ִ��� ����

    public delegate void SprayAction(bool holdingSprayer); // �й��⸦ ��ų� �������� �� ȣ��� ��������Ʈ
    public static event SprayAction OnSprayerToggled;

    public delegate void ShovelAction(bool holdingSprayer); //���̸� ��ų� �������� �� ȣ��� ��������Ʈ
    public static event ShovelAction OnShovelerToggled;

    private GameObject currentSprayer;
    // �������� ���� ������ PlayerController �ν��Ͻ�

    public static PlayerController Instance { get; private set; }

    private bool isAnimating = false; //�ִϸ��̼� ������ ���θ� ��Ÿ���� ����

    public delegate void ToolStateChanged(); //�̺�Ʈ �߻��� ���� �߰�
    public static event ToolStateChanged OnToolStateChanged;

    public AudioSource audioSource;     // AudioSource ������Ʈ
    public AudioClip plantSound;      // ����� ����� Ŭ��




    void Start()
    {
      
        // 'Hand_R' ������Ʈ�� ã�Ƽ� hand ������ �Ҵ�
        hand = transform.Find("Hand");

        //// ĳ���Ϳ� �پ��ִ� �ִϸ����� ������Ʈ�� �����ͼ� ����. 
        animator = characterBody.GetComponent<Animator>();

        // hand ������Ʈ�� �θ� ĳ������ Transform���� ����
        hand.SetParent(characterBody);

    }

    void Update()
    {
        LookAround(); // �þ߸� ȸ����Ű�� �Լ� (ī�޶� ���� ��)
        Move();       // �÷��̾� �̵� ó��

        Action();     // �Ϲ����� ���� ����

        // �й��� ���
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleSprayer();

            // �й��⸦ �տ� ��� ���� �� �ٸ� �������� �տ��� ����
            if (holdingSprayer)
            {
                if (holdingShoveler)
                {
                    ToggleShoveler();
                }
                if (holdingSeed)
                {
                    TogglePlantSeed();
                }
                if (holdingSeed2)
                {
                    TogglePlantSeed2();
                }
                if (holdingSeed3)
                {
                    TogglePlantSeed3();
                }
            }
        }

        // ���� ���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleShoveler();

            // ���̸� �տ� ��� ���� �� �ٸ� �������� �տ��� ����
            if (holdingShoveler)
            {
                if (holdingSprayer)
                {
                    ToggleSprayer();
                }
                if (holdingSeed)
                {
                    TogglePlantSeed();
                }
                if (holdingSeed2)
                {
                    TogglePlantSeed2();
                }
                if (holdingSeed3)
                {
                    TogglePlantSeed3();
                }
            }
        }

        // ���� �ɱ� ���
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TogglePlantSeed();
            SeedPrefab.transform.GetComponent<Collider>().enabled = false;

            // ������ �տ� ��� ���� �� �ٸ� �������� �տ��� ����
            if (holdingSeed)
            {
                if (holdingSprayer)
                {
                    ToggleSprayer();
                }
                if (holdingShoveler)
                {
                    ToggleShoveler();
                }
                if (holdingSeed2)
                {
                    TogglePlantSeed2();
                }
                if (holdingSeed3)
                {
                    TogglePlantSeed3();
                }
            }
        }

        // ����2 �ɱ� ���
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TogglePlantSeed2();
            SeedPrefab2.transform.GetComponent<Collider>().enabled = false;

            // ����2�� �տ� ��� ���� �� �ٸ� �������� �տ��� ����
            if (holdingSeed2)
            {
                if (holdingSprayer)
                {
                    ToggleSprayer();
                }
                if (holdingShoveler)
                {
                    ToggleShoveler();
                }
                if (holdingSeed)
                {
                    TogglePlantSeed();
                }
                if (holdingSeed3)
                {
                    TogglePlantSeed3();
                }
            }
        }

        // ����3 �ɱ� ���
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TogglePlantSeed3();

            SeedPrefab3.transform.GetComponent<Collider>().enabled = false;

            // ����3�� �տ� ��� ���� �� �ٸ� �������� �տ��� ����
            if (holdingSeed3)
            {
                if (holdingSprayer)
                {
                    ToggleSprayer();
                }
                if (holdingShoveler)
                {
                    ToggleShoveler();
                }
                if (holdingSeed)
                {
                    TogglePlantSeed();
                }
                if (holdingSeed2)
                {
                    TogglePlantSeed2();
                }
            }
        }

        // ���콺 ��Ŭ������ ���� �ɱ�
        if (Input.GetMouseButtonDown(1))
        {
            PlantSeed();
        }
      
       OnToolStateChanged += HandleActions;
    }
    void LateUpdate() //Update���� ȣ���ϸ� �÷��̾ ī�޶� �ٶ󺸴� ���� �� ��
    {
        LookAround(); //ȸ�� �޼���
    }
    void PlantSeed()
    {
        // ������ ��� �ְ�, ���콺�� ���� Ŭ���� ��쿡�� ����
        if ( holdingSeed || holdingSeed2 || holdingSeed3  )
        {
            // ���콺�� Ŭ���� �������� ray�� �߻��Ͽ� �浹�� ��ü ������ ������
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit))
            {
                if ( hit.collider.gameObject.CompareTag("FarmField")  && hit.collider.GetComponent<FarmFieldInteraction>().IsDigged)
                {
                    
                    GameObject plantImage;
                    if (holdingSeed)
                    {
                        // ������ �ɾ����� �ݶ��̴� Ȱ��ȭ
                        SeedPrefab.transform.GetComponent<Collider>().enabled = true;

                        plantImage = Instantiate(SeedPrefab, hit.point, Quaternion.identity);
                    }
                    if (holdingSeed2)
                    {
                        SeedPrefab2.transform.GetComponent<Collider>().enabled = true;

                        plantImage = Instantiate(SeedPrefab2, hit.point, Quaternion.identity);
                    }
                    if (holdingSeed3)
                    {
                        SeedPrefab3.transform.GetComponent<Collider>().enabled = true;

                        plantImage = Instantiate(SeedPrefab3, hit.point, Quaternion.identity);
                    }
                    Debug.Log("������ �翡 ����");

                    audioSource.PlayOneShot(plantSound);
                    animator.SetBool("isAction", true);
                }
                else
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
            else
            {
                animator.SetBool("isAction", false); // �߰�: �ִϸ��̼��� ��Ȱ��ȭ�ϴ� �κ�

                Debug.Log("��������");
            }
        }
    }

    public void TogglePlantSeed3()
    {

       // holdingSeed3 = !holdingSeed3; // ������ ��ų� ��������
        holdingSeed3 = !holdingSeed3;
        // ����3 ���� ���� �̺�Ʈ �߻�
        OnToolStateChanged?.Invoke(); //������Ʈ ��Ȱ��ȭ�� �� Action�ִϸ��̼� ��� �ȵǰ� �ϴ� �ڵ�
        
        if (holdingSeed3)
        {
            // ������ �÷��̾��� �� ��ġ�� �̵���Ŵ
            SeedPrefab3.transform.position = hand.position;
            heldSeed3 = Instantiate(SeedPrefab3, hand.position, Quaternion.identity);
            heldSeed3.transform.parent = hand; // ������ ���� �ڽ����� ����

            holdingSeed3 = true;
            Debug.Log("���� ���");

            seed.gameObject.SetActive(true); //���� Ȱ��ȭ
        }
        else
        {
            // ������ �÷��̾� �տ� �� �ֵ��� ��ġ ����
            SeedPrefab3.transform.position = transform.position + transform.forward * 2f; //ĳ���� ������ 2m �̵�
            SeedPrefab3.transform.parent = null; // ������ �θ� �����Ͽ� ���� ��Ʈ�� ��ġ
            holdingSeed3 = false;

            // ���� ��Ȱ��ȭ
            Destroy(heldSeed3);
            heldSeed3.SetActive(false);
            Debug.Log("���� ��������");

            seed.gameObject.SetActive(false); //���� ��Ȱ��ȭ
        }
    }
    public void TogglePlantSeed2()
    {
        holdingSeed2 = !holdingSeed2;
        //����2 ���� ���� �̺�Ʈ �߻� 
        OnToolStateChanged?.Invoke(); //������Ʈ ��Ȱ��ȭ�� �� Action�ִϸ��̼� ��� �ȵǰ� �ϴ� �ڵ�
        //holdingSeed2 = !holdingSeed2; // ������ ��ų� ��������

        if (holdingSeed2)
        {
            // ������ �÷��̾��� �� ��ġ�� �̵���Ŵ
            SeedPrefab2.transform.position = hand.position;
            heldSeed2 = Instantiate(SeedPrefab2, hand.position, Quaternion.identity);
            heldSeed2.transform.parent = hand; // ������ ���� �ڽ����� ����

            holdingSeed2 = true;
            SeedPrefab3.transform.GetComponent<Collider>().enabled = false;
            Debug.Log("���� ���");

            seed.gameObject.SetActive(true); //���� Ȱ��ȭ
        }
        else
        {
            // ������ �÷��̾� �տ� �� �ֵ��� ��ġ ����
            SeedPrefab2.transform.position = transform.position + transform.forward * 2f; // ĳ���� ������ 2m �̵�
            SeedPrefab2.transform.parent = null; // ������ �θ� �����Ͽ� ���� ��Ʈ�� ��ġ
            holdingSeed2 = false;

            // ���� ��Ȱ��ȭ
            Destroy(heldSeed2);
            heldSeed2.SetActive(false);
            SeedPrefab2.transform.GetComponent<Collider>().enabled = false;
            Debug.Log("���� ��������");

            seed.gameObject.SetActive(false); //���� ��Ȱ��ȭ
        }
    }
    public void TogglePlantSeed()
    {
        holdingSeed = !holdingSeed;
        //����1 ���� ���� �̺�Ʈ �߻�
        OnToolStateChanged?.Invoke(); //������Ʈ ��Ȱ��ȭ�� �� Action�ִϸ��̼� ��� �ȵǰ� �ϴ� �ڵ�

        if (holdingSeed)
        {
            // ������ �÷��̾��� �� ��ġ�� �̵���Ŵ
            SeedPrefab.transform.position = hand.position;
            heldSeed = Instantiate(SeedPrefab, hand.position, Quaternion.identity);
            heldSeed.transform.parent = hand; // ������ ���� �ڽ����� ����

            holdingSeed = true;
            Debug.Log("���� ���");

            seed.gameObject.SetActive(true); //���� Ȱ��ȭ
        }
        else
        {
            // ������ �÷��̾� �տ� �� �ֵ��� ��ġ ����
            SeedPrefab.transform.position = transform.position + transform.forward * 2f; //ĳ���� ������ 2m �̵�
            SeedPrefab.transform.parent = null; // ������ �θ� �����Ͽ� ���� ��Ʈ�� ��ġ
            holdingSeed = false;

            // ���� ��Ȱ��ȭ
            Destroy(heldSeed);
            heldSeed.SetActive(false);
            SeedPrefab.transform.GetComponent<Collider>().enabled = false;
            Debug.Log("���� ��������");

            seed.gameObject.SetActive(false); //���� ��Ȱ��ȭ
        }
    }

    public void ToggleShoveler()
    {
        holdingShoveler = !holdingShoveler;
        //���� ���� ���� �̺�Ʈ �߻�
        OnToolStateChanged?.Invoke(); //������Ʈ ��Ȱ��ȭ�� �� Action�ִϸ��̼� ��� �ȵǰ� �ϴ� �ڵ�
    

        if (holdingShoveler)
        {

            // ���̸� �÷��̾��� �� ��ġ�� �̵���Ŵ
            heldShovel = Instantiate(ShovelPrefab, hand.position, Quaternion.identity);
            heldShovel.transform.parent = hand; // ���̸� �տ� �θ�� ����
            // ������ �����̼��� �����Ͽ� ���ϴ� �������� ȸ����Ŵ
            heldShovel.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            holdingShoveler = true;
            Debug.Log("���� ����");

            plant.gameObject.SetActive(true); //���� Ȱ��ȭ
        }
        else
        {
            // ���̸� �÷��̾� �տ� �� �ֵ��� ��ġ ����
            Destroy(heldShovel); // ���� ������Ʈ�� ����
            holdingShoveler = false;
            Debug.Log("���� ����");

            plant.gameObject.SetActive(false); //���� ��Ȱ��ȭ

        }
        // �̺�Ʈ �߻�
        OnShovelerToggled?.Invoke(holdingShoveler);
    }

    public void ToggleSprayer()
    {
        holdingSprayer = !holdingSprayer;
        //�й��� ���� ���� �̺�Ʈ �߻�
        OnToolStateChanged?.Invoke(); //������Ʈ ��Ȱ��ȭ�� �� Action�ִϸ��̼� ��� �ȵǰ� �ϴ� �ڵ�


        if (holdingSprayer)
        {

            // ���̸� �÷��̾��� �� ��ġ�� �̵���Ŵ
            heldSprayer = Instantiate(SprayerPrefab, hand.position, Quaternion.identity);
            heldSprayer.transform.parent = hand; // ���̸� �տ� �θ�� ����
            // ������ �����̼��� �����Ͽ� ���ϴ� �������� ȸ����Ŵ
            heldSprayer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            holdingSprayer = true;
            Debug.Log("�й��� ����");

            water.gameObject.SetActive(true); //���Ѹ��� Ȱ��ȭ
        }

        else
        {
            // �й��⸦ �÷��̾� �տ� �� �ֵ��� ��ġ ����
            Destroy(heldSprayer); // ���� ������Ʈ�� ����
            holdingSprayer = false;
            Debug.Log("�й��� ����");
            water.gameObject.SetActive(false); //���Ѹ��� ��Ȱ��ȭ
        }
        // �̺�Ʈ �߻�
        OnSprayerToggled?.Invoke(holdingSprayer);
    }

    private void Move()
    {
        // Input.GetAxis�� ���� ���� �̵� �Է°��� ������.
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // moveInput�� 0�̸� �̵��� �߻����� �ʴ� ���̵� 0�� �ƴϸ� �̵��� �߻��ϴ� ��.
        bool isMove = moveInput.magnitude != 0;

        // ismove�� �߻��ϸ� �ȴ� �ִϸ��̼� �۵� ismove�� �߻����� �ʴ� �ٸ� ��� �ִϸ��̼� �۵�
        animator.SetBool("isWalk", isMove);

        if (isMove)
        {
            // ���ȭ ���Ѽ� ����
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            // �ٶ󺸰� �ִ� �������� �̵�
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            // ĳ���Ͱ� �̵��� �� ī�޶� �ٶ󺸴� �������� �ٶ󺸰� ��.
            characterBody.forward = lookForward;
            // ĳ���� �̵�
            transform.position += moveDir * Time.deltaTime * 5f;

            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", false);

            //// ĳ���� �޸���
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    transform.position += moveDir * Time.deltaTime * 8f;
            //    animator.SetBool("isWalk", false);
            //    animator.SetBool("isRun", true);
            //}
            //else
            //{
            //    animator.SetBool("isRun", false);
            //}
        }
    }

    // ���콺 �����ӿ� ���� ī�޶� ȸ��.
    private void LookAround()
    {
        // ���� ��ġ���� �󸶳� ���������� �˰� ���ְ� mouseDelta ������ ����
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // ī�޶� ���� ȸ�� ���� ���Ϸ� ������ ��ȯ.
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        // ī�޶� ������ ���� ���� ����.
        // ī�޶� ���� ������ ����.
        // x�� 180�� ���� ���� ���� ���� ȸ�� �ϴ� ���.
        // 0���� �ƴ� -1���� ������ ���� �ϴ� ������ 0���� �ϸ� ī�޶� ����� ���Ϸ� �������� �ʴ� ������ �߻�.
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }

        else
        {
            // x���� 180���� ū ���� �Ʒ��� ȸ�� �ϴ� ���.
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        //�ִϸ��̼� �߰��ϸ鼭 �÷��̾ ī���� ���� �ٶ󺸱� ���� �Ʒ� �ڵ� �߰�
        characterBody.rotation = Quaternion.Euler(0f, cameraArm.rotation.eulerAngles.y, 0f);

    }
    public void Action()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            animator.SetBool("isAction", true);
        }
        else
        {
            animator.SetBool("isAction", false); // �߰�: �ִϸ��̼��� ��Ȱ��ȭ�ϴ� �κ�
        }

    }

    void HandleActions() //��� �ִ� ������Ʈ�� ���� �´� �ڷ�ƾ ����
    {
        // �й���, ����, ���� ���¿� ���� ó���� �۾� ����
        if (holdingSprayer)
        {
            // �й���
            if (water != null)
            {
                StartCoroutine(PlayActionAnimation("Action", water));
            }
        }
        else if (holdingShoveler)
        {
            // ����
            if (plant != null)
            {
                StartCoroutine(PlayActionAnimation("Action", plant));
            }
        }
        else if (holdingSeed || holdingSeed2 || holdingSeed3)
        {
            // ����
            if (seed != null)
            {
                StartCoroutine(PlayActionAnimation("Action", seed));
            }
        }
    }
    IEnumerator PlayActionAnimation(string animationName, Image actionImage)
    {
        isAnimating = true;
        animator.SetTrigger("isAction");

        if (actionImage != null)
        {
            actionImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.6f);
            actionImage.gameObject.SetActive(false);
        }

        isAnimating = false;

        // �ִϸ��̼��� ���� �� Idle ���·� ���ư���
        animator.Play("Idle");
    }
}