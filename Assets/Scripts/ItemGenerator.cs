using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefab������
    public GameObject carPrefab;
    //coinPrefab������
    public GameObject coinPrefab;
    //conePrefab������
    public GameObject conePrefab;
    //���j�e�B�����
    [SerializeField] private GameObject player;
    //�O��̃I�u�W�F�N�g�������W
    int preSpawnPos;
    //�I�u�W�F�N�g�������W
    int spawnPos = 80;
    //�S�[���n�_
    private int goalPos = 360;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;

    private void Update()
    {
        //�u�O��̐������W�̕��������ꍇ�v�܂��́u�������W���S�[�����߂����ꍇ�v
        if (preSpawnPos > spawnPos || spawnPos >= goalPos)
            return;

        //�v���C���[���W�Ɛ������W�̋�����40�𒴂����ꍇ
        if ((player.transform.position - new Vector3(0, 0, spawnPos)).sqrMagnitude > 1600)
            return;
        
        preSpawnPos = spawnPos;
        spawnPos += 15;

        //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            //�R�[����x�������Ɉ꒼���ɐ���
            for (float x = -1; x <= 1; x += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * x, cone.transform.position.y, spawnPos);
            }
        }
        else
        {
            //���[�����ƂɃA�C�e���𐶐�
            for (int x = -1; x <= 1; x++)
            {
                //�A�C�e���̎�ނ����߂�
                int item = Random.Range(1, 11);
                //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                int offsetZ = Random.Range(-5, 6);
                //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                if (1 <= item && item <= 6)
                {
                    //�R�C���𐶐�
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * x, coin.transform.position.y, spawnPos + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //�Ԃ𐶐�
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * x, car.transform.position.y, spawnPos + offsetZ);
                }
            }
        }
    }
}
