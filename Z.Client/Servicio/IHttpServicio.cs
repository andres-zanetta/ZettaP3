
namespace Z.Client.Servicio
{
    public interface IHttpServicio
    {
        Task<HttpRespuesta<T>> Get<T>(string url);
    }
}