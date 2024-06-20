using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f; // �Ѿ� �߻� �ӵ�
    public int damage = 20; // �Ѿ� ������
    public float fireRate = 2f; // �Ѿ� �߻� ����

    void Start()
    {
        Rigidbody bulletRigidbody = GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            Destroy(gameObject, 3f); // �Ѿ� ������Ʈ�� 3�� �Ŀ� �ı��մϴ�.
        }
    }

    public void Fire(Vector3 direction)
    {
        // �ڷ�ƾ�� ����Ͽ� ���� �������� �Ѿ��� �߻��մϴ�.
        StartCoroutine(FireRoutine(direction));
    }

    IEnumerator FireRoutine(Vector3 direction)
    {
        while (true)
        {
            // �Ѿ��� �߻��մϴ�.
            Rigidbody bulletRigidbody = GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bulletSpeed;

            }

            // ���� �߻縦 ��ٸ��ϴ�.
            yield return new WaitForSeconds(fireRate);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �浹�� ����� �÷��̾����� Ȯ���մϴ�.
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� �浹 ����");
            // �÷��̾�� �������� �����ϴ�.
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("�÷��̾� ü�� -20");
                playerHealth.TakeDamage(damage);
            }

            // �Ѿ��� �ı��մϴ�.
            Destroy(gameObject);
        }
    }
}
