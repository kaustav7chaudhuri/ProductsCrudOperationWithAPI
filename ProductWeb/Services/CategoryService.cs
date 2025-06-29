using System.Text.Json;
using ProductWeb.Models;

namespace ProductWeb.Services
{
    public class CategoryService
    {
        public async Task<IEnumerable<PCategoryWeb>> GetAllCategories()
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_GetAllCategory;
            try
            {
                HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        IEnumerable<PCategoryWeb> webCategories = JsonSerializer.Deserialize<IEnumerable<PCategoryWeb>>(jsonResponse, options);
                        return webCategories;
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

        public async Task<PCategoryWeb> GetCategoryById(int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_GetCategoryById;
            try
            {
                HttpResponseMessage response = client.GetAsync($"{endpoint}/{id}").GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content is not null)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        PCategoryWeb category = JsonSerializer.Deserialize<PCategoryWeb>(jsonResponse);
                        return category;
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

        public async Task<bool> InsertCategory(PCategoryWeb category)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_InsertCategory;
            try
            {
                var jsonContent = JsonSerializer.Serialize(category);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategory(PCategoryWeb category, int Id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_UpdateCategory;
            try
            {
                var jsonContent = JsonSerializer.Serialize(category);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{endpoint}/{Id}" , content).GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_DeleteCategory;
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"{endpoint}/{id}").GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
