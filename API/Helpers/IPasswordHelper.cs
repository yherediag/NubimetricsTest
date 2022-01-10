namespace API.Helpers
{
    public interface IPasswordHelper
    {
        string Encriptar(string password);
        string Desencriptar(string password);
    }
}
