using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextLabelManager : MonoBehaviour
{

    // Empty gameobject to put labels inside of
    public GameObject labelHolder;

    public EnemyLabel labelPrefab;

    private Dictionary<Enemy, EnemyLabel> enemyLabels = new Dictionary<Enemy, EnemyLabel>();

    void c_EnemyAdded(object sender, EnemyAddedEventArgs e)
    {
        Enemy enemy = e.enemyAdded;
        // if enemy not already in dict
        if (!enemyLabels.ContainsKey(enemy))
        {
            var newLabel = Instantiate(labelPrefab, labelHolder.transform);
            enemyLabels.Add(enemy, newLabel);
            newLabel.SetEnemy(enemy);
        }
    }

    void c_EnemyRemoved(object sender, EnemyRemovedEventArgs e)
    {
        Enemy enemy = e.enemyRemoved;
        if (enemyLabels.ContainsKey(enemy))
        {
            try
            {
                Destroy(enemyLabels[enemy].gameObject); // destroy label attached to enemy
            }
            catch (MissingReferenceException exc)
            {
                Debug.Log(exc); // sometimes on editor exit/stop the label will be destroyed before this call, resulting in exception
            }
            enemyLabels.Remove(enemy);
        }
    }

    private int CompareLabelByPosition(EnemyLabel a, EnemyLabel b)
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

    private List<EnemyLabel> CreatedSortedLabelList()
    {
        List<EnemyLabel> labelList = new List<EnemyLabel>();
        foreach (KeyValuePair<Enemy, EnemyLabel> entry in enemyLabels)
        {
            if (entry.Value != null)
            {
                labelList.Add(entry.Value);
            }
        }

        labelList.Sort(CompareLabelByPosition);

        return labelList;
    }

    private void Awake()
    {
        Enemy.EnemyAdded += c_EnemyAdded;
        Enemy.EnemyRemoved += c_EnemyRemoved;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
           List<EnemyLabel> labelList = CreatedSortedLabelList();

            int numLabels = labelList.Count;
            for (int i = 0; i < numLabels - 1; i++)
            {
                EnemyLabel a = labelList[i];
                EnemyLabel b = labelList[i+1];

                RectTransform rt_a = a.GetComponent<RectTransform>();
                RectTransform rt_b = b.GetComponent<RectTransform>();

                // Rect r_a = rt_a.rect;
                // Rect r_b = rt_b.rect;
                Rect r_a = new Rect(rt_a.localPosition.x, rt_a.localPosition.y, rt_a.rect.width, rt_a.rect.height);
                Rect r_b = new Rect(rt_b.localPosition.x, rt_b.localPosition.y, rt_b.rect.width, rt_b.rect.height);

                // Debug.Log(r_a);
                // Debug.Log(r_b);

                // float newOffset = 0f;
                // while (r_a.Overlaps(r_b))
                // {
                //     Debug.Log($"{a.text.text} OVERLAPS {b.text.text}");
                //     newOffset += 1f;
                //     r_b.y += newOffset;
                //     // b.dynamicOffset += 0.1f;
                //     // b.UpdatePosition();
                // }
                // Debug.Log($"Final offset: {newOffset}");



                if (r_a.Overlaps(r_b))
                {
                    // for (int j = 0; j < 5; j++)
                    // {
                    Debug.Log($"{a.text.text} OVERLAPS {b.text.text}");
                    //     b.dynamicOffset += 0.3f;
                    //     b.UpdatePosition();
                    //     Canvas.ForceUpdateCanvases();
                    // }
                }    
            }
        }

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
