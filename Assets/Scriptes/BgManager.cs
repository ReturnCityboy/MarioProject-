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

    [Header("CLOUD")]
    public GameObject cloudPrefabs;
    public int cloudNum;
    public int cloudNumMax = 3;
    public float cloudSpawnTime = 1f;
    public float cloudTime;//현재의 시간
    public List<Vector2> cloudnodes = new List<Vector2>();

    [Header("ENEMY")]
    public GameObject enemyPrefabs;
    public int enemyNum;
    public int enemyNumMax = 3;
    public float enemySpawnTime = 8f;
    public float enemyTime;//현재의 시간
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> enemyOff = new List<GameObject>();
    public List<Vector2> enemyOn = new List<Vector2>();
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
                int n = Random.Range(0, cloudnodes.Count);//(0번 노드,노드갯수)
                c.transform.position = new Vector2(cloudnodes[n].x, cloudnodes[n].y);
                cloudTime = 0;
                cloudNum++;
            }
        }

        OffEnemy();

        enemyTime += Time.deltaTime;
        if (enemyTime > enemySpawnTime)
        {
            if (enemyNum < enemyNumMax)
            {
                Debug.Log("적리스폰");


                for (int i = 0; i < enemyList.Count; i++)
                {
                    OnEnemy(enemyList[i]);
                    Debug.Log(enemyList[i]);
                }
                enemyTime = 0;
                enemyNum++;
            }

        }
    }

    void enemyInit()//정신만 생성
    {
        for (int i = 0; i < enemyNumMax; i++)
        {
            GameObject _enemy = Instantiate(enemyPrefabs) as GameObject;
            enemyList.Add(_enemy);
            _enemy.SetActive(false);
        }
    }

    //사망 처리 가져와야
    public void DieEnemy(GameObject obj)
    {
        obj.SetActive(false);//가리고
        //enemyOff.Add(obj);//회수
        //enemyList.Remove(obj);
        
        //Todo: 12/13  문제발견 적카운트가 무한 발생
        // 숙제 카운트 1 문제가 무엇인지 2 해결방법 주석
        enemyNum--;//출고갯수처리
    }
    private void OffEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].transform.position.x < player.transform.position.x - playerRange)
            {
                Debug.Log("적list" + enemyList[i]);
                // enemyList.Remove(enemyList[i]);
                DieEnemy(enemyList[i]);
            }

            //적이 새로 생성안되는 문제
            //에너미 갯수가 작아진건 디버그
            //OnObj 디버그는 나오는데 이미지가 켜지질않는다 켜질 노드를 찾지 못하는듯
            //enemyList가 0인데(여기까지 디버그) 재생성이 되지 않는다
            //만약 enemyList가 0 이라면
        }

    }

    private void OnEnemy(GameObject obj)
    {

        obj.SetActive(true);

        float playerMin = player.transform.position.x;
        float playerMax = player.transform.position.x + playerRange;

        List<Vector2> _listV = new List<Vector2>();


        for (int i = 0; i < enemynodes.Count - 1; i++)
        {
            if (enemynodes != null)
            {
                if (playerMin < enemynodes[i].x && playerMax > enemynodes[i].x)
                {
                    for (int e = 0; e < enemyList.Count; e++)
                    {
                        if (enemyList != null)
                        {
                            if (enemyList[e].transform.position.x != enemynodes[i].x)
                            {
                                _listV.Add(enemynodes[i]);
                            }
                        }
                    }
                }
            }
            if (_listV.Count > 0)
            {
                int _n = (_listV.Count) - 1;
                int n = Random.Range(0, _n);
                Vector2 randomVector = new Vector2(_listV[n].x, _listV[n].y);

                if (!enemyOn.Contains(randomVector))
                {
                    obj.transform.position = randomVector;
                    enemyOn.Add(randomVector);
                }
                else
                {
                    Debug.Log("랜덤 돌려서 빈곳에 에너미 위치를 바꾼다=반복적으로사용해서조건이맞을때까지반복하고위치가바뀐다면 멈춰라 반복을"); 
                
                    
                }
            }
            else
            {
                Debug.Log("랜덤 리스트 카운트가 0보다 작거나 같다.");
            }
        }
    }
}






