using UnityEngine;

/// <summary>
/// �u�]�t��
/// </summary>
public class Marble : MonoBehaviour
{
    private void Awake()
    {
        // ���z.������h�I��(A ��h�AB��h) ���� A B ��h�I��
        Physics.IgnoreLayerCollision(6, 6);
    }
}
