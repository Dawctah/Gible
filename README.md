# Gible

## Deploying on Local Network
Launch Visual Studio as an Administrator
In Solution Explorer, right click Gible.Web and select `Publish` in the context menu
> Target: Web Server (IIS)
> Server: localhost
> All other fields can remain blank
> *Note* I already had this set up and didn't document it, it's possible this is wrong and I'll have to update it but I'm too lazy rn.
Click the Publish button
My settings drop the published files into `\Gible.Web\bin\Release\net8.0\publish`
*Note* Here we hit another "I already have this set up so redoing it will take some research" but I have my server setup on IIS Manager already. It shouldn't take too much effort to look up how to create a new site and point it at the above folder. I had to enable IIS Manager to actually be able to open it but I don't remember how I did that either. If it's really a problem and you need help then open an issue and I'll figure it out again.