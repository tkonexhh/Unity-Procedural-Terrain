using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Jobs;

public class DrawMesh : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    Matrix4x4[] matrices = new Matrix4x4[1023];

    MaterialPropertyBlock block;
    Vector4[] baseColors = new Vector4[1023];

    static int baseColorID = Shader.PropertyToID("_BaseColor");
    private BatchRendererGroup batchRendererGroup;
    private void Awake()
    {
        //batchRendererGroup = new BatchRendererGroup(OnPerformCulling);

        for (int i = 0; i < matrices.Length; i++)
        {
            matrices[i] = Matrix4x4.TRS(
                Random.insideUnitSphere * 10f,
                Quaternion.identity,
                Vector3.one * Random.Range(0.5f, 1.5f)
            );

            baseColors[i] = new Vector4(Random.value, Random.value, Random.value, 1);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
            block.SetVectorArray(baseColorID, baseColors);
        }
        for (int i = 0; i < 10; i++)
        {
            Graphics.DrawMeshInstanced(mesh, 0, material, matrices, matrices.Length, block);
        }
        // Graphics.DrawMeshInstanced(mesh, 0, material, matrices, matrices.Length, block);
    }

    // private JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cullingContext)
    // {
    //     var cull = new MyCullJob()
    //     {

    //     }

    //     return cull;
    // }
}
