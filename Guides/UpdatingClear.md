### Updating Clear

1. The Clear `HttpGet` Method changes to the following:
    ```
    public async Task<IActionResult> Clear()
    {
        var token = HttpContext.Session.GetString("currentUser");

        if (token == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        HttpResponseMessage response = await httpClient.DeleteAsync("/api/Math/DeleteHistory?token=" + token);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
        }
        return RedirectToAction("History");
    }
    ```

    What is happening here: 
    1. We check token as before.
    1. We make a request to the API using DeleteAsync.
    1. If successful, we will get a get back a list of MathCalculation objects which we can deserialise. I do not do this.
    1. For you to do: Pass back a message to the view with how many objects delete.

    Note: You need to add exception handling and also handling different status codes.