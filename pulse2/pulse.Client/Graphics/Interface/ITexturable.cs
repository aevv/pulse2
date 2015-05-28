namespace pulse.Client.Graphics.Interface
{
    interface ITexturable
    {
        int TextureId{ get; }
        string TexturePath { get; }
        void ApplyTexture(string path);
        void ApplyTexture(int textureId);
    }
}
