using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardBehaviour : MonoBehaviour
{

    public GameObject LeaderBoardContentParent;
    public GameObject LeaderBoardContentPrefab;

    private bool leaderBoardConfigured = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayFabManager.instance.leaderBoardDataFilled && !leaderBoardConfigured)
        {
            ConfigureDataInLeaderBoard();
        }
    }

    public void ConfigureDataInLeaderBoard()
    {
        leaderBoardConfigured = true;
        for(int i=0; i < LeaderBoardContentParent.transform.childCount; i++)
        {
            Destroy(LeaderBoardContentParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < PlayFabManager.instance.AllPlayersForLeaderBoard.Count; i++)
        {
            int _index = i;
            GameObject _leaderboardObj = Instantiate(LeaderBoardContentPrefab, LeaderBoardContentParent.transform, false);
            _leaderboardObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " " + PlayFabManager.instance.AllPlayersForLeaderBoard[_index].displayName;
            _leaderboardObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = " " + PlayFabManager.instance.AllPlayersForLeaderBoard[_index].playerMoney.ToString();
        }
    }
}
