using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;

public class NetworkConnect : MonoBehaviourPunCallbacks
{ 
    public GameObject checkNetworkUI;

    public GameObject ProgressBarUI;

    void Start()
    {
        ProgressBarUI.SetActive(true);
        StartCoroutine(ProgressBarChangerIenumerator(0.0f));
    }

    void Connect()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "5555"; // Please change this value to your own titleId from PlayFab Game Manager
        }


#if UNITY_ANDROID
        var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
        PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginMobileSuccess, OnLoginMobileFailure);
       
#endif
#if UNITY_IOS
            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginMobileSuccess, OnLoginMobileFailure);
#endif

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        StartCoroutine(ProgressBarChangerIenumerator(0.5f));
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        checkNetworkUI.SetActive(true);
    }

    public void RetryButton()
    {
        checkNetworkUI.SetActive(false);
        StartCoroutine(ProgressBarChangerIenumerator(0.0f));
    }

    private void OnLoginMobileSuccess(LoginResult result)
    {
        PlayerVariables.Inistance.setUserPlayFabID(result.PlayFabId);

        AuthenticationValues auth = new AuthenticationValues(result.PlayFabId);
        PhotonNetwork.AuthValues = auth;
        PhotonNetwork.NickName = result.PlayFabId;

        PhotonNetwork.ConnectUsingSettings();
        PlayfabDataGetter.Inistance.GetAllData(result.PlayFabId);
    }

    private void OnLoginMobileFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        SceneManager.LoadScene("LoadingScene");
    }

    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        //PhotonNetwork.NickName =""+deviceID;
        return deviceID;
        
    }

    public IEnumerator ProgressBarChangerIenumerator(float value)
    {
        ProgressBarUI.GetComponentInChildren<Slider>().value = value;
        value += 0.01f;
        yield return new WaitForEndOfFrame();
        
        if (value >= 0.5f && value < 0.51f)
            Connect();
        else if (value >= 1.0f)
            SceneManager.LoadScene("ManMenu");
        else
            StartCoroutine(ProgressBarChangerIenumerator(value));
    }

}
