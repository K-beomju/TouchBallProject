using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LeaderBoard : MonoBehaviour
{
    private void Awake()
    {
        Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log(Social.localUser.id);
                }
            });//시작할때 Authenticate를 해줘야 리더보드에 접근 할 수 있다.
    }

    public void ShowLeaderboardUI_Ranking()
    => ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_ranking);
    //내 리더보드 목록중 랭킹이라는 이름의 리더보드를 바로 보여준다

    public void ShowLeaderboardUI() => Social.ShowLeaderboardUI();
    //내 리더보드 목록을 보여주고 그 중 선택할 수 있다.

    public void AddLeaderboard(int highScore)//점수를 기록하는 함수
    => Social.ReportScore(highScore, GPGSIds.leaderboard_ranking, (bool success) => { });
}
//GPGSIds 스크립트는 static이어서 따로 참조할 필요가없다