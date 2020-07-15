//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OpeningCutscene : MonoBehaviour
//{
//    public GameObject MainCamera;
//    public GameObject CutsceneCamera;
//    public GameObject Menu;

//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(TheSequence());
//    }
//    IEnumerator TheSequence()
//    {
//        yield return new WaitForSeconds(2);
//        MainCamera.SetActive(true);
//        HUD.SetActive(true);
//        CutsceneCamera.SetActive(false);
//    }
//}
