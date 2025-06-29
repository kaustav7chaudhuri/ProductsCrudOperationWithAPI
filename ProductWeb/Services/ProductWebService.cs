using ProductWeb.Models;
using System.Collections;
using System.Text.Json;

namespace ProductWeb.Services
{
    public class ProductWebService
    {

        public async Task<IEnumerable<WebProduct>> GetAllProducts()
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_GetAllProduct;
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
                        IEnumerable<WebProduct> webProducts = JsonSerializer.Deserialize<IEnumerable<WebProduct>>(jsonResponse, options);
                        return webProducts;
                    }
                    else
                    {
                        return new List<WebProduct>();
                    }
                }
                else
                {
                    return new List<WebProduct>();
                }
            }
            catch (Exception ex)
            {
                return new List<WebProduct>();
            }
            return new List<WebProduct>();
        }

        public async Task<WebProduct> GetProductById(int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_GetProductById;

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content is not null)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        WebProduct product = JsonSerializer.Deserialize<WebProduct>(jsonResponse, options);
                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<bool> InsertProduct(WebProduct product)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_InsertProducts;
            try
            {
                var jsonContent = JsonSerializer.Serialize(product);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public async Task<bool> UpdateProduct(WebProduct product, int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_UpdateProduct;
            try
            {
                var jsonContent = JsonSerializer.Serialize(product);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{endpoint}/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            HttpClient client = new HttpClient();
            string endpoint = ApiEndpointConstant.Endpoint_DeleteProduct;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
    }
}
