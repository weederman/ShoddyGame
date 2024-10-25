using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        var main=particleSystem.main;
        main.startSize = 0.5f;
        main.startColor = Color.white;

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.mesh = Resources.GetBuiltinResource<Mesh>("Ring.fbx");
    }

    void Update()
    {
        particleSystem.Play();
    }
}
