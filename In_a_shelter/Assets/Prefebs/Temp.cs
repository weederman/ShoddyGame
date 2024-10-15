using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject sphereObject;  // Sphere ������Ʈ
    private MeshFilter meshFilter;

    void Start()
    {
        // ��ü���� �ݱ� �����
        Mesh mesh = sphereObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        // �ݱ��� ����� ���� Y�� �Ʒ��� �ִ� ���ؽ��� ����
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y < 0)  // Y�� �Ʒ��� �ִ� ���ؽ�
            {
                vertices[i] = Vector3.zero;  // �ش� ���ؽ��� �߽����� �̵� (����)
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();  // ���ؽ� ���� �� ��� ����
    }
}

