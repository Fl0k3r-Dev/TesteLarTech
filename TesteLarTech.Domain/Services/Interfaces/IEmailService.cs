namespace TesteLarTech.Core.Interfaces
{
    public interface IEmailService
    {
        public bool EnvioEmail(string emailDestino, string tituloEmail, string nomeDestinatario, string corpoEmail, string link);
    }
}
