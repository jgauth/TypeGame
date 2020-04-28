using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLabelManager : MonoBehaviour
{

    // Empty gameobject to put labels inside of
    public GameObject labelHolder;

    public GameObject labelPrefab;

    private Dictionary<Enemy, EnemyLabel> enemyLabels = new Dictionary<Enemy, EnemyLabel>();
    

    //private List<GameObject> labelList;
    //private List<Enemy> enemyList;

    //void c_EnemySpawned(object sender, EnemySpawnedEventArgs e)
    //{
    //    Debug.Log("Received enemy spawn event");

    //    labelList = CreatedSortedLabelList(enemyList);
    //}

    //void c_WordCompleted(object sender, WordCompletedEventArgs e)
    //{
    //    labelList = CreatedSortedLabelList(enemyList);
    //}

    private int CompareLabelByPosition(GameObject a, GameObject b)
    {
        float ay = a.transform.position.y;
        float by = b.transform.position.y;

        if (ay < by)
        {
            return -1;
        }
        else if (ay > by)
        {
            return 1;
        }

        return 0;
    }

    private List<GameObject> CreatedSortedLabelList(List<Enemy> enemyList)
    {
        List<GameObject> labelList = new List<GameObject>();

        foreach (Enemy e in enemyList)
        {
            Transform label = e.transform.Find("Canvas/Image");
            if (label)
            {
                labelList.Add(label.gameObject);
            }
        }

        labelList.Sort(CompareLabelByPosition);

        return labelList;
    }

    void Awake()
    {
        //Spawner.EnemySpawned += c_EnemySpawned;
        //InputHandler.WordCompleted += c_WordCompleted;

        //enemyList = Spawner.enemyList;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    labelList = CreatedSortedLabelList(Spawner.enemyList);

        //    for (int i = 0; i < labelList.Count; i++)
        //    {
        //        Enemy e = labelList[i].transform.parent.parent.GetComponent<Enemy>();
        //        e.SetWord($"{i}");
        //    }
        //}

        //foreach (GameObject l in labelList)
        //{
        //    if (l)
        //    {
        //        Debug.Log($"label = x: {l.transform.localPosition.x}, y: {l.transform.localPosition.y}. z: {l.transform.localPosition.z}");
        //    }
        //}


        //int numLabels = labelList.Count;
        //for (int i = 0; i < numLabels - 1; i++)
        //{
        //    GameObject a = labelList[i];
        //    GameObject b = labelList[i + 1];

        //    RectTransform rta = (RectTransform) a.transform;
        //    RectTransform rtb = (RectTransform) b.transform;

        //    Rect recta = new Rect(rta.position.x, rta.position.y, rta.rect.width, rta.rect.height);
        //    Rect rectb = new Rect(rtb.position.x, rtb.position.y, rtb.rect.width, rtb.rect.height);
        //    //Rect recta = rta.rect;
        //    //Rect rectb = rtb.rect;

        //    if (recta.Overlaps(rectb))
        //    {
        //        //Enemy e = b.transform.parent.parent.GetComponent<Enemy>();
        //        b.transform.parent.parent.GetComponent<Enemy>().verticalOffset += 1f;
        //    }
        //}
    }
}
