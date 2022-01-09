using UnityEngine;
using System.Collections;           // 
using System.Collections.Generic;   // �ޥ� �t��.���X.�@�� (�]�t List)

/// <summary>
/// ����t��
/// ���V�ƹ���m
/// �o�g�u�]
/// �^�X����
/// </summary>
public class ControlSystem : MonoBehaviour
{
    #region ���
    [Header("�˷ǻ��U")]
    public GameObject goArrow;
    [Header("�o�g��m")]
    public Transform traSpawnPoint;
    [Header("�l�u��")]
    public GameObject goMarbles;
    [Header("�o�g�t��"), Range(0, 5000)]
    public float speedshoot = 1000;
    [Header("�g�u�n�I�����ϼh")]
    public LayerMask layerToHit;
    [Header("���շƹ���m")]
    public Transform traTestMousePosition;
    [Header("�Ҧ��u�]")]
    public List<GameObject> listMarbles = new List<GameObject>();
    [Header("�o�g���j"), Range(0, 5)]
    public float fireInterval = 0.01f;
    /// <summary>
    /// �Ҧ��y�ټƶq
    /// </summary>
    public static int allMarbles;
    #endregion

    #region �ƥ�
    private void Start()
    {
        for (int i = 0; i < 2000; i++) SpawnMarble();
    }

    private void Update()
    {
        MouseControl();
    }
    #endregion

    #region ��k

    /// <summary>
    /// �ͦ��u�]�s���M�椺
    /// </summary>
    private void SpawnMarble()
    {
        // �u�]�`�ƼW�[
        allMarbles++;
        // �Ҧ��u�]�M��.�K�[(�ͦ��u�])
        listMarbles.Add(Instantiate(goMarbles, new Vector3(0,10,20),Quaternion.identity));
    }

    /// <summary>
    /// �ƹ�����
    /// </summary>
    private void MouseControl()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            goArrow.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 v3Mouse = Input.mousePosition;

            // print("�ƹ��y�СG" + v3Mouse);

            // �g�u = �D�n��v��.�ù��y����g�u(�ƹ��y��)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            // �g�u�I����T
            RaycastHit hit;

            // �p�G �g�u���쪫��N�B�z
            // ���z �g�u�I��(�g�u�A�Z��)
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                print("�ƹ��g�u���쪫��G" + hit.collider.name);

                Vector3 hitPosition = hit.point;
                hitPosition.y = 0.5f;
                traTestMousePosition.position = hitPosition;

                transform.forward = traTestMousePosition.position - transform.position;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(FireMarble());
        }
    }

    /// <summary>
    /// �o�g�u�]
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireMarble()
    {
        for (int i = 0; i < listMarbles.Count; i++)
        {
            GameObject temp = listMarbles[i];
            temp.transform.position = traSpawnPoint.position;
            temp.transform.rotation = traSpawnPoint.rotation;
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.GetComponent<Rigidbody>().AddForce(traSpawnPoint.forward * speedshoot);    // �o�g�߿�
            yield return new WaitForSeconds(fireInterval);
        }
    }

    #endregion
}
