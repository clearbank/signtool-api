using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace ClearBank.Signing.Api
{
    /// <inheritdoc />
    public class PlainTextInputFormatter : TextInputFormatter
    {
        /// <inheritdoc />
        public PlainTextInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        /// <inheritdoc />
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding effectiveEncoding)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (effectiveEncoding == null) throw new ArgumentNullException(nameof(effectiveEncoding));

            using var reader = new StreamReader(context.HttpContext.Request.Body, effectiveEncoding);
            try
            {
                var result = await reader.ReadToEndAsync();

                return await InputFormatterResult.SuccessAsync(result);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}