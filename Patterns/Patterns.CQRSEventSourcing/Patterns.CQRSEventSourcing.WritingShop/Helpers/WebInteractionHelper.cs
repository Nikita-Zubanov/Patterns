using Newtonsoft.Json;
using System.Text;

namespace Patterns.CQRSEventSourcing.WritingShop.Helpers
{
	/// <summary>
	/// Хелпер для веб-взаимодействия. 
	/// </summary>
	public static class WebInteractionHelper
	{
		/// <summary>
		/// Тип содержимого "application/json".
		/// </summary>
		private const string JsonMediaType = "application/json";

		/// <summary>
		/// Отправляет сообщение на указанный адрес.
		/// </summary>
		/// <param name="message">Сообщение для отправки.</param>
		/// <param name="uriAddress">Uri-адрес сервиса.</param>
		/// <param name="method">Метод запроса.</param>
		/// <param name="headers">Заголовки запроса.</param>
		/// <returns>Полученный ответ (результат вызова).</returns>
		public static void SendRequest(
			object message,
			Uri uriAddress,
			HttpMethod method = null,
			Dictionary<string, string>? headers = null)
		{
			var responseText = string.Empty;
			try
			{
				if (message == null)
				{
					throw new ArgumentNullException(nameof(message));
				}

				if (uriAddress == null)
				{
					throw new ArgumentNullException(nameof(uriAddress));
				}

				using (var request = new HttpRequestMessage(method ?? HttpMethod.Post, uriAddress))
				{
					request.Content = new StringContent(
						JsonConvert.SerializeObject(message),
						Encoding.UTF8,
						JsonMediaType);

					if (headers != null)
					{
						foreach (var header in headers)
						{
							request.Headers.Add(header.Key, header.Value);
						}
					}

					using (var client = new HttpClient())
					using (var response = client.SendAsync(request).GetAwaiter().GetResult())
					{
						responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

						response.EnsureSuccessStatusCode();
					}
				}
			}
			catch (Exception ex)
			{
				var responseErrorText = !string.IsNullOrWhiteSpace(responseText)
					? $"Текст ответа: {responseText}. "
					: string.Empty;

				throw new Exception(
					$"Метод {nameof(SendRequest)}. " +
					$"При выполнении запроса возникла ошибка. {responseErrorText}\n" +
					$"Текст ошибки: {ex}",
					ex);
			}
		}
	}
}