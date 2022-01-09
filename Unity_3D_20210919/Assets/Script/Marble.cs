using UnityEngine;

/// <summary>
/// ¼u¯]¨t²Î
/// </summary>
public class Marble : MonoBehaviour
{
    private void Awake()
    {
        // ª«²z.©¿²¤¶î¼h¸I¼²(A ¶î¼h¡AB¶î¼h) ©¿²¤ A B ¶î¼h¸I¼²
        Physics.IgnoreLayerCollision(6, 6);
    }
}
