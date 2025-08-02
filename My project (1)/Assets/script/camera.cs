using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;            // 회전 중심점
    public float rotationSpeed = 30f;   // 도/초

    private float currentAngle = 0f;
    private float radius = 0f;
    private float heightOffset = 0f;

    void Start()
    {
        // 현재 위치와 중심점 사이의 벡터
        Vector3 diff = transform.position - target.position;

        // 높이(Y)는 따로 저장하고, XZ 평면상의 거리만 계산
        heightOffset = diff.y;
        diff.y = 0f;

        radius = diff.magnitude; // 시작 반지름
        currentAngle = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg; // 시작 각도
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); // A:-1, D:1
        if (false)
        {
            currentAngle += 0.5f * rotationSpeed * Time.deltaTime;

            // 라디안으로 변환
            float rad = currentAngle * Mathf.Deg2Rad;

            // 새 위치 계산 (XZ 평면에서 원형 궤도)
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
            offset.y = heightOffset;

            transform.position = target.position + offset;

            // 항상 중심점 바라보게
            transform.LookAt(target.position);
        }
    }
}
