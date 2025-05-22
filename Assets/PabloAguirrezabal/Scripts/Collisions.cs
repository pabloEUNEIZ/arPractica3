using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ColisionEn " + other.gameObject.transform.parent.gameObject.name);
        GameObject.Find("Manager").GetComponent<GameManager>().FlechaEncontrada(other.gameObject.transform.parent.gameObject.name);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("ColisionEx " + other.gameObject.name);

    }
}
