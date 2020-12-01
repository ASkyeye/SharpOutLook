# SharpOutLook

This project is a mess about project I used to play about with [API Monitor](http://www.rohitab.com/apimonitor). As I detail [in my blog post on this](https://mez0.cc/posts/extracting-creds-from-outlook/), I had to downgrade the capabilities of the [CredEnumerateA](https://docs.microsoft.com/en-us/windows/win32/api/wincred/nf-wincred-credenumeratea) function by adding an `if` to only ge Office related creds:

```csharp
if (!app.ToLower().Contains("microsoftoffice")) { continue;  }
```
So I want to reiterate, this is a mess about project. If you find use for it, give it a star. Otherwise it exists to be a reference to my blog :)