using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ��� �߻�� ��ġ
    public float bulletSpeed = 10f; // �Ѿ� �߻� �ӵ�
    public float detectionRange = 10f; // �ܰ����� ���� ����

    public float rotationSpeed = 5f; // �ܰ����� ȸ�� �ӵ�

    public float swayAmount = 10f; // ������ �¿�� �����̴� �ִ� ����
    public float swaySpeed = 2f; // ������ �¿�� �����̴� �ӵ�

    private Quaternion startRotation; // ������ ���� ȸ����

    private GameObject player; // �÷��̾� ��ü
    public bool isShooting = false; // �Ѿ� �߻� ���θ� ��Ÿ���� ����
    private bool canShoot = true; // �ܰ����� ���� �߻��� �� �ִ��� ���θ� ��Ÿ���� ����

    public AudioSource audioSource;     // AudioSource ������Ʈ
    public AudioClip shoothingSound;      //  ����� Ŭ��

    void Start()
    {
       // InvokeRepeating("FireBullet", 0f, 2f); // ���� �� 2�ʸ��� FireBullet �޼��带 �ݺ� ȣ��
        player = GameObject.FindGameObjectWithTag("Player");

        startRotation = transform.rotation; // ������ ���� ȸ���� ����
    }
    void Update()
    {
        if (player != null)
        {
            // �÷��̾�� �ܰ����� �Ÿ��� ���
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // �÷��̾ ���� ���� ���� ������ �Ѿ� �߻�
            if (distanceToPlayer <= detectionRange && !isShooting && canShoot)
            {
                
                isShooting = true;
                InvokeRepeating("Shoot", 0f, 2f); // 2�ʸ��� Shoot �޼��带 �ݺ� ȣ��
            }
            else if (distanceToPlayer > detectionRange && isShooting)
            {
                isShooting = false;
                CancelInvoke("Shoot"); // �߻� ����
            }
            // �÷��̾ �ٶ󺸴� ȸ��
            RotateTowardsPlayer();

           
        }
    }
    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Y �� ���� ȸ�� ����
        lookRotation.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
    //Vector3 direction = (player.transform.position - transform.position).normalized;
    //Quaternion lookRotation = Quaternion.LookRotation(direction);
    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            audioSource.PlayOneShot(shoothingSound);
            Vector3 direction = (player.transform.position - bulletSpawnPoint.position).normalized;
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
    //private void FireBullet()
    //{
    //    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    //    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
    //    if (bulletRigidbody != null)
    //    {
    //        Vector3 direction = (PlayerPosition() - bulletSpawnPoint.position).normalized;
    //        bulletRigidbody.velocity = direction * bulletSpeed;
    //    }
    //}

    private Vector3 PlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector3.zero;
    }
}

