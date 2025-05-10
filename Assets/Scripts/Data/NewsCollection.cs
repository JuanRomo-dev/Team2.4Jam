using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataCollection", menuName = "ScriptableObjects/DataCollection")]
public class NewsCollection : ScriptableObject
{ 
    public List<NewsData> trueNews;
    public List<NewsData> falseNews;
}
