using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitOutParticles : MonoBehaviour
{
    public Rigidbody2D particle;

    //time between generating particles
    public float generationRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateParticles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GenerateParticles()
    {
        while (true)
        {
            Rigidbody2D p = Instantiate(particle, transform.position + new Vector3(Random.Range(-20f, 20f), 0f, 0f),
                transform.rotation);
            p.velocity = new Vector3(0f, Random.Range(-1f, -.1f), 0f);
            p.angularVelocity = Random.Range(-90f, 90f);

            yield return new WaitForSecondsRealtime(generationRate);
        }
        
    }
}
