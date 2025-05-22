using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject ruta1;
    public GameObject goPatron;
    public GameObject goRuta;

    private GameObject parentGameObject; // Asigna el GameObject padre desde el Inspector
    private List<GameObject> childrenList = new List<GameObject>();

    private Vector3 _editorRelativePosition;
    private Quaternion _editorRelativeRotation;
    private Vector3 _editorRelativeScale;

    void Awake()
    {
        _editorRelativePosition = goPatron.transform.InverseTransformPoint(goRuta.transform.position);
        Debug.Log("_editorRelativePosition "+_editorRelativePosition);
        _editorRelativeRotation = Quaternion.Inverse(goPatron.transform.rotation) * goRuta.transform.rotation;
        Debug.Log("_editorRelativeRotation "+_editorRelativeRotation);

        _editorRelativeScale = goPatron.transform.localScale; // Guarda la escala original del cubo
        Debug.Log("_editorRelativeScale "+_editorRelativeScale);

        goPatron.SetActive(false);
        goRuta.SetActive(false);
    }

    void InicializarLista()
    {
        if (parentGameObject != null)
        {
            GetAllChildrenOrdered(parentGameObject.transform);

            // Imprimir los nombres de los hijos para verificar el orden
            foreach (GameObject child in childrenList)
            {
                Debug.Log(child.name);
            }
        }
        else
        {
            Debug.LogError("Parent GameObject no asignado.");
        }
    }

    void GetAllChildrenOrdered(Transform parent)
    {
        // Limpiar la lista por si se llama múltiples veces
        childrenList.Clear();

        // Iterar sobre cada Transform hijo del padre
        foreach (Transform childTransform in parent)
        { 
            Debug.Log("hijo"+childTransform.gameObject.name);
            if (!childTransform.gameObject.name.Equals("0"))
            {
                childTransform.gameObject.SetActive(false);
            }
            // Añadir el GameObject del hijo a la lista
            childrenList.Add(childTransform.gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlechaEncontrada(string value)
    {
        GameObject.Find("AudioSource").GetComponent<AudioSource>().Play();
        Debug.Log("value"+value);
        // GameObject.Find(value).SetActive(false);
        // GameObject.Find(((int.Parse(value))+1).ToString()).SetActive(false);
        childrenList[int.Parse(value)].SetActive(false);
        childrenList[(int.Parse(value))+1].SetActive(true);
    }

    public void ImagenEncontrada(string value, Transform arImageRuntimeTransform)
    {
            
            parentGameObject = GameObject.Instantiate(ruta1);
            arImageRuntimeTransform.localScale = _editorRelativeScale;
            parentGameObject.transform.parent = arImageRuntimeTransform;
            parentGameObject.transform.position = arImageRuntimeTransform.TransformPoint(_editorRelativePosition);
            parentGameObject.transform.rotation = arImageRuntimeTransform.rotation * _editorRelativeRotation;
            //parentGameObject.transform.localScale = _editorRelativeScale; // Restaura su escala original

            InicializarLista();
    }
    


}
