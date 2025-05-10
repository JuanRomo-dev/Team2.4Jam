using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data")]
public class NewsCollection : ScriptableObject
{ 
    public List<NewsData> trueNews;
    public List<NewsData> falseNews;
}
