using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenspaceFollowerManager : MonoBehaviour
{
    public static List<WorldspaceObjectToFollow> _worldspaceObjectToFollows = new List<WorldspaceObjectToFollow>();
    [SerializeField]
    private ScreenObjectFollower _screenSpacePrefab;
    // Start is called before the first frame update
    void Start()
    {
       // _worldspaceObjectToFollows = FindAllWorldSpaceObjectsToFollow();

        for(int i = 0; i <  _worldspaceObjectToFollows.Count; i++)
        {
            ScreenObjectFollower newFollower = Instantiate(_screenSpacePrefab, transform);
            newFollower.Initialize(_worldspaceObjectToFollows[i]);
        }
    }



   
}
