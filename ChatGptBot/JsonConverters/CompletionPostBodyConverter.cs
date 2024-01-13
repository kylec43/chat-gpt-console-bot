using System.Text.Json;
using System.Text.Json.Serialization;
using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Dto.CompletionApi;

namespace ChatGptBotProject.JsonConverters;

internal class CompletionPostBodyConverter : JsonConverter<CompletionPostBody>
{
    public override CompletionPostBody Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Read is not implemented for CompletionPostBodyConverter");
    }

    public override void Write(Utf8JsonWriter writer, CompletionPostBody value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        WriteModelProperty(writer, value.Model);
        WriteMessagesProperty(writer, value.Messages, options);
        writer.WriteEndObject();
    }

    private void WriteModelProperty(Utf8JsonWriter writer, string model)
    {
        writer.WriteString("model", model);
    }

    private void WriteMessagesProperty(Utf8JsonWriter writer, Messages messages, JsonSerializerOptions options)
    {
        writer.WritePropertyName("messages");
        writer.WriteStartArray();
        foreach (var message in messages)
        {
            JsonSerializer.Serialize(writer, message, options);
        }
        writer.WriteEndArray();
    }
}
