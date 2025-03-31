using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAvalon : MonoBehaviour
{
    [SerializeField]
    private Chat chatClient;

    public void OnClick_Start()
    {
        if (PhotonNetwork.isMasterClient && PhotonNetwork.playerList.Length >= 5)
        {
            MainCanvasManager.Instance.AvalonCanvas.transform.SetAsLastSibling();
            Chat chat = GameObject.Find("ChatClient").GetComponent<Chat>();
            chat.InputFieldChat = GameObject.Find("ChatInput").GetComponent<InputField>();
            chat.chatLayoutGroup = GameObject.Find("ChatLayoutGroup").GetComponent<ChatLayoutGroup>();
            StartGame();
        }
        else if (!PhotonNetwork.isMasterClient)
        {
            print("게임을 시작할 권한이 없습니다.");
            chatClient.SendChatMessage("시스템: 게임을 시작할 권한이 없어서 게임을 시작할 수 없습니다.");
        }
        else
        {
            MainCanvasManager.Instance.AvalonCanvas.transform.SetAsLastSibling();
            Chat chat = GameObject.Find("ChatClient").GetComponent<Chat>();
            chat.InputFieldChat = GameObject.Find("ChatInput").GetComponent<InputField>();
            chat.chatLayoutGroup = GameObject.Find("ChatLayoutGroup").GetComponent<ChatLayoutGroup>();
            StartGame();
            //print("플레이어 수가 부족합니다.");
            //chatClient.SendChatMessage("시스템: 플레이어 수가 부족해 게임을 시작할 수 없습니다.");
        }
    }

    private void StartGame()
    {
        print("게임을 시작합니다.");
        if (PhotonNetwork.isMasterClient)
        {
            print("역할을 배분합니다.");
            GiveRoles();
        }
    }

    private void GiveRoles()
    {
        PhotonPlayer[] players = PhotonNetwork.playerList;
        int N = players.Length;
        switch (N)
        {
            case 5: //5인: (선)멀린+신하2 (악)암살자+신하1
                int assassin = (int)Random.Range(0, 4.99f);
                chatClient.SendChatMessage("Avalon시스템>>>" + players[assassin].NickName + ": 당신은 암살자입니다.");

                int evil = (int)Random.Range(0, 4.99f);
                while ( evil == assassin) { evil = (int)Random.Range(0, 4.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[evil].NickName + ": 당신은 악의 세력입니다.");

                int merlin = (int)Random.Range(0, 4.99f);
                while (merlin == assassin || merlin == evil) { merlin = (int)Random.Range(0, 4.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 멀린입니다.");
                

                for (int i = 0; i<5; i++)
                {
                    if (i != merlin && i != assassin && i != evil)
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 당신은 아서 왕의 충실한 기사입니다.");
                    }
                    else
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 악의 세력은 " + players[assassin].NickName + "님과 " + players[evil].NickName + "님입니다.");
                    }
                }
                break;


            case 6: //6인: (선)멀린+신하3 (악)암살자+신하1
                assassin = (int)Random.Range(0, 5.99f);
                chatClient.SendChatMessage("Avalon시스템>>>" + players[assassin].NickName + ": 당신은 암살자입니다.");

                evil = (int)Random.Range(0, 4.99f);
                while (evil == assassin) { evil = (int)Random.Range(0, 5.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[evil].NickName + ": 당신은 악의 세력입니다.");

                merlin = (int)Random.Range(0, 4.99f);
                while (merlin == assassin || merlin == evil) { merlin = (int)Random.Range(0, 5.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 멀린입니다.");

                for (int i = 0; i < 6; i++)
                {
                    if (i != merlin && i != assassin && i != evil)
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 당신은 아서 왕의 충실한 기사입니다.");
                    }

                    else if (i == merlin)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[evil].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[evil].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }
                }
                break;


            case 7: //7인: (선)멀린+퍼시벌+신하2 (악)암살자+모르가나+오베론
                int oberon = (int)Random.Range(0, 6.99f);
                chatClient.SendChatMessage("Avalon시스템>>>" + players[oberon].NickName + ": 당신은 오베론입니다.");

                assassin = (int)Random.Range(0, 6.99f);
                while (assassin == oberon) { assassin = (int)Random.Range(0, 6.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[assassin].NickName + ": 당신은 암살자입니다.");

                int morgana = (int)Random.Range(0, 6.99f);
                while (morgana == oberon || morgana == assassin) { morgana = (int)Random.Range(0, 6.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[morgana].NickName + ": 당신은 모르가나입니다.");

                merlin = (int)Random.Range(0, 6.99f);
                while (merlin == oberon || merlin == assassin || merlin == morgana) { merlin = (int)Random.Range(0, 6.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 멀린입니다.");

                for (int i = 0; i < 7; i++)
                {
                    if (i != merlin && i != assassin && i != morgana && i != oberon)
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 당신은 아서 왕의 충실한 기사입니다.");
                    }

                    else if (i == merlin)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + ", " + players[oberon].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else if (i != oberon)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }
                }
                break;


            case 8: //8인: (선)멀린+퍼시벌+신하3 (악)암살자+모르가나+오베론
                oberon = (int)Random.Range(0, 7.99f);
                chatClient.SendChatMessage("Avalon시스템>>>" + players[oberon].NickName + ": 당신은 오베론입니다.");

                assassin = (int)Random.Range(0, 7.99f);
                while (assassin == oberon) { assassin = (int)Random.Range(0, 7.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[assassin].NickName + ": 당신은 암살자입니다.");

                morgana = (int)Random.Range(0, 7.99f);
                while (morgana == oberon || morgana == assassin) { morgana = (int)Random.Range(0, 7.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[morgana].NickName + ": 당신은 모르가나입니다.");

                merlin = (int)Random.Range(0, 7.99f);
                while (merlin == oberon || merlin == assassin || merlin == morgana) { merlin = (int)Random.Range(0, 7.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 멀린입니다.");

                for (int i = 0; i < 8; i++)
                {
                    if (i != merlin && i != assassin && i != morgana && i != oberon)
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 당신은 아서 왕의 충실한 기사입니다.");
                    }

                    else if (i == merlin)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + ", " + players[oberon].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else if (i != oberon)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }
                }
                break;

            case 9: //9인: (선)멀린+퍼시벌+신하4 (악)암살자+모르가나+모드레드
                int mordred = (int)Random.Range(0, 8.99f);
                chatClient.SendChatMessage("Avalon시스템>>>" + players[mordred].NickName + ": 당신은 모드레드입니다.");

                assassin = (int)Random.Range(0, 8.99f);
                while (assassin == mordred) { assassin = (int)Random.Range(0, 8.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[assassin].NickName + ": 당신은 암살자입니다.");

                morgana = (int)Random.Range(0, 8.99f);
                while (morgana == mordred || morgana == assassin) { morgana = (int)Random.Range(0, 8.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[morgana].NickName + ": 당신은 모르가나입니다.");

                merlin = (int)Random.Range(0, 8.99f);
                while (merlin == mordred || merlin == assassin || merlin == morgana) { merlin = (int)Random.Range(0, 8.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 멀린입니다.");

                int percival = (int)Random.Range(0, 8.99f);
                while (percival == mordred || percival == assassin || percival == morgana || percival == merlin) { percival = (int)Random.Range(0, 8.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 퍼시벌입니다.");

                for (int i = 0; i < 9; i++)
                {
                    if (i != merlin && i != assassin && i != morgana && i != mordred && i != percival)
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 당신은 아서 왕의 충실한 기사입니다.");
                    }

                    else if (i == merlin)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName +  "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else if (i == percival)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 멀린은 " + players[merlin].NickName + ", " + players[morgana].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + ", " + players[mordred].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }
                }
                break;

            case 10: //10인: (선)멀린+퍼시벌+신하4 (악)암살자+모르가나+모드레드+신하 
                mordred = (int)Random.Range(0, 9.99f);
                chatClient.SendChatMessage("Avalon시스템>>>" + players[mordred].NickName + ": 당신은 모드레드입니다.");

                assassin = (int)Random.Range(0, 9.99f);
                while (assassin == mordred) { assassin = (int)Random.Range(0, 9.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[assassin].NickName + ": 당신은 암살자입니다.");

                morgana = (int)Random.Range(0, 9.99f);
                while (morgana == mordred || morgana == assassin) { morgana = (int)Random.Range(0, 9.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[morgana].NickName + ": 당신은 모르가나입니다.");

                merlin = (int)Random.Range(0, 9.99f);
                while (merlin == mordred || merlin == assassin || merlin == morgana) { merlin = (int)Random.Range(0, 9.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 멀린입니다.");

                percival = (int)Random.Range(0, 9.99f);
                while (percival == mordred || percival == assassin || percival == morgana || percival == merlin) { percival = (int)Random.Range(0, 9.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[merlin].NickName + ": 당신은 퍼시벌입니다.");

                evil = (int)Random.Range(0, 9.99f);
                while (evil == mordred || evil == assassin || evil == morgana || evil == merlin || evil == percival) { percival = (int)Random.Range(0, 9.99f); }
                chatClient.SendChatMessage("Avalon시스템>>>" + players[evil].NickName + ": 당신은 악의 세력입니다.");

                for (int i = 0; i < 9; i++)
                {
                    if (i != merlin && i != assassin && i != morgana && i != mordred && i != percival && i != evil)
                    {
                        chatClient.SendChatMessage("Avalon시스템>>>" + players[i].NickName + ": 당신은 아서 왕의 충실한 기사입니다.");
                    }

                    else if (i == merlin)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + ", " + players[evil].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else if (i == percival)
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 멀린은 " + players[merlin].NickName + ", " + players[morgana].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }

                    else
                    {
                        string msg = "Avalon시스템>>>" + players[i].NickName;
                        msg += ": 악의 세력은 " + players[assassin].NickName + ", " + players[morgana].NickName + ", " + players[mordred].NickName + ", " + players[evil].NickName + "님입니다.";
                        chatClient.SendChatMessage(msg);
                    }
                }
                break;
            default:
                print("게임을 시작할 수 없습니다.");
                string message = "Avalon시스템>>>";
                for (int i = 0; i < N; i++) {
                    message += players[i].NickName + " ";
                }
                message += "님을 환영합니다!";
                print(message);
                chatClient.SendChatMessage(message);
                break;
        }
    }
}
