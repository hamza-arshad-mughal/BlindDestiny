using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayFabManager : MonoBehaviour
{

    public static PlayFabManager instance { get; private set; }


    public bool leaderBoardDataFilled = false;
    public int playerMoney;
    public List<int> indexesOFunLockedCharacters;

    public List<PlayerLeaderBoardData> AllPlayersForLeaderBoard = new List<PlayerLeaderBoardData>();


    public bool hasDataBeenSet = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        playerMoney = GAMEMANAGER.instance.PlayerMoney;

        List<Character> _unlockedCharacters = CharacterSelectionManager.instance.AllCharcters.Where(c => c.isUnlocked).ToList();
        for(int i = 0; i < _unlockedCharacters.Count; i++)
        {
            indexesOFunLockedCharacters.Add(_unlockedCharacters[i].index);
        }

        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "PlayerMoney", Value = playerMoney },
            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });


        for (int i = 0; i < indexesOFunLockedCharacters.Count; i++)
        {
            string _statName = "UnlockedChar-" + indexesOFunLockedCharacters[i].ToString();
            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
                Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = _statName, Value = indexesOFunLockedCharacters[i] },
            }
            },
            result => { Debug.Log("User statistics updated"); },
            error => { Debug.LogError(error.GenerateErrorReport()); });
        }
    }

    public void GetDataAndAssignValues()
    {
        GetStatistics();
    }

    void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        foreach (var eachStat in result.Statistics)
        {
            if(eachStat.StatisticName == "PlayerMoney")
            {
                GAMEMANAGER.instance.PlayerMoney = eachStat.Value;
            }
            else
            {
                Character _character = CharacterSelectionManager.instance.AllCharcters[eachStat.Value];
                _character.isUnlocked = true;
                CharacterSelectionManager.instance.AllCharcters[eachStat.Value] = _character;
            }
        }
        hasDataBeenSet = true;
    }

    public void GetLeaderBoardDataFromPlayFab()
    {
        leaderBoardDataFilled = false;
        var request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerMoney", MaxResultsCount = 10 };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardSuccess, OnLeaderBoardFailure);
    }

    public void OnLeaderBoardFailure(PlayFabError obj)
    {
        Debug.Log("LEADERBOARD DATA FAILED");
    }

    public void OnLeaderBoardSuccess(GetLeaderboardResult obj)
    {
        foreach(PlayerLeaderboardEntry player in obj.Leaderboard)
        {
            PlayerLeaderBoardData _player;
            _player.displayName = player.DisplayName;
            _player.playerMoney = player.StatValue;
            AllPlayersForLeaderBoard.Add(_player);
        }
        leaderBoardDataFilled = true;
    }
}
