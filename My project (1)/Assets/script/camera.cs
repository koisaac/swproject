using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;            // ȸ�� �߽���
    public float rotationSpeed = 30f;   // ��/��

    private float currentAngle = 0f;
    private float radius = 0f;
    private float heightOffset = 0f;

    void Start()
    {
        // ���� ��ġ�� �߽��� ������ ����
        Vector3 diff = transform.position - target.position;

        // ����(Y)�� ���� �����ϰ�, XZ ������ �Ÿ��� ���
        heightOffset = diff.y;
        diff.y = 0f;

        radius = diff.magnitude; // ���� ������
        currentAngle = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg; // ���� ����
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); // A:-1, D:1
        if (false)
        {
            currentAngle += 0.5f * rotationSpeed * Time.deltaTime;

            // �������� ��ȯ
            float rad = currentAngle * Mathf.Deg2Rad;

            // �� ��ġ ��� (XZ ��鿡�� ���� �˵�)
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
            offset.y = heightOffset;

            transform.position = target.position + offset;

            // �׻� �߽��� �ٶ󺸰�
            transform.LookAt(target.position);
        }
    }
}
