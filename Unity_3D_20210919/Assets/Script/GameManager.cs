using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Turn turn = Turn.My;

    [Header("�Ǫ��}�C")]
    public GameObject[] goEnemys;
    [Header("�y�[��")]
    public GameObject goMarble;
    [Header("�ѽL�s��")]
    public Transform traCheckboard;
    [Header("�ͦ��ƶq�̤j��")]
    public Vector2Int v2randomEnemyCount = new Vector2Int(1, 7);
    [SerializeField]
    private Transform[] traCheckboards;
    [SerializeField]
    /// <summary>
    /// �ĤG�C�G�ͦ��Ǫ����ѽL
    /// </summary>
    private Transform[] traColumnSecond;
    /// <summary>
    /// �ѽL���ƶq
    /// </summary>
    private int countRow = 8;
    /// <summary>
    /// �ĤG�C�����ޭȡG�B�z�Ǫ��ͦ�������
    /// </summary>
    [SerializeField]
    private List<int> indexColumnsecond = new List<int>();

    private void Awake()
    {
        // �ѽL�}�C = �ѽL�s��.���o�l���󪺤���<�ܧΤ���>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();
        
        // ��l�ĤG�C�ƶq
        traColumnSecond = new Transform[countRow];
        // ���o�ĤG�C���ѽL
        for (int i = 9; i < 9 +countRow; i++)
        {
            traColumnSecond[i - countRow - 1] = traCheckboards[i];
        }

        SpawnEnemy();
    }

    /// <summary>
    /// �ͦ��ĤH�G�H���ƶq v2RandomEnemyCount
    /// </summary>
    private void SpawnEnemy()
    {
        int countEnemy = Random.Range(v2randomEnemyCount.x, v2randomEnemyCount.y);

        indexColumnsecond.Clear();

        for (int i = 0; i < 8; i++) indexColumnsecond.Add(i);

        for (int i = 0; i < countEnemy; i++)
        {
            int randomEnemy = Random.Range(0, goEnemys.Length);     // 0 ~ 2 - �H�� 0 �� 1

            int randomColumnSecond = Random.Range(0, indexColumnsecond.Count);

            Instantiate(goEnemys[randomEnemy], traColumnSecond[indexColumnsecond[randomColumnSecond]].position, Quaternion.Euler(0 ,180 ,0));

            indexColumnsecond.RemoveAt(randomColumnSecond);
        }

        int randomMarble = Random.Range(0, indexColumnsecond.Count);
        Instantiate(
            goMarble,
            traColumnSecond[indexColumnsecond[randomMarble]].position + Vector3.up,
            Quaternion.identity);
    }

    /// <summary>
    /// �����^�X
    /// </summary>
    /// <param name="isMyTurn">�O�_���a�^�X</param>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) turn = Turn.My;
        else turn = Turn.Enemy;
    }
}

/// <summary>
/// �^�X�G�ڤ�P�Ĥ�
/// </summary>
public enum Turn
{
    My, Enemy
}
