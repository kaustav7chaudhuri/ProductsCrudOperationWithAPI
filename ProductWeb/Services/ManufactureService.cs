using ProductWeb.Models;
using System.Text.Json;

namespace ProductWeb.Services
{
    public class ManufactureService
    {
        public async Task<IEnumerable<PManufactureWeb>> GetAllManufacturers()
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_GetAllManufacturer;
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        IEnumerable<PManufactureWeb> webManufacturers = JsonSerializer.Deserialize<IEnumerable<PManufactureWeb>>(jsonResponse, options);
                        return webManufacturers;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<PManufactureWeb> GetManufacturerById(int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_GetManufacturerById;
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content is not null)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        PManufactureWeb manufacturer = JsonSerializer.Deserialize<PManufactureWeb>(jsonResponse);
                        return manufacturer;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> InsertManufacturer(PManufactureWeb manufacturer)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_InsertManufacturers;
            try
            {
                var jsonContent = JsonSerializer.Serialize(manufacturer);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateManufacturer(PManufactureWeb manufacturer, int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_UpdateManufacturer;
            try
            {
                var jsonContent = JsonSerializer.Serialize(manufacturer);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{endpoint}/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteManufacturer(int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_DeleteManufacturer;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{endpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
