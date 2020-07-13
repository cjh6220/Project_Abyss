using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace verifyData
{
    class VerifyData
    {
        string currentDir;
        string dataDir;

        List<string> dataFiles = new List<string>();
        List<string> stringFiles = new List<string>();

        // DataSheet Array
        private static DataSheet[] arrDataSheet;

        Dictionary<string, int> dataSheetIndex = new Dictionary<string,int>();


        private static Dictionary<string, string> textTable = new Dictionary<string, string>();
        private static int lineCount = 0;

        public VerifyData(string dataDir)
        {
            Console.WriteLine("dataDir:" + dataDir);

            currentDir = System.Environment.CurrentDirectory;
            this.dataDir = dataDir;
        }

        public void Verify()
        {
            string[] filePaths = Directory.GetFiles(dataDir, "*.bytes");
            foreach (string path in filePaths)
            {
                if (!path.Contains("language") && !path.Contains("charSet"))
                {
                    dataFiles.Add(path);
                }
                else
                {
                    stringFiles.Add(path);
                }
            }

            arrDataSheet = new DataSheet[dataFiles.Count];

            LoadAllDataTable();
            LoadLanguageFile(dataDir+"/language-en.bytes");

            VerifyHerb();
            VerifyAnimalEXP();
            VerifyBuildingUpgrade();
            VerifyCharacter();
            VerifyCraft();
            VerifyDisease();
            VerifyMedicine();
            VerifyObject();
            VerifyPatient();
            VerifyRecoveryRoom();
            VerifySquare();
            VerifyPreDisease();
            VerifyFDialogue();
            VerifyPDialogue();
            VerifyItem();

            // Add Hans
            VerifyAchieve();
            VerifyQuest();
            VerifyCutScene();
            VerifyMission();
            VerifyToDo();

            Console.WriteLine();
            Console.WriteLine("Finished verifying data!!!!!");
        }

        bool VerifyHerb()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Herb...");

            int index = dataSheetIndex["herb_ref"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in Herb Table.");
                }

                if (!FindValue(dataSheetIndex["ItemTable"], "id", data.Key))
                {
                    Console.WriteLine("ERROR: cannot find herb's ID in Item Table.["+data.Key+"]");
                }
            }

            return true;
        }

        bool VerifyAnimalEXP()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Animal EXP...");

            int index = dataSheetIndex["animal_xp"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in Animal EXP.");
                }

                if (!FindValue(dataSheetIndex["CharTable"], "id", data.Key))
                {
                    Console.WriteLine("ERROR: cannot find animal's ID in Animal Table.[" + data.Key + "]");
                }
            }

            return true;
        }

        bool VerifyBuildingUpgrade()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Building Upgrade...");

            int index = dataSheetIndex["objectTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in Object table.");
                }

                if (data.Value["upgrade"].CompareTo("Y") == 0)
                {
                    if (!FindValue(dataSheetIndex["buildingUpgrade"], "id", data.Key))
                    {
                        Console.WriteLine("ERROR: cannot find Building's ID in Building-Upgrade Table.[" + data.Key + "]");
                    }
                }
            }

            index = dataSheetIndex["buildingUpgrade"];
            table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in building upgrade.");
                }

                int spriteCount = System.Int32.Parse(data.Value["max_upgrade"]);

                string[] temp;
                if (data.Value["use_SM"].CompareTo("N") == 0)
                {
                    temp = data.Value["resID"].Split(',');
                    if (spriteCount != temp.Length)
                    {
                        Console.WriteLine("ERROR: sprite count is not matched with the upgrade-level in Building-Upgrade Table.[" + data.Key + "]");
                    }
                }

                temp = data.Value["rewardType"].Split(',');
                if (spriteCount != temp.Length)
                {
                    Console.WriteLine("ERROR: sprite count is not matched with the reward-type in Building-Upgrade Table.[" + data.Key + "]");
                }

                string[] items = data.Value["rewardItem"].Split(',');
                int i = 0;
                foreach (string type in temp)
                {
                    if (type.CompareTo("5") == 0 || type.CompareTo("4") == 0 || type.CompareTo("6") == 0)
                    {
                        if (i >= items.Length)
                        {
                            Console.WriteLine("ERROR: count of 'rewardItem' is not matched with the reward-type in Building-Upgrade Table.[" + data.Key + "]");
                            continue;
                        }

                        if (type.CompareTo("5") == 0)
                        {
                            if (!FindValue(dataSheetIndex["ItemTable"], "id", items[i]))
                            {
                                Console.WriteLine("ERROR: cannot find item's ID in Item Table.[" + data.Key + "][" + items[i] + "]");
                            }
                        }
                        else if (type.CompareTo("4") == 0)
                        {
                            if (!FindValue(dataSheetIndex["objectTable"], "id", items[i]))
                            {
                                Console.WriteLine("ERROR: cannot find objcet's ID in Object Table.[" + data.Key + "][" + items[i] + "]");
                            }
                        }
                        else if (type.CompareTo("6") == 0)
                        {
                            if (!FindValue(dataSheetIndex["CharTable"], "id", items[i]))
                            {
                                Console.WriteLine("ERROR: cannot find character's ID in Char Table.[" + data.Key + "][" + items[i] + "]");
                            }
                        }
                    }

                    i++;
                }


                temp = data.Value["rewardAmount"].Split(',');
                if (spriteCount != temp.Length)
                {
                    Console.WriteLine("ERROR: sprite count is not matched with the reward-amount in Building-Upgrade Table.[" + data.Key + "]");
                }

                temp = data.Value["rewardCycle"].Split(',');
                if (spriteCount != temp.Length)
                {
                    Console.WriteLine("ERROR: sprite count is not matched with the reward-cycle in Building-Upgrade Table.[" + data.Key + "]");
                }
            }

            return true;
        }

        bool VerifyCharacter()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Character...");

            int index = dataSheetIndex["CharTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in Char table.");
                }

                if (!FindText(data.Value["txt_id"]))
                {
                    Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + data.Value["txt_id"] + "]");
                }
            }

            return true;
        }

        bool VerifyCraft()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Craft...");

            string[] materials;
            string[] amount;

            int index = dataSheetIndex["craft_ref"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in craft table.");
                }

                if (!FindValue(dataSheetIndex["ItemTable"], "id", data.Key))
                {
                    Console.WriteLine("ERROR: cannot find ID in Item Table.[" + data.Key + "]");
                }

                materials = data.Value["material"].Split(',');
                amount = data.Value["amount"].Split(',');

                if (materials.Length != amount.Length)
                {
                    Console.WriteLine("ERROR: material count is not matched with the amount in Craft Table.[" + data.Key + "]");
                }

                foreach (string mat in materials)
                {
                    if (!FindValue(dataSheetIndex["ItemTable"], "id", mat))
                    {
                        Console.WriteLine("ERROR: cannot find material's ID in Item Table.[" + data.Key + "][" + mat + "]");
                    }
                }
            }

            return true;
        }

        bool VerifyDisease()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Disease...");

            string[] medicines;

            int index = dataSheetIndex["Disease_List"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in Disease list.");
                }

                medicines = data.Value["medi_list"].Split(',');

                foreach (string m in medicines)
                {
                    if (!FindValue(dataSheetIndex["medicine_ref"], "id", m))
                    {
                        Console.WriteLine("ERROR: cannot find medicine's ID in Item Table.[" + data.Key + "][" + m + "]");
                    }
                }

                medicines = data.Value["salve_list"].Split(',');

                foreach (string m in medicines)
                {
                    if (!FindValue(dataSheetIndex["medicine_ref"], "id", m))
                    {
                        Console.WriteLine("ERROR: cannot find salve's ID in Item Table.[" + data.Key + "][" + m + "]");
                    }
                }

                medicines = data.Value["needle_list"].Split(',');

                foreach (string m in medicines)
                {
                    if (!FindValue(dataSheetIndex["medicine_ref"], "id", m))
                    {
                        Console.WriteLine("ERROR: cannot find needle's ID in Item Table.[" + data.Key + "][" + m + "]");
                    }
                }

                medicines = data.Value["moxa_list"].Split(',');

                foreach (string m in medicines)
                {
                    if (!FindValue(dataSheetIndex["medicine_ref"], "id", m))
                    {
                        Console.WriteLine("ERROR: cannot find moxa's ID in Item Table.[" + data.Key + "][" + m + "]");
                    }
                }

                if (!FindText(data.Value["txt_id"]))
                {
                    Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + data.Value["txt_id"] + "]");
                }
            }

            return true;
        }

        bool VerifyMedicine()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Medicine...");

            int index = dataSheetIndex["medicine_ref"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in medicine ref.");
                }

                if (!FindValue(dataSheetIndex["ItemTable"], "id", data.Key))
                {
                    Console.WriteLine("ERROR: cannot find medicine's ID in Item Table.[" + data.Key + "]");
                }
            }

            return true;
        }

        bool VerifyObject()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Object...");

            int index = dataSheetIndex["objectTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in object table.");
                }

                if (data.Value["rewardType"].CompareTo("4") == 0)
                {
                    if (!FindValue(dataSheetIndex["objectTable"], "id", data.Value["rewardItem"]))
                    {
                        Console.WriteLine("ERROR: cannot find object's ID in Object Table.[" + data.Key + "][" + data.Value["rewardItem"] + "]");
                    }
                }
                else if (data.Value["rewardType"].CompareTo("5") == 0)
                {
                    if (!FindValue(dataSheetIndex["ItemTable"], "id", data.Value["rewardItem"]))
                    {
                        Console.WriteLine("ERROR: cannot find item's ID in Item Table.[" + data.Key + "][" + data.Value["rewardItem"] + "]");
                    }
                }
                else if (data.Value["rewardType"].CompareTo("6") == 0)
                {
                    if (!FindValue(dataSheetIndex["CharTable"], "id", data.Value["rewardItem"]))
                    {
                        Console.WriteLine("ERROR: cannot find character's ID in Char Table.[" + data.Key + "][" + data.Value["rewardItem"] + "]");
                    }
                }

                if (!FindText(data.Value["txt_id"]))
                {
                    Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + data.Value["txt_id"] + "]");
                }

                if (data.Value["useSimpleAni"].CompareTo("Y") == 0)
                {
                    if (!FindValue(dataSheetIndex["SimpleAni"], "id", data.Value["id"]))
                    {
                        Console.WriteLine("ERROR: cannot find text ID in SimpleAni Table.[" + data.Key + "]");
                    }
                }
            }

            return true;
        }

        bool VerifyPatient()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Patient...");

            int index = dataSheetIndex["PatientCharTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in patient char table.");
                }

                if (!FindValue(dataSheetIndex["CharTable"], "id", data.Key))
                {
                    Console.WriteLine("ERROR: cannot find character's ID in Char Table.[" + data.Key + "]");
                }

                if (!FindText(data.Value["txt_id"]))
                {
                    Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + data.Value["txt_id"] + "]");
                }
            }

            return true;
        }

        bool VerifyRecoveryRoom()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Recovery Room...");

            int index = dataSheetIndex["objectTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in object table.");
                }

                if (data.Key.Contains("ID_RecoveryRoom"))
                {
                    if (!FindValue(dataSheetIndex["RecoveryRoom_ref"], "id", data.Key))
                    {
                        Console.WriteLine("ERROR: cannot find recovery room's ID in Recovery Room Table.[" + data.Key + "]");
                    }
                }
            }

            return true;
        }

        bool VerifySquare()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Unlock Square...");

            string[] category;
            string[] items;

            int index = dataSheetIndex["squareTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in square table.");
                }

                if (!FindValue(dataSheetIndex["ItemTable"], "id", data.Value["item_1"]))
                {
                    Console.WriteLine("ERROR: cannot find item in Item Table'.[" + data.Key + "]["+data.Value["item_1"]+"]");
                }

                if (!FindValue(dataSheetIndex["ItemTable"], "id", data.Value["item_2"]))
                {
                    Console.WriteLine("ERROR: cannot find item in Item Table'.[" + data.Key + "][" + data.Value["item_2"] + "]");
                }

                category = data.Value["unlock_category"].Split(',');
                items = data.Value["unlock_item"].Split(',');

                if (category.Length != items.Length)
                {
                    Console.WriteLine("ERROR: count of 'unlock_item' should be matched with the count of 'unlock_category'.[" + data.Key + "]");
                }

                int i = 0;
                foreach (string c in category)
                {
                    if (i >= items.Length)
                    {
                        Console.WriteLine("ERROR: count of 'unlock_item' should be matched with the count of 'unlock_category'.[" + data.Key + "]");
                        continue;
                    }

                    if (c.CompareTo("0") == 0)
                    {
                        if (!FindValue(dataSheetIndex["ItemTable"], "id", items[i]))
                        {
                            Console.WriteLine("ERROR: cannot find item's ID in Item Table.[" + data.Key + "][" + items[i] + "]");
                        }
                    }
                    else if (c.CompareTo("1") == 0 || c.CompareTo("9") == 0)
                    {
                        if (!FindValue(dataSheetIndex["objectTable"], "id", items[i]))
                        {
                            Console.WriteLine("ERROR: cannot find object's ID in Object Table.[" + data.Key + "][" + items[i] + "]");
                        }
                    }

                    i++;
                }
            }

            return true;
        }

        bool VerifyPreDisease()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying PreDisease...");

            int index = dataSheetIndex["Pre_Disease"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in pre disease.");
                }

                if (!FindText(data.Value["txt_id"]))
                {
                    Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + data.Value["txt_id"] + "]");
                }
            }

            return true;
        }

        bool VerifyFDialogue()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying F_Dialogue...");

            string[] text;

            int index = dataSheetIndex["F_Dialogue_List"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in f dialogue list.");
                }

                text = data.Value["text"].Split(',');

                foreach(string t in text)
                {
                    if (!FindText(t))
                    {
                        Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + t + "]");
                    }
                }
            }

            return true;
        }

        bool VerifyPDialogue()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying P_Dialogue...");

            string[] text;

            int index = dataSheetIndex["P_Dialogue_List"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in p dialogue list.");
                }

                text = data.Value["text"].Split(',');

                foreach (string t in text)
                {
                    if (!FindText(t))
                    {
                        Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + t + "]");
                    }
                }
            }

            return true;
        }

        bool VerifyItem()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Item...");

            string targetPath = dataDir + "/../Prefabs/gui/icons";

            int index = dataSheetIndex["ItemTable"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in item table.");
                }

                if (!FindFile(targetPath, data.Value["res_id"] + ".prefab"))
                {
                    Console.WriteLine("ERROR: cannot find Item's resource.[" + data.Value["res_id"] + "]");
                }
            }

            return true;
        }

        //----------------------------------------------------------------------
        // Add Hans
        //----------------------------------------------------------------------
        bool VerifyAchieve()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Achievement...");

            int index = dataSheetIndex["achievement_list"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in achievement list.");
                }

                if (!FindText(data.Value["text_title_id"]) || !FindText(data.Value["achievement_text"]))
                {
                    Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + "][" + data.Value["txt_id"] + "]");
                }
            }

            return true;
        }

        bool VerifyCutScene()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying CutScene...");
            
            int sceneVolume;
            string[] sprImageID;
            string[] sprLocation;
            string[] sprEffIn;
            string[] sprEffOut;
            string[] sceneText;

            int index = dataSheetIndex["cutscene_list"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in cutscene list.");
                }

                sceneVolume = System.Int32.Parse(data.Value["scene_volume"]);

                sprImageID = data.Value["sceneimage"].Split(',');
                sprLocation = data.Value["scenelocation"].Split(',');
                sprEffIn = data.Value["sceneeffect_in"].Split(',');
                sprEffOut = data.Value["sceneeffect_out"].Split(',');
                sceneText = data.Value["scenetext"].Split(',');

                // for Debuging..........
                if (sprImageID.Length != sceneVolume)
                    Console.WriteLine("Error Key:" + data.Key + " sprImageID len:" + sprImageID.Length + " sceneVolume:" + sceneVolume);
                if (sprLocation.Length != sceneVolume)
                    Console.WriteLine("Error Key:" + data.Key + " sprLocationlen:" + sprLocation.Length + " sceneVolume:" + sceneVolume);
                if (sprEffIn.Length != sceneVolume)
                    Console.WriteLine("Error Key:" + data.Key + " sprEffInlen:" + sprEffIn.Length + " sceneVolume:" + sceneVolume);
                if (sprEffOut.Length != sceneVolume)
                    Console.WriteLine("Error Key:" + data.Key + " sprEffOutlen:" + sprEffOut.Length + " sceneVolume:" + sceneVolume);
                if (sceneText.Length != sceneVolume)
                    Console.WriteLine("Error Key:" + data.Key + " sceneTextlen:" + sceneText.Length + " sceneVolume:" + sceneVolume);			
            }

            return true;
        }

        bool VerifyQuest()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Quest...");

            int missionCount;
            string[] missionIds;

            int index = dataSheetIndex["quest_list"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in quest list.");
                }

                missionCount = System.Int32.Parse(data.Value["mission_count"]);
                missionIds = data.Value["mission_id"].Split(',');

               //Console.WriteLine("==> missionCount:" + missionCount + " length:" + missionIds.Length);

                if (missionCount != missionIds.Length)
                {
                    Console.WriteLine("ERROR: 퀘스트리스트에 입력된 미션카운트와 나열된 미션아이디의 갯수가 틀림...위치:[" + data.Key + "]");
                }

                foreach (string mId in missionIds)
                {
                    if (!FindValue(dataSheetIndex["mission_list"], "id", mId))
                    {
                        Console.WriteLine("ERROR: cannot find Mission's ID in Mission List. QuestID:" + data.Key + " MissionID:" + mId);
                    }
                }
            }

            return true;
        }

        bool VerifyMission()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying Mission...");

            int toDoCount;           
            string[] toDoTxt;
            string[] toDoIds;
            string[] toDoVal;

            int rewardCount;
            string[] toDoReward;

            int index = dataSheetIndex["mission_list"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in mission list.");
                }

                toDoCount = System.Int32.Parse(data.Value["todo_count"]);                
                toDoTxt = data.Value["todo_text"].Split(',');
                toDoIds = data.Value["todo_id"].Split(',');
                toDoVal = data.Value["todo_value"].Split(',');

                rewardCount = System.Int32.Parse(data.Value["reward_count"]);
                toDoReward = data.Value["reward_icon"].Split(',');

                // check count
                if (toDoCount != toDoTxt.Length)
                {
                    Console.WriteLine("ERROR: todo_text is not matched toDo count in MissinList.[" + data.Key + "]");
                }
                if (toDoCount != toDoIds.Length)
                {
                    Console.WriteLine("ERROR: todo_id is not matched toDo count in MissinList.[" + data.Key + "]");
                }
                if (toDoCount != toDoVal.Length)
                {
                    Console.WriteLine("ERROR: todo_value is not matched toDo count in MissinList.[" + data.Key + "]");
                }
                
                // check todo id
                foreach (string tId in toDoIds)
                {
                    if (!FindValue(dataSheetIndex["todo_list"], "id", tId))
                    {
                        Console.WriteLine("ERROR: cannot find ToDo's ID in ToDo List. MissionID:" + data.Key + "- ToDoID:" + tId);
                    }
                }
                // check reward
                if (rewardCount != toDoReward.Length)
                {
                    Console.WriteLine("ERROR: rewardicon is not matched rewardCount in MissinList.[" + data.Key + "]");
                }
            }

            return true;
        }
        bool VerifyToDo()
        {
            Console.WriteLine();
            Console.WriteLine("Verifying ToDoList...");

            string genKey = "";
            Dictionary<string, string> dicStatics = new Dictionary<string, string>();
            dicStatics.Clear();

            string[] toDoTxt;

            int index = dataSheetIndex["todo_list"];
            Dictionary<string, Dictionary<string, string>> table = GetTable(index);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (string.IsNullOrEmpty(data.Key) || string.IsNullOrWhiteSpace(data.Key))
                {
                    Console.WriteLine("ERROR: there is empty cell in todo list.");
                }

                toDoTxt = data.Value["todo_txt"].Split(',');

                genKey = KeyGenerator(data.Value["todo_action"], data.Value["todo_parameter"], data.Value["todo_object"]);

//                Console.WriteLine( "id: " + data.Value["id"] + " key: " + genKey);

                if(dicStatics.ContainsKey(genKey))
                {
                    Console.WriteLine("ERROR: already has same atctios key in ToDoList....toDoID:" + data.Key + " genKey:" + genKey); 
                }
                else
                {
                    dicStatics.Add(genKey, "Verify");
                }

                // check todo text
                foreach (string txt in toDoTxt)
                {
                    if (!FindText(txt))
                    {
                        Console.WriteLine("ERROR: cannot find text ID in String Table.[" + data.Key + " toDoTxt:" + txt);
                    }
                }
            }

            return true;
        }

        string KeyGenerator(string action, string param, string target)
        {
            return action + "$" + param + "$" + target;           
        }
        //----------------------------------------------------------------------

        bool FindFile(string dir, string name)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
            foreach(System.IO.FileInfo f in di.GetFiles())
            {
                if(f.Extension != "meta" && f.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
        

        public static bool FindValue(int tableIndex, string targetHeader, string value)
        {
            Dictionary<string, Dictionary<string, string>> table = GetTable(tableIndex);
            foreach (KeyValuePair<string, Dictionary<string, string>> data in table)
            {
                if (data.Value[targetHeader].CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;
        }

#region Sheet Data Return
        public static DataSheet GetSheet(int tableIdx)
        {
            return arrDataSheet[tableIdx];
        }

        public static Dictionary<string, Dictionary<string, string>> GetTable(int tableIdx)
        {
            return GetSheet(tableIdx).dataTable;
        }

        public static Dictionary<string, string> GetData(int tableIdx, string id)
        {
            if (!arrDataSheet[tableIdx].dataTable.ContainsKey(id))
            {
                Console.WriteLine("cannot find data - tableIndex:[" + tableIdx + "], id:[" + id + "]!!!!!!!!!!!!!!!");
                return null;
            }

            return GetTable(tableIdx)[id];
        }

        /// <summary>
        /// Get a specific value in the table.
        /// </summary>
        public static string GetValue(int tableIndex, string id, string header)
        {
            if (!arrDataSheet[tableIndex].dataTable.ContainsKey(id))
            {
                Console.WriteLine("cannot find data - tableIndex:[" + tableIndex + "], id:[" + id + "]!!!!!!!!!!!!!!!");
                return null;
            }

            Dictionary<string, string> data = arrDataSheet[tableIndex].dataTable[id];
            if (data != null)
            {
                if (data.ContainsKey(header))
                {
                    string output = "";
                    data.TryGetValue(header, out output);
                    return output;
                }
            }

            return null;
        }

        //public static string GetText(int tableIdx, string id)
        //{
        //    return TextMgr.GetText(GetSheet(tableIdx).GetValue(id, "txt_id"));
        //}
#endregion

        void LoadAllDataTable()
        {       
            for (int i = 0; i < dataFiles.Count; i++)
            {
                if (null == arrDataSheet[i])
                    arrDataSheet[i] = new DataSheet();

                LoadDataSheet(dataFiles[i], arrDataSheet[i]);
                dataSheetIndex.Add(Path.GetFileNameWithoutExtension(dataFiles[i]), i);
                //Debug.Log("[DataMgr] table: " + tableName[i] + " Loaded !!");
            }

            //Debug.Log("[DataMgr] LoadAllDataTable Complete !!");
        }

        void LoadDataSheet(string filePath, DataSheet ds)
        {
            string path = filePath;

            Console.WriteLine("LoadDataSheet:["+path+"]");

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            // Get Sheet Name
            ds.sheetName = Path.GetFileName(filePath);

            // Skip Header 			
            ds.dataRow = System.Convert.ToInt32(br.ReadString()) - 1;
            ds.dataColum = System.Convert.ToInt32(br.ReadString());

            // String Key Value Header
            for (int k = 0; k < ds.dataColum; k++)
            {
                ds.headerList.Add(br.ReadString());
            }

            // Skip 0's Line cause use to header
            for (int j = 0; j < ds.dataRow; j++)
            {
                Dictionary<string, string> info = new Dictionary<string, string>();

                for (int i = 0; i < ds.dataColum; i++)
                {
                    info.Add(ds.headerList[i], br.ReadString());
                }

                if (ds.dataTable.ContainsKey(info["id"]))
                {
                    Console.WriteLine("Error!! Same Key in Sheet: " + ds.sheetName + " key: " + info["id"]);
                }
                else
                {
                    ds.dataTable.Add(info["id"], info);
                }
            }

            br.Close();
            fs.Close();
        }

        #region String...
        bool FindText(string key)
        {
            return textTable.ContainsKey(key);
        }

        public static bool LoadLanguageFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            lineCount = System.Convert.ToInt32(br.ReadString());

            for (int i = 0; i < lineCount; i++)
            {
                textTable.Add(br.ReadString(), br.ReadString());
            }

            fs.Close();
            br.Close();

            return true;
        }
        #endregion
    }

    // Data sheet.
    public class DataSheet
    {
        public string sheetName;
        public int dataRow, dataColum;

        public List<string> headerList = new List<string>();
        public Dictionary<string, Dictionary<string, string>> dataTable = new Dictionary<string, Dictionary<string, string>>();

        private string compare = null;

        public string GetHeader(int idx) { return headerList[idx]; }

        public void Clear()
        {
            dataRow = dataColum = 0;
            headerList.Clear();
            dataTable.Clear();
        }

        public int GetRow { get { return dataRow; } }
        public int GetCol { get { return dataColum; } }

        public string GetValue(string id, string key)
        {
            if (dataTable.ContainsKey(id) && dataTable[id].ContainsKey(key))
                return dataTable[id][key];

            Console.WriteLine("Sheet:[" + sheetName + "]" + " id:[" + id + "] or key:[" + key + "] is not in dataTable..");

            return null;
        }

        public float GetFloat(string id, string key)
        {
            compare = GetValue(id, key);
            return (null != compare ? System.Convert.ToSingle(compare) : 0);
        }

        public int GetInt32(string id, string key)
        {
            compare = GetValue(id, key);
            return (null != compare ? System.Convert.ToInt32(compare) : 0);
        }

        public bool IsCompare(string id, string key, string compareKey)
        {
            return compareKey.Equals(GetValue(id, key));
        }

        public string[] GetSplit(string id, string key)
        {
            compare = GetValue(id, key);
            if (null != compare)
                return compare.Split(',');

            return null;
        }
    }
}
