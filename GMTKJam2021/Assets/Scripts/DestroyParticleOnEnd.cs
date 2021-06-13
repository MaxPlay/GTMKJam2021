using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleOnEnd : MonoBehaviour
{
    List<ParticleSystem> systems;

    // Start is called before the first frame update
    void Start()
    {
        systems = new List<ParticleSystem>();
        systems.Add(GetComponent<ParticleSystem>());
        foreach(ParticleSystem newSystem in GetComponentsInChildren<ParticleSystem>())
        {
            systems.Add(newSystem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool activeSystems = false;
        for (int i = 0; i < systems.Count; i++)
        {
            if (systems[i].isPlaying)
                activeSystems = true;
        }
        if (!activeSystems)
            Destroy(gameObject);
    }
}
