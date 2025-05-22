using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems; // Necesario para TrackingState

public class LogTrackedImages : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
   

    void Awake()
    {
        // Intenta encontrar el ARTrackedImageManager en la escena.
        // Es mejor si lo asignas manualmente en el Inspector para mayor robustez.
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        if (trackedImageManager == null)
        {
            Debug.LogError("LogTrackedImages: ARTrackedImageManager no encontrado en la escena.");
            enabled = false; // Deshabilita este script si no se encuentra el manager.
        }
    }

    void OnEnable()
    {
        if (trackedImageManager != null)
        {
            // Suscribirse al evento cuando el script se habilita.
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    void OnDisable()
    {
        if (trackedImageManager != null)
        {
            // Darse de baja del evento cuando el script se deshabilita o destruye.
            // Esto es muy importante para evitar errores.
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }


    

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Log general para saber que la función se ha llamado
        Debug.LogWarning($"EVENTO TrackedImagesChanged DISPARADO! Imágenes añadidas: {eventArgs.added.Count}, Actualizadas: {eventArgs.updated.Count}, Eliminadas: {eventArgs.removed.Count}");

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            Debug.Log($"IMAGEN AÑADIDA (Evento): Nombre='{trackedImage.referenceImage.name}', Estado={trackedImage.trackingState}, Posición={trackedImage.transform.position}");
            Debug.Log($"Nombre de la imagen: {trackedImage.referenceImage.name}");
            Debug.Log($"Posición: {trackedImage.transform.position}");
            Debug.Log($"Rotación: {trackedImage.transform.rotation}");
            Debug.Log($"RotaciónE: {trackedImage.transform.eulerAngles}");
            Debug.Log($"Estado de rastreo: {trackedImage.trackingState}");
            Debug.Log($"ID de la imagen de referencia: {trackedImage.referenceImage.guid}");
            Debug.Log($"Tamaño físico: {trackedImage.referenceImage.size}");
            GameObject.Find("Manager").GetComponent<GameManager>().ImagenEncontrada(trackedImage.referenceImage.name, trackedImage.transform);
            GameObject.Find("Manager").GetComponent<UIManager>().actualizarImage(trackedImage.transform.position.ToString() + trackedImage.transform.eulerAngles.ToString());
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            Debug.Log($"IMAGEN ACTUALIZADA (Evento): Nombre='{trackedImage.referenceImage.name}', Estado={trackedImage.trackingState}, Posición={trackedImage.transform.position}");
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Debug.Log($"IMAGEN ELIMINADA (Evento): Nombre='{trackedImage.referenceImage.name}'");
        }
    }

    // // Esta función se llamará cada vez que el ARTrackedImageManager detecte cambios.
    // void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    // {
    //     // Iterar sobre las imágenes que se han AÑADIDO en este frame.
    //     // Estas son las imágenes detectadas por primera vez.
    //     foreach (ARTrackedImage trackedImage in eventArgs.added)
    //     {
    //         // Comprobamos si la imagen está siendo rastreada activamente.
    //         if (trackedImage.trackingState == TrackingState.Tracking)
    //         {
    //             Debug.Log($"IMAGEN AÑADIDA Y RASTREANDO: '{trackedImage.referenceImage.name}' en posición {trackedImage.transform.position}");
    //             // Podrías añadir más información, como la rotación, el ID, etc.
    //             // Debug.Log($"Nombre de la imagen: {trackedImage.referenceImage.name}");
    //             // Debug.Log($"Posición: {trackedImage.transform.position}");
    //             // Debug.Log($"Rotación: {trackedImage.transform.rotation}");
    //             // Debug.Log($"Estado de rastreo: {trackedImage.trackingState}");
    //             // Debug.Log($"ID de la imagen de referencia: {trackedImage.referenceImage.guid}");
    //             // Debug.Log($"Tamaño físico: {trackedImage.referenceImage.size}");
    //         }
    //         else if (trackedImage.trackingState == TrackingState.Limited)
    //         {
    //              Debug.Log($"IMAGEN AÑADIDA CON RASTREO LIMITADO: '{trackedImage.referenceImage.name}'");
    //         }
    //     }

    //     // Iterar sobre las imágenes que se han ACTUALIZADO en este frame.
    //     // Estas son imágenes que ya estaban detectadas y cuya pose o estado ha cambiado.
    //     foreach (ARTrackedImage trackedImage in eventArgs.updated)
    //     {
    //         // Comprobamos si la imagen está siendo rastreada activamente.
    //         if (trackedImage.trackingState == TrackingState.Tracking)
    //         {
    //             Debug.Log($"IMAGEN ACTUALIZADA Y RASTREANDO: '{trackedImage.referenceImage.name}' en posición {trackedImage.transform.position}");
    //         }
    //         // Si quieres un log incluso si el rastreo es limitado o se pierde temporalmente:
    //         // else if (trackedImage.trackingState == TrackingState.Limited)
    //         // {
    //         //     Debug.Log($"IMAGEN ACTUALIZADA CON RASTREO LIMITADO: '{trackedImage.referenceImage.name}'");
    //         // }
    //         // else // TrackingState.None (si se perdió el rastreo completamente en esta actualización)
    //         // {
    //         //     Debug.Log($"IMAGEN ACTUALIZADA PERO YA NO RASTREA: '{trackedImage.referenceImage.name}'");
    //         // }
    //     }

    //     // Opcional: Iterar sobre las imágenes que se han ELIMINADO en este frame.
    //     // Estas son imágenes que el sistema ha decidido dejar de rastrear.
    //     // foreach (ARTrackedImage trackedImage in eventArgs.removed)
    //     // {
    //     //     Debug.Log($"IMAGEN ELIMINADA: '{trackedImage.referenceImage.name}'");
    //     // }
    // }
}