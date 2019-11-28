# Signing API

.NET Core web api to help with signing payloads correctly

This sample ClearBank® code is intended to provide Financial Institutions examples to help integrate with ClearBank®’s live API.
All information provided by ClearBank® is provided "as is" and without any implied warranty, representation, condition or otherwise, regarding its accuracy or completeness.

## Description

This tool is designed to replicate the signing for FI API calls with payload or WebHook responses.

It is not intended for production use as-is.

## Availability

Packaged as C# source code this is targeted at .NET Core, but is compatible with any OS capable of running .NET Core (e.g. Windows, MacOS, Linux). Specifically, you will need .NET Core v3.0 installed.

Contains single API controller with one method, which can be called like this:

```sh
curl -X POST \
  https://localhost:51280/signing \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Content-Length: 8' \
  -H 'Content-Type: text/plain' \
  -d 'test body'
```

You should get a response similar to :

```json
{"signature":"ga1whxQvQsmkCT1Y+SkLWES4X9rXi9YAMu9PrKB4S2Ls7Z+/3yLhIHYdVLbEtifjog0WDftMERc1Gh5dtJft9gfKCdccagJY1S5Wahw9vF1lmXbgiokrPMhQlLthbAbEg1F/9c1HQyik0kaeACfXxGYSIFwrCj0wSaysG/+touxwnFoHErjo9H63uiPr3xGVxxBr6ADIZCtUjQwgfGZgPfMMwv8WhgQN/BCvo6qrs/TARx90NbGzKpMkojXMJR4AJgmHyWz2K487UfMMWJPoqM5TmrEkpYpboKI4YyBSKy9umRTx3LkUS7cnRxrnu9oGfX19FOk1bgTd+gXELbdvKg=="}
```

This is using a fixed private key in the `Data` folder.

If you are making your `POST` request from an application such as [Postman](https://www.getpostman.com/), you can
invoke this endpoint using a pre-request script to obtain a signature for payload bodies, before they are sent.

```javascript
pm.sendRequest({
    url: 'http://localhost:51280/signing',
    method: 'POST',
    header: {
        'content-type': 'text/plain'
    },
    body: {
        mode: 'raw',
        raw: request.data
    }
}, function (err, response) {
    pm.environment.set("DigitalSignature", response.json().signature);
});
```

This uses the built-in Postman object `pm`, to call the endpoint exposed by the above solution, and sets a variable called `DigitalSignature` which can then be referenced in your Postman request using `{{DigitalSignature}}`

E.g.

- Copy the script above into your Postman pre-request window.

- Set a Header called `DigitalSignature` with a value of `{{DigitalSignature}}`

- Make your `POST` request

- Open the Postman console and your request should have a populated `DigitalSignature` header similar to:

```text
DigitalSignature: "tqUZMD16npsoxdbSTXgIxxNd/qzUkx8ISIiHAiEOV2O/wgpfHU/JYG0X4UG/XsfLHGol8p58atx2zaZ0zl71fRJnBnmwhTsU+R4OE2k1UueoYGtIsAkMvkzHtklxA/fGC1Hs09MRAKvKecNdnwDYxLzcgoEHggTT3sJgep66sPSki9asZfWdGBC45xf7cMEjmmindJPn0Y6rf9V36kCUACT5W2u/dscj+L9UcE+gxt50HMEFW3Vt3onu00CVZqSWCPxnwwg8d+n2vp2wXptj/vi9Ulza+R4HV+o4gEdaDZNInLRwC6NV/hykhUlsU2NPHdhOt7JZob6jDELTxymsJA=="
```

## TODO

- Support HSM Signing via Azure KeyVault
- Support multiple keys
