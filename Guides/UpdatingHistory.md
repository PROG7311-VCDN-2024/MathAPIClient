### Updating History

1. The History `HttpGet` Method changes to the following:
    ```
    public async Task<IActionResult> History()
    {
        var token = HttpContext.Session.GetString("currentUser");

        if (token == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        
        HttpResponseMessage response = await httpClient.GetAsync("/api/Math/GetHistory?token=" + token);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            List<MathCalculation>? deserialisedResponse = JsonConvert.DeserializeObject<List<MathCalculation>>(jsonResponse);
            if (deserialisedResponse.Count == 0)
            {
                ViewBag.HistoryMessage = "No history exists";
            }
            return View(deserialisedResponse);
        }  else
        {
            ViewBag.HistoryMessage = "No history to show";
            return View();
        }            
    }
    ```

    What is happening here: 
    1. We check token as before.
    1. We make a request to the API using GetAsync.
    1. If successful, we then get back a list of MathCalculation objects which we can deserialise
    1. We pass list back to the view.

    Note: You need to add exception handling and also handling different status codes.