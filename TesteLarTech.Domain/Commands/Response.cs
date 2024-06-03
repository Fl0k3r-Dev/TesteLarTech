
namespace TesteLarTech.Domain.Commands
{
    public class Response
    {
        public Response(object data)
        {
            Data = data;
        }

        public object Data { get; private set; }
    }
}
