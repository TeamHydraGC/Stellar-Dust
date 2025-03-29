// By: Vin
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class MissionManager : MonoBehaviour
{
    public TextMeshProUGUI missionText; 

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "WW_BossArea") 
        {
            ShowMissionObjective("Vanquish the Sandbeast!");
            StartCoroutine(HideMissionTextAfterDelay(3f));
        }
    }

    public void ShowMissionObjective(string mission)
    {
        if (missionText != null)
        {
            missionText.text = mission; 
            missionText.gameObject.SetActive(true); 
            // Debug.Log($"Mission Objective: {mission}");
        }
    }

    public void CompleteMission()
    {
        ShowMissionObjective("Mission Complete!");
        StartCoroutine(HideMissionTextAfterDelay(3f)); 
        // Debug.Log("Mission completed!");
    }

    public IEnumerator HideMissionTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
         
        if (missionText != null)
        {
            missionText.gameObject.SetActive(false); 
            // Debug.Log("Mission text hidden.");
        }
    }

    // PUT THIS INSIDE SANDBEAST SCRIPT WHEN DONE!!!
    // public void CompleteSandbeastMission()
    // {
    //     ShowMissionObjective("You have vanquished the Sandbeast!");
    //     StartCoroutine(HideMissionTextAfterDelay(3f)); // Hide after 3 seconds
    // }
}