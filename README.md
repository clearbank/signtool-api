# Signing API

.NET Core web api to help with signing payloads correctly

This sample ClearBank® code is intended to provide Financial Institutions examples to help integrate with ClearBank®’s live API.
All information provided by ClearBank® is provided "as is" and without any implied warranty, representation, condition or otherwise, regarding its accuracy or completeness.

## Description

This tool is designed to replicate the signing for FI API calls with payload or WebHook responses.

## Availability

Packaged as C# source code this is targeted at .NET Core, but is compatible with any OS capable of running .NET Core (e.g. Windows, MacOS, Linux). Specifically, you will need .NET Core v3.0 installed.

Contains single API controller with one method, which can be called like this:

```sh
curl -X POST \
  https://localhost:5001/signing \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Content-Length: 8' \
  -H 'Content-Type: text/plain' \
  -H 'Host: staginggreen-wh-server-api-uksouth.azurewebsites.net' \
  -d 'test body'
```
