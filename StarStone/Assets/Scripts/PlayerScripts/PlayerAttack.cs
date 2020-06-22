//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerAttack : MonoBehaviour
//{
//    public float attackSpeed;

//    public Transform KnifePosition; // 
//    public Transform KnifeEndPosition;

//    private BoxCollider m_knifeCollider;


//    // Start is called before the first frame update
//    void Start()
//    {
//        gameObject.SetActive(false);
//        m_knifeCollider = GetComponent<BoxCollider>();
//        m_knifeCollider.enabled = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        MeleeAttack();
//    }

//    private void MeleeAttack()
//    {
//        if (PlayerController.meleeActive == true && KnifePosition.position.x != KnifeEndPosition.position.x)
//        {
//            gameObject.SetActive(true);
//            m_knifeCollider.enabled = true;
//            KnifePosition.position = new Vector3(Mathf.MoveTowards(KnifePosition.position.x, KnifeEndPosition.position.x, Time.deltaTime), KnifePosition.position.y, KnifePosition.position.z);
//        }
//        else if(KnifePosition.position.x == KnifeEndPosition.position.x)
//        {
//            //KnifePosition.position = PlayerController.KnifeStartPosition.position;
//        }
//    }

//}
