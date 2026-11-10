using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerMove : MonoBehaviour
{
    private CharacterController cc;
    public Transform headCamera;
    public float speed = 3.5f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if (headCamera == null && Camera.main != null)
        {
            headCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Keyboard.current == null || cc == null) return;

        // Si no se asignó manualmente la cámara, buscarla
        if (headCamera == null && Camera.main != null)
        {
            headCamera = Camera.main.transform;
        }

        float x = 0f;
        float z = 0f;

        // Lectura de teclas W, A, S, D o flechas
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z += 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z -= 1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1f;

        Vector3 move = Vector3.zero;

        if (headCamera != null)
        {
            // Extraer la dirección frontal y lateral de la cabeza proyectadas al suelo (plano horizontal)
            Vector3 camForward = headCamera.forward;
            Vector3 camRight = headCamera.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // Calcular el movimiento relativo a la mirada del usuario
            move = (camRight * x + camForward * z).normalized;
        }

        cc.SimpleMove(move * speed);
    }
}