using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace EasyJson
{
    public static class EasyToJson
    {
        // 주소
        public static string localPath = Application.dataPath + "/00_Database/Json/";
        /**
         * <summary>
         * Json 파일로 저장
         * </summary>
         * <param name="obj">Json으로 저장할 객체</param>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <param name="prettyPrint">Json을 보기 좋게 출력할 지 여부</param>
         */
        public static void ToJson<T>(T obj, string jsonFileName, bool prettyPrint = false)
        {
            if (!Directory.Exists(localPath))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(localPath);
            }
            string path = localPath + jsonFileName + ".json";
            string json = JsonUtility.ToJson(obj, prettyPrint);
            File.WriteAllText(path, json);
            Debug.Log(json);
        }
        
        /**
         * <summary>
         * Json 파일을 읽어서 객체로 반환
         * </summary>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <returns>Json 파일을 읽어서 만든 객체</returns>
         */
        public static T FromJson<T>(string jsonFileName)
        {
            string path = localPath + jsonFileName + ".json";
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                return default;
            }
            string json = File.ReadAllText(path);
            T obj = JsonUtility.FromJson<T>(json);
            return obj;
        }
        
        /**
         * <summary>
         * List를 Json 파일로 저장
         * </summary>
         * <param name="list">Json으로 저장할 리스트</param>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <param name="prettyPrint">Json을 보기 좋게 출력할 지 여부</param>
         */
        public static void ListToJson<T>(List<T> list, string jsonFileName, bool prettyPrint = false)
        {
            Debug.Log(localPath);
            if (!Directory.Exists(localPath))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(localPath);
            }
            string path = Path.Combine(localPath, jsonFileName + ".json");
            string json = JsonConvert.SerializeObject(list, prettyPrint ? Formatting.Indented : Formatting.None);
            File.WriteAllText(path, json);
            Debug.Log(json);
        }
        
        /**
         * <summary>
         * Json 파일을 읽어서 List로 반환
         * </summary>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <returns>Json 파일을 읽어서 만든 List</returns>
         */
        public static List<T> ListFromJson<T>(string jsonFileName)
        {
            string path = Path.Combine(localPath, jsonFileName + ".json");
            string json = File.ReadAllText(path);
            List<T> obj = JsonConvert.DeserializeObject<List<T>>(json);
            return obj;
        }

        /**
         * <summary>
         * Dictionary를 Json 파일로 저장
         * </summary>
         * <param name="dictionary">Json으로 저장할 Dictionary</param>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <param name="prettyPrint">Json을 보기 좋게 출력할 지 여부</param>
         */
        public static void DictionaryToJson<T, U>(Dictionary<T, U> dictionary, string jsonFileName, bool prettyPrint = false)
        {
            if (!Directory.Exists(localPath))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(localPath);
            }
            string path = localPath + jsonFileName + ".json";
            string json = JsonConvert.SerializeObject(dictionary, prettyPrint ? Formatting.Indented : Formatting.None);
            File.WriteAllText(path, json);
            Debug.Log(json);
        }
        
        /**
         * <summary>
         * Json 파일을 읽어서 Dictionary로 반환
         * </summary>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <returns>Json 파일을 읽어서 만든 Dictionary</returns>
         */
        public static Dictionary<T, U> DictionaryFromJson<T, U>(string jsonFileName)
        {
            string path = localPath + jsonFileName + ".json";
            string json = File.ReadAllText(path);
            Dictionary<T, U> obj = JsonConvert.DeserializeObject<Dictionary<T, U>>(json);
            Debug.Log(json);
            return obj;
        }
    }
}