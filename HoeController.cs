using UnityEngine;

public class HoeController : MonoBehaviour
{
    public float interactionRange = 100f; // ��ȣ�ۿ� ����
    public LayerMask groundLayer; // �� ���̾�
    public GameObject plantPrefab; // ���� �Ĺ� ������

    void Update()
    {
        // ���콺 ������ ��ư�� ������ ��
        if (Input.GetMouseButtonDown(1))
        {
            // �ֺ��� �ִ� ���� �˻��Ͽ� ���� ��� �������� Ȯ��
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, interactionRange, groundLayer))
            {
                // ���� �� �� �ִ� ��ġ���� �Ĺ� �ɱ� �õ�
                PlantAt(hit.point);
            }
        }
    } 

    void PlantAt(Vector3 position)
    {
        // ���� �Ĺ��� ���� ��ġ�� ���ο� �Ĺ� ����
        GameObject newPlant = Instantiate(plantPrefab, position, Quaternion.identity);
        // �Ĺ� ������Ʈ�� ���� �߰� ���� ����

        Debug.Log("�Ĺ��� �ɾ����ϴ�!");
    }
}