// public void SetUserArenaRank(JArray _array)
//         {
//             mUserArenaRank.Clear();

//             for (int i = 0; i < _array.Count; i++)
//             {                
//                 JObject _obj = (JObject)_array[i];

//                 STUserArenaRankInfo info = new STUserArenaRankInfo();
//                 info.InitData();
//                 if (_obj["nickname"] != null && _obj["nickname"].Type != JTokenType.Null)
//                 {
//                     info.nickName = (string)_obj["nickname"];
//                 }

//                 if (_obj["player_id"] != null && _obj["player_id"].Type != JTokenType.Null)
//                 {
//                     info.playerID = (int)_obj["player_id"];
//                 }

//                 if (_obj["rank"] != null && _obj["rank"].Type != JTokenType.Null)
//                 {
//                     info.rank = (int)_obj["rank"];
//                 }

//                 if (_obj["score"] != null && _obj["score"].Type != JTokenType.Null)
//                 {
//                     info.score = (int)_obj["score"];
//                 }

//                 if (_obj["team_level"] != null && _obj["team_level"].Type != JTokenType.Null)
//                 {
//                     info.teamLevel = (int)_obj["team_level"];
//                 }
//                 mUserArenaRank.Add(info);
//             }
//         }

//         public void SetMyArenaRank(JObject _data)
//         {
//             float per = (float)(_data["per"]);
//             int rank = (int)(_data["rank"]);
//             int score = (int)(_data["score"]);
                        
//             mMyArenaRank.nickName = IHPlayerDataManager.Instance.playerNickName;
//             mMyArenaRank.playerID = HOW.IHProjectManager.Instance.networkManager.playerID;
//             mMyArenaRank.rank = rank;
//             mMyArenaRank.score = score;
//             mMyArenaRank.teamLevel = IHPlayerDataManager.Instance.teamLevel;
//             mMyArenaRank.per = per;
//         }

//         public void SetResultItem(JArray _array)
//         {
//             for(int i = 0; i < _array.Count; i++)
//             {
//                 eGoodsAndItemList _goodsId = eGoodsAndItemList.NONE;
//                 int _count = 0;

//                 JObject _obj = (JObject)_array[i];

//                 if (_obj["item_count"] != null && _obj["item_count"].Type != JTokenType.Null)
//                 {
//                     _count = (int)_obj["item_count"];
//                 }

//                 if (_obj["item_type"] != null && _obj["item_type"].Type != JTokenType.Null)
//                 {
//                     _goodsId = Utils.ConvertEnumData<eGoodsAndItemList>(_obj["item_type"].ToString());
//                 }

//                 if(_goodsId != eGoodsAndItemList.NONE)
//                 {
//                     IHItemDataManager.Instance.AddPlayerGoodsAndItemCount(_goodsId, _count);
//                 }
//             }
//         }