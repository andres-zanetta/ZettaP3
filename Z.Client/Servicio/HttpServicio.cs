
using System.Text.Json;

namespace Z.Client.Servicio
{
    public class HttpServicio : IHttpServicio
    {
        protected readonly HttpClient http;
        public HttpServicio(HttpClient http)
        {
            this.http = http;
        }

        public async Task<HttpRespuesta<T>> Get<T>(string url)
        {
            var response = await http.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(response);
                return new HttpRespuesta<T>(respuesta!, false, response);

            }
            else
            {
                return new HttpRespuesta<T>(default, true, response);
            }
        }

        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            var respuesta = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });//que no sea sensible a mayusculas y minusculas
        }
    }
}
