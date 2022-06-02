using UnityEngine;
// Modificado 25/07/2021
// Biblioteca Feita por: Valdeci Danilo
namespace Duarte{
    public class game{
        public class android{
            #if UNITY_ANDROID && !UNITY_EDITOR
                public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            #else
                public static AndroidJavaClass unityPlayer;
                public static AndroidJavaObject currentActivity;
                public static AndroidJavaObject vibrator;
            #endif
            public static void Vibrate(){
                if (isAndroid())
                    vibrator.Call("vibrate");
                else
                    Handheld.Vibrate();
            }
            public static void Vibrate(long milliseconds){
                if (isAndroid())
                    vibrator.Call("vibrate", milliseconds);
                else
                    Handheld.Vibrate();
            }
            public static void Vibrate(long[] pattern, int repeat){
                if (isAndroid())
                    vibrator.Call("vibrate", pattern, repeat);
                else
                    Handheld.Vibrate();
            }
            public static bool HasVibrator(){
                return isAndroid();
            }
            public static void Cancel(){
                if (isAndroid())
                    vibrator.Call("cancel");
            }
            private static bool isAndroid(){
                #if UNITY_ANDROID && !UNITY_EDITOR
                    return true;
                #else
                    return false;
                #endif
            }
        }
        public static class data{
            public static void SaveBool(string name, bool value){
                if(value){
                    PlayerPrefs.SetInt(name, 1);
                }else{
                    PlayerPrefs.SetInt(name, 0);
                }
            }
            public static void SaveInt(string name, int value){
                PlayerPrefs.SetInt(name, value);
            }
            public static void SaveColor(string name, Color color){
                float r = color.r;
                float g = color.g;
                float b = color.b;
                float a = color.a;
                PlayerPrefs.SetFloat(name + "R", r);
                PlayerPrefs.SetFloat(name + "G", g);
                PlayerPrefs.SetFloat(name + "B", b);
                PlayerPrefs.SetFloat(name + "A", a);
            }
            public static void SaveLong(string name, long value){
                string convert = value.ToString();
                PlayerPrefs.SetString(name, convert);
            }
            public static void SaveDouble(string name, double value){
                string convert = value.ToString();
                PlayerPrefs.SetString(name, convert);
            }
            public static void SaveFloat(string name, float value){
                PlayerPrefs.SetFloat(name, value);
            }
            public static void SaveString(string name, string value){
                PlayerPrefs.SetString(name, value);
            }
            public static void SavePosition(string name, Vector2 vector){
                PlayerPrefs.SetFloat(name + "X", vector.x);
                PlayerPrefs.SetFloat(name + "Y", vector.y);
            }
            public static bool LoadBool(string name){
                if(PlayerPrefs.GetInt(name) == 0){
                    return false;
                }else{
                    return true;
                }
            }
            public static void ResetData(){
                PlayerPrefs.DeleteAll();
                return;
            }
            public static int LoadInt(string name){
                return PlayerPrefs.GetInt(name);
            }
            public static Color LoadColor(string name){
                float r = PlayerPrefs.GetFloat(name + "R");
                float g = PlayerPrefs.GetFloat(name + "G");
                float b = PlayerPrefs.GetFloat(name + "B");
                float a = PlayerPrefs.GetFloat(name + "A");
                Color color = new Color(r, g, b, a);
                return color;
            }
            public static long LoadLong(string name){
                if(PlayerPrefs.GetString(name) == ""){
                    return 0;
                }
                string convert = PlayerPrefs.GetString(name);
                Debug.Log(convert);
                long result = long.Parse(convert);
                return result;
            }
            public static double LoadDouble(string name){
                if(PlayerPrefs.GetString(name) == ""){
                    return 0;
                }
                string convert = PlayerPrefs.GetString(name);
                Debug.Log(convert);
                double result = double.Parse(convert);
                return result;
            }
            public static float LoadFloat(string name){
                return PlayerPrefs.GetFloat(name);
            }
            public static string LoadString(string name){
                return PlayerPrefs.GetString(name);
            }
            public static Vector2 LoadPosition(string name, Vector2 vector){
                float x = PlayerPrefs.GetFloat(name + "X", vector.x);
                float y = PlayerPrefs.GetFloat(name + "Y", vector.y);
                return new Vector2 (x, y);
            }
        }
        public static class Mathf{
            private static System.Random RandomGen = new System.Random();
            public static int Rand(float percent){
                int rand = RandomGen.Next(100);
                if (rand <= percent){
                    return 1;
                }else{
                    return 0;
                }
            }
            public static int GreaterThan3(float number1, float number2, float number3){
                if(number1 > number2 && number1 > number3){
                    return 0;
                }else
                if(number2 > number1 && number2 > number3){
                    return 1;
                }else
                if(number3 > number1 && number3 > number2){
                    return 2;
                }else{
                    return -1;
                }
            }
            public static float ReturnChance(float percent){
                int rand = RandomGen.Next(100);
                if (rand <= percent){
                    return rand;
                }else{
                    return 0f;
                }
            }
            public static bool RandBool(float percent){
                int rand = RandomGen.Next(100);
                if (rand <= percent){
                    return true;
                }else{
                    return false;
                }
            }
            public static float RemaindOf(float currentValue, float totalPercent){
                return (totalPercent / 100.0f) * currentValue;
            }
            public static float PixelforCM(float DPI, float pixels, float multiplifer = 1f){
                return (pixels / DPI) * (2.5f * multiplifer);
            }
            public static float PercentOf(float currentValue, float totalPercent){
                return (100.0f / totalPercent) * currentValue;
            }
        }
    }
}