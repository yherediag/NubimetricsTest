namespace API.Helpers
{
    public interface IPasswordService
    {
        string Encriptar(string password);
        string Desencriptar(string password);
    }
}
