// public void ReadData ( string _data, Action<string> _ACTION_READ_LINE )
// 		{
// 			string decryptData = IHProjectManager.Instance.resourceManager.FindMetaTextAssetInAssetBundle ( _data );
// 			if ( string.IsNullOrEmpty ( decryptData ) ) {
// 				return;
// 			}

// 			using( StringReader sr = new StringReader( decryptData ) )
// 			{
// 				string readLine;

// 				while( ( readLine = sr.ReadLine() ) != null )
// 				{
// 					if( ! readLine.StartsWith( "#" ) && ! readLine.StartsWith( "\t" ) )
// 					{
// 						_ACTION_READ_LINE (readLine);
// 					}
// 				}
// 			}
// 		}

// /**************************************************/
// IHProjectManager.Instance.dataReader.ReadData("HOW_COLLECTION_OPTIONVALUE", OnCollectionOptionValueReadLine);

// /**************************************************/

// // Option Value ���� �ֱ�
//         private void OnCollectionOptionValueReadLine(string _lines)
//         {
//             string[] word = _lines.Split('\t');
//             STCollectionOptionValueData valueData = new STCollectionOptionValueData();

//             int optionValueIndex = Utils.IntConvert(word[1]);
//             int level = Utils.IntConvert(word[2]);
//             int value = Utils.IntConvert(word[3]);

//             valueData.needLevel = new List<int>();
//             for (int i = 4; i < word.Length; i++)
//             {
//                 int lv = Utils.IntConvert(word[i]);
//                 valueData.needLevel.Add(lv);
//             }
//             // ������ �ֱ�
//             valueData.optionValueIndex = optionValueIndex;
//             valueData.level = level;             
//             valueData.value = value;

//             // ����Ʈ�� ������ �ֱ�
//             if (collectionValueDataDic.ContainsKey(optionValueIndex) == false)
//             {
//                 collectionValueDataDic.Add(optionValueIndex, new List<STCollectionOptionValueData>());
//             }

//             List<STCollectionOptionValueData> listData = collectionValueDataDic[optionValueIndex];
//             listData.Add(valueData);
//         }