using Btpt.App;
using CommandLine;
using RestSharp;

return await Parser.Default.ParseArguments<CommandLineOptions>(args)
        .MapResult(async (CommandLineOptions opts) =>
        {
            try
            {
                return await Make(opts);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(ex.Message);
                return -3;
            }
        },
        errs => Task.FromResult(-1)); // Invalid arguments

async Task<int> Make(CommandLineOptions opts)
{
    int result = default;

    if (opts.Provider.ToLowerInvariant() != "cingo")
    {
        throw new ArgumentException("Invalid provider! Only cingo is supported now.");
    }

    var client = new RestClient("https://www.cingoportal.com");
    var request = new RestRequest("/becomex/portal/action/Login/view/normal", Method.Post);
    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
    request.AddParameter("action", "login");
    request.AddParameter("domain", "");
    request.AddParameter("user", opts.UserName);
    request.AddParameter("pass", opts.Password);
    request.AddParameter("code", "");
    request.AddParameter("bornDate", "");
    var response = await client.PostAsync(request);

    if (!response.IsSuccessful)
    {
        return -1;
    }

    request = new RestRequest($"/becomex/pontoeletronico/marcacaoponto?timeZone={opts.TimeZone}&latlon={opts.Latitude},{opts.Longitude}", Method.Get);
    request.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
    response = await client.GetAsync(request);

    if (!response.IsSuccessful)
    {
        return -1;
    }

    return result;
}