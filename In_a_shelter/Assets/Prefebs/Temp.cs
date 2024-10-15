using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject sphereObject;  // Sphere 오브젝트
    private MeshFilter meshFilter;

    void Start()
    {
        // 구체에서 반구 만들기
        Mesh mesh = sphereObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        // 반구만 남기기 위해 Y축 아래에 있는 버텍스를 제거
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y < 0)  // Y축 아래에 있는 버텍스
            {
                vertices[i] = Vector3.zero;  // 해당 버텍스를 중심으로 이동 (없앰)
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();  // 버텍스 수정 후 노멀 재계산
    }
}

