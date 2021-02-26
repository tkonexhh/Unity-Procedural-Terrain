using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    int depth = 4;

    [SerializeField] private Mesh mesh = default;
    [SerializeField] private Material material = default;

    static Vector3[] dir = { Vector3.up, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
    static Quaternion[] rotations ={Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f), Quaternion.Euler(-90f, 0f, 0f)};
    FractalPart[][] parts;

    struct FractalPart
    {
        public Vector3 direction;
        public Quaternion rotation;
        public Transform transform;
    }

    private void Awake()
    {
        parts = new FractalPart[depth][];
        parts[0] = new FractalPart[1];
        for (int i = 0, size = 0; i < parts.Length; i++, size *= 5)
        {
            parts[i] = new FractalPart[size];
        }
        CreatePart(0);
    }

    private void CreatePart(int levelIndex)
    {
        var go = new GameObject("Fractal Part " + levelIndex);
        go.transform.SetParent(transform, false);
        go.AddComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>().material = material;
    }

    // private void Start()
    // {
    //     name = "Fractal " + depth;
    //     if (depth <= 1)
    //         return;

    //     Fractal childUp = CreateChild(Vector3.up, Quaternion.identity);
    //     Fractal childRight = CreateChild(Vector3.right, Quaternion.Euler(0f, 0f, -90f));
    //     Fractal childleft = CreateChild(Vector3.left, Quaternion.Euler(0f, 0f, 90f));
    //     Fractal childforward = CreateChild(Vector3.forward, Quaternion.Euler(90f, 0f, 0f));
    //     Fractal childback = CreateChild(Vector3.back, Quaternion.Euler(-90f, 0f, 0f));

    //     childUp.transform.SetParent(transform, false);
    //     childRight.transform.SetParent(transform, false);
    //     childleft.transform.SetParent(transform, false);
    //     childforward.transform.SetParent(transform, false);
    //     childback.transform.SetParent(transform, false);
    // }

    private Fractal CreateChild(Vector3 direction, Quaternion rotation)
    {
        Fractal child = Instantiate(this);
        child.depth = depth - 1;
        child.transform.localPosition = direction * 0.75f;
        child.transform.localRotation = rotation;
        child.transform.localScale = Vector3.one * 0.5f;
        return child;
    }

    private void Update()
    {
        transform.Rotate(0f, 22.5f * Time.deltaTime, 0f);
    }
}
