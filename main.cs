using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
// Add this:
using TrainpainHelper;

public class main : MonoBehaviour
{

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "C0DAE";
        }

        Login("tarektest208");
    }

    private void Login(string customId)
    {
        var request = new LoginWithCustomIDRequest { CustomId = "tarektest208", CreateAccount = false };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        List<string> keys = new List<string> { "a1_program", "algo_string" };
        GetUserData(keys);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void GetUserData(List<string> keys)
    {
        var request = new GetUserDataRequest { Keys = keys };
        PlayFabClientAPI.GetUserData(request, result => {
            Debug.Log("Got user data:");

            // It returns two values:
            // bool (validateResult.IsMissingKeys): true if there is any missing KVP, false otherwise
            // List<string> (validateResult.missingKeys): a list of all the missing KVPs
            Trainpain.ValidateReceivedDataResult validateResult = Trainpain.ValidateReceivedData(result, keys);
            
            if (validateResult.IsMissingKeys)
            {
                foreach (var missingkey in validateResult.missingKeys)
                {
                    Debug.Log(missingkey);
                }
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
