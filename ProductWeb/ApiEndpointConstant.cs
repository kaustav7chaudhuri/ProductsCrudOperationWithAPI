namespace ProductWeb
{
    public class ApiEndpointConstant
    {
        public const string API_BASE_URL = "https://localhost:44361/";

        #region PRODUCT API ENDPOINTS
        public const string Endpoint_GetAllProduct = API_BASE_URL + "api/Product/GetAllProducts";
        public const string Endpoint_GetProductById = API_BASE_URL + "api/Product/GetAllProductById";
        public const string Endpoint_InsertProducts = API_BASE_URL + "api/Product/CreateProduct";
        public const string Endpoint_UpdateProduct = API_BASE_URL + "api/Product/UpdateProduct";
        public const string Endpoint_DeleteProduct = API_BASE_URL + "api/Product/DeleteProduct";
        #endregion

        #region PRODUCT CATEGORY API ENDPOINT
        public const string Endpoint_GetAllCategory = API_BASE_URL + "api/Category/GetAllCategory";
        public const string Endpoint_GetCategoryById = API_BASE_URL + "api/Category/GetAllCategoryById";
        public const string Endpoint_InsertCategory = API_BASE_URL + "api/Category/InsertCategory";
        public const string Endpoint_UpdateCategory = API_BASE_URL + "api/Category/UpdateCategory";
        public const string Endpoint_DeleteCategory = API_BASE_URL + "api/Category/DeleteCategory";
        #endregion

        #region PRODUCT MANUFACTURE API ENDPOINT
        public const string Endpoint_GetAllManufacturer = API_BASE_URL + "api/Manufacturer/GetAllManufacturer";
        public const string Endpoint_GetManufacturerById = API_BASE_URL + "api/Manufacturer/GetAllManufacturerById";
        public const string Endpoint_InsertManufacturers = API_BASE_URL + "api/Manufacturer/InsertManufacturer";
        public const string Endpoint_UpdateManufacturer = API_BASE_URL + "api/Manufacturer/UpdateManufacturer";
        public const string Endpoint_DeleteManufacturer = API_BASE_URL + "api/Manufacturer/DeleteManufacturer";
        #endregion
    }
}
