**PdnErrorHandling** was developed for a "C# Error Handling" presentation to the PhillyDotNet user group (<a href="https://www.meetup.com/philly-net/events/293263260/" target="_blank">https://www.meetup.com/philly-net/events/293263260</a>).  As such, there there is no documentation on offer (beyond the included PowerPoint), nor does the author have any intent of documenting the code in the near future.

NOTE: Before running the **LoggingDemo**, you'll need to setup a Seq instance on your local <a href="https://learn.microsoft.com/en-us/windows/wsl/install" target="_blank">WSL2</a> instance.  (See <a href="https://docs.datalust.co/docs/getting-started-with-docker" target="_blank">https://docs.datalust.co/docs/getting-started-with-docker</a> for detailed info on how to do just that), although for most folks, running the following script in PowerShell should work just fine:

```powershell
docker run -d --name seq-dev --restart unless-stopped -p 5341:80 
    -v "$(pwd)/seq-dev:/data" -e ACCEPT_EULA=y datalust/seq:latest
```


**Supper-Duper Extra-Important Caveat**:  THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.