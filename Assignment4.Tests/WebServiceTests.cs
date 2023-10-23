using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assignment4.Tests;


public class WebServiceTests
{
    private const string CategoriesApi = "http://localhost:5001/api/categories";
    private const string ProductsApi = "http://localhost:5001/api/products";

    /* /api/categories */

    [Fact]
    public async Task ApiCategories_GetWithNoArguments_OkAndAllCategories()
    {
        var (data, statusCode) = await GetArray(CategoriesApi);

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(8, data?.Count);
        Assert.Equal("Beverages", data?.FirstElement("name"));
        Assert.Equal("Seafood", data?.LastElement("name"));
    }

    [Fact]
    public async Task ApiCategories_GetWithValidCategoryId_OkAndCategory()
    {
        var (category, statusCode) = await GetObject($"{CategoriesApi}/1");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("Beverages", category?.Value("name"));
    }

    [Fact]
    public async Task ApiCategories_GetWithInvalidCategoryId_NotFound()
    {
        var (_, statusCode) = await GetObject($"{CategoriesApi}/0");

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

    [Fact]
    public async Task ApiCategories_PostWithCategory_Created()
    {
        var newCategory = new
        {
            Name = "Created",
            Description = ""
        };
        var (category, statusCode) = await PostData(CategoriesApi, newCategory);

        string? id = null;
        if (category?.Value("id") == null)
        {
            var url = category?.Value("url");
            if (url != null)
            {
                id = url.Substring(url.LastIndexOf('/') + 1);
            }
        }
        else
        {
            id = category.Value("id");
        }

        Assert.Equal(HttpStatusCode.Created, statusCode);

        await DeleteData($"{CategoriesApi}/{id}");
    }

    [Fact]
    public async Task ApiCategories_PutWithValidCategory_Ok()
    {

        var data = new
        {
            Name = "Created",
            Description = "Created"
        };
        var (category, _) = await PostData($"{CategoriesApi}", data);

        string? id = null;
        if (category?.Value("id") == null)
        {
            var url = category?.Value("url");
            if (url != null)
            {
                id = url.Substring(url.LastIndexOf('/') + 1);
            }
        }
        else
        {
            id = category?.Value("id");
        }


        var update = new
        {
            Id = category.Value("id"),
            Name = category.Value("name") + "Updated",
            Description = category.Value("description") + "Updated"
        };

        var statusCode = await PutData($"{CategoriesApi}/{id}", update);

        Assert.Equal(HttpStatusCode.OK, statusCode);

        var (cat, _) = await GetObject($"{CategoriesApi}/{id}");

        Assert.Equal(category.Value("name") + "Updated", cat?.Value("name"));
        Assert.Equal(category.Value("description") + "Updated", cat?.Value("description"));

        await DeleteData($"{CategoriesApi}/{id}");
    }

    [Fact]
    public async Task ApiCategories_PutWithInvalidCategory_NotFound()
    {
        var update = new
        {
            Id = -1,
            Name = "Updated",
            Description = "Updated"
        };

        var statusCode = await PutData($"{CategoriesApi}/-1", update);

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

    [Fact]
    public async Task ApiCategories_DeleteWithValidId_Ok()
    {

        var data = new
        {
            Name = "Created",
            Description = "Created"
        };
        var (category, _) = await PostData($"{CategoriesApi}", data);

        string id = null;
        if (category?.Value("id") == null)
        {
            var url = category?.Value("url");
            id = url.Substring(url.LastIndexOf('/') + 1);
        }
        else
        {
            id = category?.Value("id");
        }

        var statusCode = await DeleteData($"{CategoriesApi}/{id}");

        Assert.Equal(HttpStatusCode.OK, statusCode);
    }

    [Fact]
    public async Task ApiCategories_DeleteWithInvalidId_NotFound()
    {

        var statusCode = await DeleteData($"{CategoriesApi}/-1");

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

    /* /api/products */

    [Fact]
    public async Task ApiProducts_ValidId_CompleteProduct()
    {
        var (product, statusCode) = await GetObject($"{ProductsApi}/1");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("Chai", product?.Value("name"));
        Assert.Equal("Beverages", product?.Value("categoryName"));
    }


    [Fact]
    public async Task ApiProducts_InvalidId_CompleteProduct()
    {
        var (_, statusCode) = await GetObject($"{ProductsApi}/-1");

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

    [Fact]
    public async Task ApiProducts_CategoryValidId_ListOfProduct()
    {
        var (products, statusCode) = await GetArray($"{ProductsApi}/category/1");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(12, products?.Count);
        Assert.Equal("Chai", products?.FirstElement("name"));
        Assert.Equal("Beverages", products?.FirstElement("categoryName"));
        Assert.Equal("Lakkalikööri", products?.LastElement("name"));
    }

    [Fact]
    public async Task ApiProducts_CategoryInvalidId_EmptyListOfProductAndNotFound()
    {
        var (products, statusCode) = await GetArray($"{ProductsApi}/category/1000001");

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
        Assert.Equal(0, products?.Count);
    }

    [Fact]
    public async Task ApiProducts_NameContained_ListOfProduct()
    {
        var (products, statusCode) = await GetArray($"{ProductsApi}?name=em");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(4, products?.Count);
        Assert.Equal("NuNuCa Nuß-Nougat-Creme", products?.FirstElement("productName"));
        Assert.Equal("Flotemysost", products?.LastElement("productName"));
    }

    [Fact]
    public async Task ApiProducts_NameNotContained_EmptyListOfProductAndNotFound()
    {
        var (products, statusCode) = await GetArray($"{ProductsApi}?name=CIT");

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
        Assert.Equal(0, products?.Count);
    }

    // Helpers

    async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> PostData(string url, object content)
    {
        var client = new HttpClient();
        var requestContent = new StringContent(
            JsonSerializer.Serialize(content),
            Encoding.UTF8,
            "application/json");
        var response = await client.PostAsync(url, requestContent);
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

    async Task<HttpStatusCode> PutData(string url, object content)
    {
        var client = new HttpClient();
        var response = await client.PutAsync(
            url,
            new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json"));
        return response.StatusCode;
    }

    async Task<HttpStatusCode> DeleteData(string url)
    {
        var client = new HttpClient();
        var response = await client.DeleteAsync(url);
        return response.StatusCode;
    }
}

static class HelperExt
{
    public static string? Value(this JsonNode node, string name)
    {
        var value = node[name];
        return value?.ToString();
    }

    public static string? FirstElement(this JsonArray node, string name)
    {
        return node.First()?.Value(name);
    }

    public static string? LastElement(this JsonArray node, string name)
    {
        return node.Last()?.Value(name);
    }
}