using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{

    #region 싱글톤
    private static BgManager _instance;
    public static BgManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BgManager();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    public float playerRange = 12f;
    public GameObject player;

    public GameObject cloudPrefabs;

    public GameObject enemyPrefabs;
    public List<GameObject> enemyList = new List<GameObject>();

    public int cloudNum;
    public int enemyNum;
    public int cloudNumMax = 3;
    public int enemyNumMax = 3;
    public float cloudSpawnTime = 1f;
    public float cloudTime;//현재의 시간
    public float enemySpawnTime = 4f;
    public float enemyTime;//현재의 시간


    public List<GameObject> enemyoff = new List<GameObject>();

    public List<Vector2> nodes = new List<Vector2>();
    public List<Vector2> enemynodes = new List<Vector2>();


    void Start()
    {
        //GameObject _enemy = Instantiate(enemyPrefabs) as GameObject;
        //EnemyList.Add(_enemy);

        enemyInit();

        cloudTime = 0;
        cloudNum = 0;
        enemyTime = 0;
        enemyNum = 0;
    }


    void Update()
    {
        cloudTime += Time.deltaTime;
        if (cloudTime > cloudSpawnTime)
        {

            if (cloudNum < cloudNumMax)
            {
                //Debug.Log("구름수" + cloudNum);
                GameObject c = Instantiate(cloudPrefabs);
                int n = Random.Range(0, nodes.Count);//(0번 노드,노드갯수)
                c.transform.position = new Vector2(nodes[n].x, nodes[n].y);
                cloudTime = 0;
                cloudNum++;
            }



        }

        OffEnemy();

        //enemyTime += Time.deltaTime;
        if (enemyNum < enemyNumMax)
        {
            //GameObject c = Instantiate(enemyPrefabs);
            //int n = Random.Range(0, enemynodes.Count);
            //c.transform.position = new Vector2(enemynodes[n].x, enemynodes[n].y);



            //EnemyList[0]의 몇번째 적인지를 알려줘야한다

            for (int i = 0; i < enemyList.Count; i++)
            {
                OnObj(enemyList[i], enemynodes);
            }
            enemyTime = 0;
            enemyNum++;
        }

    }

    void enemyInit()//정신만 생성
    {
        for (int i = 0; i < 3; i++)
        {

            GameObject _enemy = Instantiate(enemyPrefabs) as GameObject;
            enemyList.Add(_enemy);//enemylist에 추가

            _enemy.SetActive(false);
        }
    }

    //사망 처리 가져와야
    public void DieEnemy(GameObject obj)
    {
        obj.SetActive(false);//가리고
        enemyoff.Add(obj);//회수
        enemyList.Remove(obj);
        enemyNum--;//출고갯수처리
    }
    private void OffEnemy()
    {

        //만약에 적의x값이 플레이어레인지*2한것보다 작다면 DieEnemy(enemyList[i])처리 (업데이트)
        for (int i = 0; i < enemyList.Count; i++)//에너미 리스트
        {

            if (enemyList[i].transform.position.x < player.transform.position.x - playerRange)
            {
                Debug.Log("적list" + enemyList[i]);
                // enemyList.Remove(enemyList[i]);
                DieEnemy(enemyList[i]);

            }
        }

    }

    private void OnObj(GameObject obj, List<Vector2> listV)//listV모든랜덤리스트
    {
        //제품의 존재, 진열위치     
        obj.SetActive(true);//오브젝트를 켜주고
                            //int n = Random.Range(0, listV.Count);//(0번 노드,노드갯수)랜덤위치
                            //obj.transform.position = new Vector2(listV[n].x, listV[n].y);//그위치로 보내기
                            //카메라 안(플레이어)의 노드중 랜덤 생성

        float playerMin = player.transform.position.x;//플레이어위치
        float playerMax = player.transform.position.x + playerRange;//플레이어의 범위 = 플레이어 x값 +_특정값  = playerRange

        List<Vector2> _listV = new List<Vector2>();//_listV카메라속(playerMin,Max) 랜덤리스트
        for (int i = 0; i < listV.Count - 1; i++)
        {
            if (playerMin < listV[i].x && playerMax > listV[i].x)//플레이어보다 앞에있는(레인지)
            {
                _listV.Add(listV[i]);//_리스트에 추가
            }

            int n = Random.Range(0, _listV.Count - 1);//(0번 노드,인덱스 최대값)
            obj.transform.position = new Vector2(_listV[n].x, _listV[n].y); //에러
        }

    }

    //버벗이 죽으면 숫자는 카운트된다  새로생성되는 적이 보이지 않는다
    //버섯이 죽는데 숮ㅅ자 3이 유지된다
    //카메라 밖으로 나가면 적은 죽는다 버그발생
    //하이락키에서 에너미 오브젝트 삭제 인스펙터에서는 3존재
}
