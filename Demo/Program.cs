using System.Net.Http.Headers;
using System.Text;
using MyWebApi.Models;
using MyWebApi.Security;
using Newtonsoft.Json;
using System.Text.Json;
using Azure;


HttpClient client = new HttpClient
{
    BaseAddress = new Uri("http://localhost/MyWebApi/api/SKUWithDB/")
};

CreateSku();
GetSku();
UpdateSku();
DeleteSku();
//GetError();
//GetSecure();
Console.ReadLine();


void GetSecure()
{



   string loginUrl = "http://localhost/MyWebApi/api/Auth/login";//our mockup login api
  

    LoginModel login = new LoginModel
    {
        Username = "admin",//
        Password = "password"
    };



    var jsonPayload = System.Text.Json.JsonSerializer.Serialize(login);
    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    // Create HttpClient
    using HttpClient client = new HttpClient();

    // Send POST request to Login API
    HttpResponseMessage loginresponse =  client.PostAsync(loginUrl, content).GetAwaiter().GetResult(); 


    if (loginresponse.IsSuccessStatusCode)
    {
        // Parse the response to extract the JWT token
        string responseString =   loginresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var responseObject = JsonConvert.DeserializeObject<JwtResponse>(responseString);

        Console.WriteLine("JWT Token: " + responseObject.Token);

        // Use the token for subsequent requests
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseObject.Token);

        // Example: Call a protected endpoint
        HttpResponseMessage secureResponse =   client.GetAsync("http://localhost/MyWebApi/api/SKUWithDB/Secure").GetAwaiter().GetResult();
        string message =   secureResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        Console.WriteLine("Secure API Response: " + message);
    }



   

 
       


}


void GetError()
{
    HttpResponseMessage response = client.GetAsync("GetError1").GetAwaiter().GetResult();

    if (!response.IsSuccessStatusCode)
    {
        
        string errorDetails = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Console.WriteLine($"Error Response: {errorDetails}");
      //  Console.WriteLine($"Status Code: {(int)response.StatusCode}");
    }
    

}

void CreateSku()
{


    var sku = new SKU
    {
        SKUName = "SKU created with C# code",
        SKUQuantity = 1,
        Price = 10.0,

    };

    string json = JsonConvert.SerializeObject(sku);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    HttpResponseMessage response = client.PostAsync("createsku", content).GetAwaiter().GetResult();
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("SKU created successfully.");
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
    }
}

void GetSku()
{
    HttpResponseMessage response =  client.GetAsync("GetSKUById/10").GetAwaiter().GetResult();
    if (response.IsSuccessStatusCode)
    {
        string result =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();  
        Console.WriteLine($"Response: {result}");
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
    }
}

void UpdateSku()
{
    var sku = new SKU
    {
      SKUQuantity=7,
       SKUName="Update the name",//SKU created with C# code

    };

    string json = JsonConvert.SerializeObject(sku);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    HttpResponseMessage response =  client.PutAsync("UpdateSKU/17", content).GetAwaiter().GetResult();
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("SKU updated successfully.");
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
    }
}

void DeleteSku()
{
    HttpResponseMessage response =  client.DeleteAsync("deletesku/17").GetAwaiter().GetResult(); 
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("SKU deleted successfully.");
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
    }
}
