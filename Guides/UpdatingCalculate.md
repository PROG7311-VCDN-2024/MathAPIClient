### Updating Calculate

1. Adding in the HttpClient object to point to the API as base URL. Remember to put the port number of your API. Add this to the `MathController`
    ```
    private static HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:{port here}"),
    };
    ```
1. The Calculate `HttpGet` Method stays the same
1. The Calculate `HttpPost` Method changes to the following:
    ```
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Calculate(decimal? FirstNumber, decimal? SecondNumber,int Operation)
    {
        var token = HttpContext.Session.GetString("currentUser");

        if (token == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        decimal? Result = 0;
        MathCalculation mathCalculation;

        try
        {
            mathCalculation = MathCalculation.Create(FirstNumber, SecondNumber, Operation, Result, token);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
            throw;
        }
        
        StringContent jsonContent = new(JsonConvert.SerializeObject(mathCalculation), Encoding.UTF8,"application/json"); 
        
        HttpResponseMessage response = await httpClient.PostAsync("api/Math/PostCalculate", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            MathCalculation? deserialisedResponse = JsonConvert.DeserializeObject<MathCalculation>(jsonResponse);
            ViewBag.Result = deserialisedResponse.Result;
            return View();
        } else
        {
            ViewBag.Result = "An error has occurred";
            return View();
        }
    }
    ```

    What is happening here: 
    1. We get values from the view as before, check token as before, create MathCalculation object as before.
    1. We don't do the calculation in this method anymore as that is done by the API.
    1. We serialise the object and send it across using `PostAsync()`. Note that we are sending the JSON in the body of the request, which the API will receive and process.
    1. If successful, since the API sends back a JSON object with a result, we then deserialize object to get the result.
    1. We pass result back to the view.

    Note: You need to add exception handling and also handling different status codes.
