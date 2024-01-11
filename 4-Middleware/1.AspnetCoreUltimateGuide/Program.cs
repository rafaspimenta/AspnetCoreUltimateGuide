using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using System.Text;

namespace AspnetCoreUltimateGuide;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.Run(async context =>
        {

            var query = context.Request.QueryString.ToString();

            var dic = QueryHelpers.ParseQuery(query);

            dic.TryGetValue("firstNumber", out var firstNumber);
            dic.TryGetValue("secondNumber", out var secondNumber);
            dic.TryGetValue("operator", out var op);


            var resultContent = new StringBuilder();
            var resultCode = 200;

            if (firstNumber.Count == 0)
            {
                resultContent.Append("Invalid input for 'firstNumber'");
                resultContent.Append("<br>");
                resultCode = 400;
            }
            if (secondNumber.Count == 0)
            {
                resultContent.Append("Invalid input for 'secondNumber'");
                resultContent.Append("<br>");
                resultCode = 400;
            }
            if (op.Count == 0)
            {
                resultContent.Append("Invalid input for 'operator'");
                resultCode = 400;
            }

            if (resultCode == 200)
            {
                if (op == "add")
                {
                    resultContent.Append(int.Parse(firstNumber[0]!) + int.Parse(secondNumber[0]!));
                }
                else if (op == "multiply")
                {
                    resultContent.Append(int.Parse(firstNumber[0]!) * int.Parse(secondNumber[0]!));
                } else
                {
                    resultContent.Append("Invalid input for 'operator'");
                    resultCode = 400;
                }
            }

            context.Response.ContentType = "text/html";
            context.Response.StatusCode = resultCode;
            await context.Response.WriteAsync(resultContent.ToString());

        });

        app.Run();
    }
}