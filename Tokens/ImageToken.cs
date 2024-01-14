namespace Graven.Hearts.MakeGLTF.Tokens
{
    internal class ImageToken(string name, string uri)
    {
        public string mimeType = "image/png";
        public string name = name;
        public string uri = "." + uri;
    }
}
