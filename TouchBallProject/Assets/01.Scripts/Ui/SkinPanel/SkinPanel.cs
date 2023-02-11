using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private GameObject parentGroup;

    private void Awake() 
    {
        // 기본스킨 제외 
        for (int i = 1; i < parentGroup.transform.childCount; i++)
        {
            if(!SecurityPlayerPrefs.HasKey($"{i}Skin"))
            {
                // 초기화 
                SecurityPlayerPrefs.SetBool($"{i}Skin", false);
            }
            else
            {
                // 초기화됐다면 구매했던 스킨들 체크 && 내가 현재 사용하고 있는 스킨 셋팅 
            }
        }
    }


}
