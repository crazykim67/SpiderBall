using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NoRing : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    void Update() => transform.Translate(Vector2.left * speed * Time.deltaTime);


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "DestroyNoRing")
            Destroy(this.gameObject);
    }
}
