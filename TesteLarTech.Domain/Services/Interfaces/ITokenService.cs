namespace TesteLarTech.Domain.Service.Interfaces
{
    //TODO: Falta registrar no injetor de dependência
    public interface ITokenService
    {
        public object GerarToken(Guid id, string tipo);
    }
}
