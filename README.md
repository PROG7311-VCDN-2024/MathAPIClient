## Building the MathAPI Client

This repo shows the building of the MathAPI Client in VS. A starting point for this API is the [MathApp](https://github.com/PROG7311-VCDN-2024/MathApp). I recommend building that first so you understand how it all fits together before you break it up :-)

This API covers aspects like:
* Making POST requests to the MathAPI and sending through an object for processing
* Making GET requests to get history
* Making DELETE requests to clear history

Before commencing, please consult the following API documentation as it gives an idea of what we will be doing:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio

**If you notice any errors or need to suggest improvements, please reach out to me!! I will be grateful**

It is recommended that you follow these steps in order:

1. [Creating your project](/Guides/CreatingYourProject.md)
1. [Reusing the various class](/Guides/ReusingVariousClasses.md)
1. [Updating the Calculate() HttpPost to post to API](/Guides/UpdatingCalculate.md)
1. [Updating the History() HttpGet to read from API](/Guides/UpdatingHistory.md)
1. [Updating the Clear() to delete using API](/Guides/UpdatingClear.md)