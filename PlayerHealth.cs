using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // ���� ü��
    public int currentHealth { get; private set; } // ���� ü��
    public int maxHealth = 100; // �ִ� ü��
    public Image damageImage; // UI �̹����� ������ ����
    private Coroutine damageCoroutine; // �ڷ�ƾ ���� ����

    public AudioSource audioSource;     // AudioSource ������Ʈ
    public AudioClip recoverhealthSound;      //  ����� Ŭ��
    private bool invincible = false; // ���� ���� ���θ� ��Ÿ���� ����

    void Start()
    {
        currentHealth = startingHealth;

    }

    // �÷��̾ ���ظ� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int amount)
    {

        // ���� ������ ���� ���ظ� ���� ����
        if (invincible)
            return;

        currentHealth -= amount;

        damageImage.gameObject.SetActive(true);

        // ������ ���� ���� �ڷ�ƾ�� ������ ����
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }

        // 0.5�� �Ŀ� UI �̹����� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        damageCoroutine = StartCoroutine(DisableDamageImageAfterDelay());

        // ü���� 0 ���Ϸ� �������� �� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �÷��̾ ������� �� ȣ��Ǵ� �Լ�
    void Die()
    {

        Debug.Log("Player died!");

        // ���� ���� ������ ��ȯ
        SceneManager.LoadScene("BadEnding");


    }

    //ü�� ȸ�� �޼���
    public void RecoverHealth(int amount)
    {
        currentHealth += amount;

        //ü���� �ִ� ü���� ���� �ʵ��� ó��
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //������ �浹 ó��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HpItem"))
        {
            Debug.Log("Hp +20");

            //ü�� ȸ�� ó��
            RecoverHealth(20);

            audioSource.PlayOneShot(recoverhealthSound);

            //HpItem ������Ʈ ����
            Destroy(other.gameObject);
        }
    }
    // ���� UI �̹����� ���� �ð� �Ŀ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ �޼���
    IEnumerator DisableDamageImageAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);

        if (damageImage != null)
        {
            damageImage.gameObject.SetActive(false);
        }
    }

    // ���� ���� ����
    public void SetInvincible(bool value)
    {
        invincible = value;
    }
}
