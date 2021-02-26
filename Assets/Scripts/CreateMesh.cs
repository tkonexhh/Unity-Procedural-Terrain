using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{

    public int xSize = 20, zSize = 20;
    private Mesh m_Mesh;
    private Vector3[] m_Vertices;
    private int[] m_Triangles;
    // private int 

    // Start is called before the first frame update
    void Start()
    {
        m_Mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = m_Mesh;
        CreateShape();
        UpdateMesh();
    }



    void CreateShape()
    {
        m_Vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                m_Vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;
        m_Triangles = new int[xSize * zSize * 6];
        for (int x = 0; x < xSize; x++)
        {
            // Debug.LogError(x);
            m_Triangles[vert + 0] = vert + 0;
            m_Triangles[vert + 1] = vert + xSize + 1;
            m_Triangles[vert + 2] = vert + 1;
            m_Triangles[vert + 3] = vert + 1;
            m_Triangles[vert + 4] = vert + xSize + 1;
            m_Triangles[vert + 5] = vert + xSize + 2;
            vert++;
            tris += 6;
        }

    }

    private void UpdateMesh()
    {
        m_Mesh.Clear();
        m_Mesh.vertices = m_Vertices;
        m_Mesh.triangles = m_Triangles;
        m_Mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Vertices != null)
        {
            for (int i = 0; i < m_Vertices.Length; i++)
            {
                Gizmos.DrawSphere(m_Vertices[i], 0.1f);
            }
        }
    }
}