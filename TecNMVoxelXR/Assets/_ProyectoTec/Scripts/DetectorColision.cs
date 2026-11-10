using UnityEngine;

public class DetectorColision : MonoBehaviour
{
    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Se dispara en el momento exacto en que la cápsula choca contra cualquier Collider
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Ignoramos el suelo para no saturar la consola
        if (hit.gameObject.name.ToLower().Contains("plane")) return;

        Debug.LogWarning($"[IMPACTO DETECTADO] Chocando con: {hit.gameObject.name} | Punto: {hit.point}");
    }

    void Update()
    {
        if (cc == null) return;

        // Revisa si las paredes laterales de la cápsula están bloqueadas por un obstáculo
        if ((cc.collisionFlags & CollisionFlags.Sides) != 0)
        {
            Debug.Log("<color=red>[BLOQUEO FÍSICO]</color> El Character Controller está frenado contra un muro/columna.");
        }
    }
}