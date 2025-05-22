using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text cam;
    public TMPro.TMP_Text image;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        actualizarCam(Camera.main.transform.position.ToString() + Camera.main.transform.eulerAngles.ToString());
    }

    public void actualizarCam(string value)
    {
        cam.text = value;
    }

    public void actualizarImage(string value)
    {
        image.text = value;
    }
}
