using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatListDump{

    public class ChatListManager : MonoBehaviour
    {

        [SerializeField]
        private List<string> allMessages = new List<string>();

        public List<string> messages
        {

            get { return allMessages; }
            set { allMessages = value; }

        }

    }

}
