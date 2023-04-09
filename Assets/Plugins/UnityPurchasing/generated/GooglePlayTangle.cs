#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("/OpToIDfFyUaGQPzpewm5cOZNmX7eq7h+a5dLJiAT5Y40ANYdGfE6vk1bPeAxtEwApov3ZQXDv/4SduTTvx/XE5zeHdU+Db4iXN/f397fn2AwzBXy/LN/G6cIqTZHgpNLWCPypStC3RL7oHzV/Y3j9mvRzgQNCNlTsoust21AjkgNAc45gCya4fqRin8f3F+Tvx/dHz8f39++oXrigq2a8G0dcBfc34wsFxmCC/hA2Gf2CIisokAOB4sOMtOP4kMwNQENxLUCBZUc+gSWPfBVDQ2OpKpkVLs6l+osTu3j4EJQqXspeHizC8nFHwO8e9w0wnUu7jvoIjfffn1mQeFv+I5kcZWuBRmyymzeN+9y3L+QHgVbxtxikuAQaW2Ga7iL3x9f35/");
        private static int[] order = new int[] { 13,7,2,13,6,11,9,7,13,10,12,13,12,13,14 };
        private static int key = 126;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
