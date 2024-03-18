### Reusing the code from the MathApp.

Firstly, since the MathAPI Client is a web application, it makes sense that we will reuse several features except the code that deals with a DB and read/write operations. We will replace this with the HttpClient code like we did in the ESP API Client app.

1. `Views` - you can copy all the Views across from the MathApp - please maintain the folder structure.
1. `Utils` - you can copy the Utils across from the MathApp - remember to rename the namespace in code to match the new API client app.
1. `Models` - you can copy the contents of the models folder across, but leave out the` DBContext` class. Again, remember to rename the namespace.
1. `Controllers` - you can copy the `AuthController` class from the MathApp. Don't forget to rename the namespace.
1. At this point, you can also copy the MathController class across, but you will make some significant changes in the next section. You could also build it from scratch and copy the lines you need. Your call.
